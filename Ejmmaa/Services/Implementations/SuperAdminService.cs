using Microsoft.Data.SqlClient;
using System.Data;
    
using Ejmmaa.Services.Interfaces;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;
using Ejmmaa.Data;

namespace Ejmmaa.Services.Implementations
{
    public class SuperAdminService : ISuperAdminService
    {
            private readonly DbHelper _dbHelper; 
            private readonly Helper _helper;

            public SuperAdminService(DbHelper dbHelper, Helper helper)
            {
                 _dbHelper = dbHelper; 
                 _helper = helper;
            }


        public UserViewModel Login(LoginRequest loginRequest)
        {
            string passwordHash = _helper.ComputeMd5Hash(loginRequest.Password);
            
            string query = @"SELECT userId,FullName
                             FROM System_Users
                             WHERE Email = @UserName 
                             AND PasswordHash = @Password
                             AND IsShow = @IsShow";
            
            var parameters = new[]
            {
                new SqlParameter("@UserName", loginRequest.UserName),
                new SqlParameter("@Password", passwordHash),
                 new SqlParameter("@IsShow", 1)
            };

          DataTable dt = _dbHelper.Select(query,parameters);       
          
          if (dt.Rows.Count > 0)
          {  
              var row = dt.Rows[0];
              return new UserViewModel
              {
                  UserID = Convert.ToInt32(row["userId"]),
                  FullName = row["FullName"].ToString()
              };
          }

          throw new InvalidOperationException("Invalid username or password");
        }





    }
}