using Microsoft.Data.SqlClient;
using System.Data;
    
using Ejmmaa.Services.Interfaces;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;
using Ejmmaa.Data;

namespace Ejmmaa.Services.Implementations
{
    public class ClansService : IClansService
    {

        private readonly DbHelper _dbHelper; 
         private readonly Helper _helper;

        public ClansService(DbHelper dbHelper, Helper helper)
        {
                 _dbHelper = dbHelper; 
                 _helper = helper;
        }
         public List<ClanViewModel> GetClanData(UserDto user)
        {
            List<ClanViewModel> clans = new List<ClanViewModel>(); 

            string query  = @"SELECT ClanId,ClanName,CreatedAt
                              FROM Clans
                              WHERE ClanId = @ClanId
                              AND TenantId = @TenantId
                               AND IsActive = 1"; 

                              
            var parameters = new[]
            {
                new SqlParameter("@ClanId", user.ClanId),
                new SqlParameter("@TenantId", user.TenantId)
            };               
             
             
          DataTable dt = _dbHelper.Select(query,parameters);       
          
          if (dt.Rows.Count > 0)
          {  
             foreach (DataRow row in dt.Rows)
              {
                  var clan = new ClanViewModel
                  {
                      ClanId  = Convert.ToInt32(row["ClanId"]),
                      ClanName = row["ClanName"].ToString(),
                      CreatedAt = Convert.ToDateTime(row["CreatedAt"]),

                  };

                  clans.Add(clan);

              }
          }

          return clans;
            
        } 
         public bool AddClan(ClanDto  clanDto)
        {
            string query  = @"INSERT INTO Clans(ClanName,CreatedAt)
                            VALUES(@ClanName,@CreatedAt)";
         
            var parameters = new[]
            {
                new SqlParameter("@ClanName", clanDto.ClanName),
                new SqlParameter("@CreatedAt", clanDto.CreatedAt)
            };


            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
        }

    }
}
