using Microsoft.Data.SqlClient;
using System.Data;
    
using Ejmmaa.Services.Interfaces;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;
using Ejmmaa.Data;

namespace Ejmmaa.Services.Implementations
{
    public class PollingStationsService : IPollingStationsService
    {

        private readonly DbHelper _dbHelper; 
         private readonly Helper _helper;

        public PollingStationsService(DbHelper dbHelper, Helper helper)
        {
                 _dbHelper = dbHelper; 
                 _helper = helper;
        }


      public List<PollingStationsViewModel> GetAllPollingStations()
        {
            List<PollingStationsViewModel> pollingStations = new List<PollingStationsViewModel>(); 

            string query = @"SELECT stationId, stationName, RegionName,LocationDetails
                             FROM Polling_Stations S
                             LEFT JOIN Regions R ON S.RegionId = R.RegionId"; 


             DataTable dt = _dbHelper.Select(query); 

            if (dt != null && dt.Rows.Count > 0)
            {  
                foreach (DataRow row in dt.Rows)
                {
                    var pollingStation = new PollingStationsViewModel
                    {
                        StationId = Convert.ToInt32(row["stationId"]), 
                        StationName = row["stationName"].ToString(),
                        RegionName =  row["RegionName"].ToString(),
                        LocationDetails =  row["LocationDetails"].ToString(),
     
                    };

                    pollingStations.Add(pollingStation); 
                }
            }


            return pollingStations; 

        }
    
      public PollingStationsViewModel GetPollingStationById(int pollingStationId)
        {
            string query = @"SELECT S.stationId, S.stationName, R.RegionId, R.RegionName, S.LocationDetails
                             FROM Polling_Stations S
                             LEFT JOIN Regions R ON S.RegionId = R.RegionId
                             WHERE S.stationId = @stationId";

            var parameters = new[]
            {
                new SqlParameter("@stationId", pollingStationId)
            };

            DataTable dt = _dbHelper.Select(query, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                var  pollingStation=  new PollingStationsViewModel
                {
                    StationId = Convert.ToInt32(row["stationId"]),
                    StationName = row["stationName"].ToString(),
                    RegionId = Convert.ToInt32(row["RegionId"]),
                    RegionName = row["RegionName"].ToString(),
                    LocationDetails = row["LocationDetails"].ToString()
                };

                return pollingStation;
            }

            return null;
        }

      public bool AddPollingStation(PollingStationsDto pollingStationsDto)
      {
            if (pollingStationsDto == null) return false;

            string query  = @"INSERT INTO Polling_Stations(StationName,RegionId,LocationDetails)
                            VALUES(@StationName,@RegionId,@LocationDetails)";
         
            var parameters = new[]
            {
                new SqlParameter("@StationName", pollingStationsDto.StationName),
                new SqlParameter("@RegionId", pollingStationsDto.RegionId),
                new SqlParameter("@LocationDetails", pollingStationsDto.LocationDetails),
            };


            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
      }

     public bool UpdatePollingStation(PollingStationsDto pollingStationsDto)
      {
            if (pollingStationsDto == null) return false;

            string query  = @"UPDATE Polling_Stations
                            SET StationName = @StationName,
                                RegionId = @RegionId,
                                LocationDetails = @LocationDetails
                            WHERE StationId = @StationId";
         
            var parameters = new[]
            {
                new SqlParameter("@StationId", pollingStationsDto.StationId),
                new SqlParameter("@StationName", pollingStationsDto.StationName),
                new SqlParameter("@RegionId", pollingStationsDto.RegionId),
                new SqlParameter("@LocationDetails", pollingStationsDto.LocationDetails),
            };

            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
        }
    
    
    public bool DeletePollingStation(PollingStationsDto pollingStationsDto  )
        {
            if (pollingStationsDto == null) return false;

            string query = @"DELETE FROM Polling_Stations WHERE StationId = @StationId";

            var parameters = new[]
            {
                new SqlParameter("@StationId", pollingStationsDto.StationId)
            };

            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
        }
            
    }
}
