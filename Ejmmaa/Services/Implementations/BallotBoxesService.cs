using Microsoft.Data.SqlClient;
using System.Data;
    
using Ejmmaa.Services.Interfaces;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;
using Ejmmaa.Data;

namespace Ejmmaa.Services.Implementations
{
    public class BallotBoxesService : IBallotBoxesService
    {

        private readonly DbHelper _dbHelper; 
         private readonly Helper _helper;

        public BallotBoxesService(DbHelper dbHelper, Helper helper)
        {
                 _dbHelper = dbHelper; 
                 _helper = helper;
        }


      public List<BallotBoxesViewModel> GetAllBallotBoxes()
        {
            List<BallotBoxesViewModel> ballotBoxes = new List<BallotBoxesViewModel>(); 

            string query = @"SELECT BoxId, BoxNumber, StationName,Status
                             FROM Ballot_Boxes B
                             LEFT JOIN Polling_Stations P ON B.StationId = P.StationId
                              AND B.IsShow = @IsShow"; 
 
             var sqlParameters  = new []
             {
                  new SqlParameter("@IsShow", 1)
             }; 

             DataTable dt = _dbHelper.Select(query,sqlParameters); 

            if (dt != null && dt.Rows.Count > 0)
            {  
                foreach (DataRow row in dt.Rows)
                {
                    var ballotBox = new BallotBoxesViewModel
                    {
                        BoxId = Convert.ToInt32(row["BoxId"]), 
                        BoxNumber = row["BoxNumber"].ToString(),
                        StationName =  row["StationName"].ToString(),
                        Status =  Convert.ToBoolean(row["Status"])
     
                    };

                    ballotBoxes.Add(ballotBox); 
                }
            }


            return ballotBoxes; 

        }
    
      public BallotBoxesViewModel GetBallotBoxById(BallotBoxesDto ballotBoxesDto)
        {
            if (ballotBoxesDto == null) return null;

            string query = @"SELECT BoxId, BoxNumber, StationId, Status
                             FROM Ballot_Boxes
                             WHERE BoxId = @BoxId
                             AND IsShow = @IsShow";

            var parameters = new[]
            {
                new SqlParameter("@BoxId", ballotBoxesDto.BoxId),
                 new SqlParameter("@IsShow", 1)
            };

            DataTable dt = _dbHelper.Select(query, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                var ballotBox = new BallotBoxesViewModel
                {
                    BoxId = Convert.ToInt32(row["BoxId"]),
                    BoxNumber = row["BoxNumber"].ToString(),
                    StationId = Convert.ToInt32(row["StationId"]),
                    Status = Convert.ToBoolean(row["Status"])
                };

                return ballotBox;
            }

            return null;
        }


      public bool AddBallotBox(BallotBoxesDto ballotBoxesDto)
      {
            if (ballotBoxesDto == null) return false;

            string query  = @"INSERT INTO Ballot_Boxes(BoxNumber,StationId,Status)
                            VALUES(@BoxNumber,@StationId,@Status)";
         
            var parameters = new[]
            {
                new SqlParameter("@BoxNumber", ballotBoxesDto.BoxNumber),
                new SqlParameter("@StationId", ballotBoxesDto.StationId),
                new SqlParameter("@Status", 1),
            };


            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
      }

      public bool UpdateBallotBox(BallotBoxesDto ballotBoxesDto)
      {
            if (ballotBoxesDto == null) return false;

            string query  = @"UPDATE Ballot_Boxes
                              SET BoxNumber = @BoxNumber,
                                  StationId = @StationId,
                                  Status = @Status
                              WHERE BoxId = @BoxId
                              AND IsShow = @IsShow";
         
            var parameters = new[]
            {
                new SqlParameter("@BoxId", ballotBoxesDto.BoxId),
                new SqlParameter("@BoxNumber", ballotBoxesDto.BoxNumber),
                new SqlParameter("@StationId", ballotBoxesDto.StationId),
                new SqlParameter("@Status", ballotBoxesDto.Status),
                 new SqlParameter("@IsShow", 1)
            };


            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;

      }

      public bool DeleteBallotBox(BallotBoxesDto ballotBoxesDto)
        {
                if (ballotBoxesDto == null) return false;
    
                string query  = @"UPDATE Ballot_Boxes
                                 SET IsShow = @IsShow,
                                 WHERE BoxId = @BoxId
                                 AND IsShow = 1";
             
                var parameters = new[]
                {
                    new SqlParameter("@BoxId", ballotBoxesDto.BoxId),
                    new SqlParameter("@IsShow",SqlDbType.Bit){Value = 0}
                };

                int rowsAffected = _dbHelper.Execute(query, parameters);

                return rowsAffected > 0;

        }
    }
}
