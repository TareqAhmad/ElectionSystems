using Microsoft.Data.SqlClient;
using System.Data;
    
using Ejmmaa.Services.Interfaces;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;
using Ejmmaa.Data;

namespace Ejmmaa.Services.Implementations
{
    public class VotersService : IVotersService
    {
            private readonly DbHelper _dbHelper; 
            private readonly Helper _helper;

            public VotersService(DbHelper dbHelper, Helper helper)
            {
                 _dbHelper = dbHelper; 
                 _helper = helper;
            }


        public UserViewModel Login(LoginRequest loginRequest)
        {
           // string passwordHash = _helper.ComputeMd5Hash(loginRequest.password);
            
            string query = @"SELECT O.OTPId,M.fullName,O.Otp_Code,FullName,C.TenantId,C.ClanId
                             FROM OTP_Registry O
                             INNER JOIN Clan_Members M ON O.memberId = M.memberId
                             inner JOIN Clans C ON M.ClanId = C.ClanId
                             WHERE M.NationalId = @UserName 
                             AND O.Otp_Code = @Password
                             AND O.IsUsed = 0
                             AND M.IsEligible = 1";
            
            var parameters = new[]
            {
                new SqlParameter("@UserName", loginRequest.UserName),
                new SqlParameter("@Password", loginRequest.Password)
            };

          DataTable dt = _dbHelper.Select(query,parameters);       
          
          if (dt.Rows.Count > 0)
          {  
              var row = dt.Rows[0];
              return new UserViewModel
              {
                  UserID = Convert.ToInt32(row["OTPId"]),
                  FullName = row["FullName"].ToString(),
                  TenantId = Convert.ToInt32(row["TenantId"]),
                  ClanId = Convert.ToInt32(row["ClanId"])
              };
          }

          throw new InvalidOperationException("Invalid username or password");
        }





    }
}