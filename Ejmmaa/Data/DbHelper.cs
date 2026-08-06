using System.Data;
//using Ejmmaa.;
using Microsoft.Data.SqlClient;

namespace Ejmmaa.Data
{
    public class DbHelper
    {
        private readonly IConfiguration? _configuration; 
        private readonly string? _connectionString;

        public DbHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration?.GetConnectionString("DefaultConnection");
        }
       

        public DataTable Select(string query,SqlParameter[] parameters = null)
        {
            DataTable dataTable  = new DataTable();

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

         // Insert Update Delete
        public int Execute(string query,SqlParameter[] parameters = null,SqlTransaction transaction = null)
        {
            SqlConnection conn = transaction != null ? transaction.Connection : new SqlConnection(_connectionString);
            
                using(SqlCommand cmd = new SqlCommand(query, conn,transaction))
                {

                   if (parameters != null)
                    {
                        cmd.Parameters.Clear(); // Clear existing parameters to avoid conflicts
                        cmd.Parameters.AddRange(parameters);
                    }

                   if(conn.State != ConnectionState.Open) conn.Open();

                   int result = cmd.ExecuteNonQuery();

                   if (transaction == null) conn.Close(); // Close connection if we opened it here

                   return result;
                }
        }

        public int ExecuteScalarWithoutStoredProcedure(string query, SqlParameter[] parameters = null,SqlTransaction transaction = null)
        {
            SqlConnection conn = transaction != null ? transaction.Connection : new SqlConnection(_connectionString);

            using (SqlCommand cmd = new SqlCommand(query, conn,transaction))
            {
                cmd.CommandType = CommandType.Text;

                if (parameters != null)
                {
                    cmd.Parameters.Clear(); // Clear existing parameters to avoid conflicts
                    cmd.Parameters.AddRange(parameters);
                }

                if(conn.State != ConnectionState.Open) conn.Open();

                var result = cmd.ExecuteScalar();

                if(transaction == null) conn.Close(); // Close connection if we opened it here

                if (result != null  && result != DBNull.Value )
                { 
                 
                   return Convert.ToInt32(result); // Return 0 if result is null or DBNull
                } 
                return 0;
                    
            }  

            
        }

        public object ExecuteScalar(string storedProcedureName, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                conn.Open();

                var result = cmd.ExecuteScalar();

                return result;
            }  

            
        }

        public SqlDataReader ExecuteReaderWithStoredProcedure(string spName, SqlParameter[] parameters = null)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(spName, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            conn.Open();

            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }




    }
}

     

