using Microsoft.Data.SqlClient;
using System.Data;
    
using Ejmmaa.Services.Interfaces;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;
using Ejmmaa.Data;

namespace Ejmmaa.Services.Implementations
{
    public class ElectionsService : IElectionsService
    {

        private readonly DbHelper _dbHelper; 
         private readonly Helper _helper;

        public ElectionsService(DbHelper dbHelper, Helper helper)
        {
                 _dbHelper = dbHelper; 
                 _helper = helper;
        }
        public List<ElectionsViewModel> GetAllElections(ElectionDto electionDto)
        {
            List<ElectionsViewModel> elections = new List<ElectionsViewModel>(); 

            string query  = @"SELECT ElectionId,ElectionTitle,StartDate,EndDate
                              FROM Clan_Elections
                              WHERE ClanId = @ClanId
                              AND IsActive = 1"; 

                              
            var parameters = new[]
            {
                new SqlParameter("@ClanId", electionDto.ClanID)
            };               
             
             
             DataTable dt = _dbHelper.Select(query,parameters);       
          
            if (dt.Rows.Count > 0)
            {  
                foreach(DataRow row in dt.Rows)
                    {
                    var election =  new ElectionsViewModel
                        {
                            ElectionId  = Convert.ToInt32(row["ElectionId"]),
                            ElectionTitle = row["ElectionTitle"].ToString(),
                            StartDate = Convert.ToDateTime(row["StartDate"]),
                            EndDate = Convert.ToDateTime(row["EndDate"]),
                        };

                        elections.Add(election); 
                    }

            }

            return elections; 

            
        } 
        public bool AddElection(ElectionDto electionDto)
        {
            if (electionDto == null) return false;

            string query  = @"INSERT INTO Clan_Elections(ElectionTitle, StartDate, EndDate, IsActive,ClanId)
                            VALUES(@ElectionTitle,@StartDate,@EndDate,@IsActive,@ClanId)";
         
            var parameters = new[]
            {
                new SqlParameter("@ElectionTitle", electionDto.ElectionTitle),
                new SqlParameter("@StartDate",electionDto.StartDate),
                new SqlParameter("@EndDate",electionDto.EndDate),
                new SqlParameter("@IsActive",1),
                new SqlParameter("@ClanId", electionDto.ClanID)
            };


            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
        }

       public bool UpdateElection(ElectionDto electionDto)
        {
            if (electionDto == null) return false;

            string query  = @"UPDATE Clan_Elections
                              SET ElectionTitle = @ElectionTitle,
                                  StartDate = @StartDate,
                                  EndDate = @EndDate
                              WHERE ElectionId = @ElectionId";

            var parameters = new[]
            {
                new SqlParameter("@ElectionTitle", electionDto.ElectionTitle),
                new SqlParameter("@StartDate",electionDto.StartDate),
                new SqlParameter("@EndDate",electionDto.EndDate),
                new SqlParameter("@ElectionId", electionDto.ElectionId)
            };

            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
        }
   
   
       public bool DeleteElection(ElectionDto electionDto)
        {
                if (electionDto == null) return false;
    
                string query  = @"DELETE FROM Clan_Elections
                                WHERE ElectionId = @ElectionId";
             
                var parameters = new[]
                {
                    new SqlParameter("@ElectionId", electionDto.ElectionId)
                };
    
                int rowsAffected = _dbHelper.Execute(query, parameters);

                return rowsAffected > 0;
        }
   
    }
}
