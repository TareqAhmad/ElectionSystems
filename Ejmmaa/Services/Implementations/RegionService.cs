using Microsoft.Data.SqlClient;
using System.Data;
    
using Ejmmaa.Services.Interfaces;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;
using Ejmmaa.Data;

namespace Ejmmaa.Services.Implementations
{
    public class RegionsService : IRegionsService
    {

        private readonly DbHelper _dbHelper; 
         private readonly Helper _helper;

        public RegionsService(DbHelper dbHelper, Helper helper)
        {
                 _dbHelper = dbHelper; 
                 _helper = helper;
        }


      public List<RegionsViewModel> GetAllRegions()
        {
            List<RegionsViewModel> regions = new List<RegionsViewModel>(); 

            string query = @"SELECT RegionId, RegionName
                             FROM Regions"; 


             DataTable dt = _dbHelper.Select(query); 

            if (dt != null && dt.Rows.Count > 0)
            {  
                foreach (DataRow row in dt.Rows)
                {
                    var region = new RegionsViewModel
                    {
                        RegionId = Convert.ToInt32(row["regionId"]), 
                        RegionName = row["regionName"].ToString(),
     
                    };

                    regions.Add(region); 
                }
            }


            return regions; 

        }
    
      public RegionsViewModel GetRegionById(int RegionId)
        {
            string query = @"SELECT RegionId, RegionName
                             FROM Regions
                             WHERE RegionId = @RegionId";
                           
             var parameters = new[]
            {
                new SqlParameter("@RegionId", RegionId),
            };
             
            DataTable dt = _dbHelper.Select(query, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                var region = new RegionsViewModel
                {
                    RegionId = Convert.ToInt32(row["RegionId"]),
                    RegionName = row["RegionName"].ToString(),
                };

                return region;
            }

            return null; 
        }

      public bool AddRegion(RegionDto regionDto)
        {
            if (regionDto == null) return false;

            string query  = @"INSERT INTO Regions(RegionName)
                            VALUES(@RegionName)";
         
            var parameters = new[]
            {
                new SqlParameter("@RegionName", regionDto.RegionName),
            };


            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
        }

      public bool UpdateRegion(RegionDto regionDto)
        {
            if (regionDto == null) return false;

            string query  = @"UPDATE Regions
                              SET RegionName = @RegionName
                              WHERE RegionId = @RegionId";
         
            var parameters = new[]
            {
                new SqlParameter("@RegionId", regionDto.RegionId),
                new SqlParameter("@RegionName", regionDto.RegionName),
            };      

            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;

        }

      public bool DeleteRegion(RegionDto regionDto)
        {
            string query = @"DELETE FROM Regions
                             WHERE RegionId = @RegionId";

            var parameters = new[]
            {
                new SqlParameter("@RegionId", regionDto.RegionId),
            };

            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
        }
   
   
    }
}
