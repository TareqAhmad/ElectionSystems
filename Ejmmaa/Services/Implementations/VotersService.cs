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
            
            string query = @"SELECT O.OTPId,M.memberId,M.fullName,O.Otp_Code,FullName,C.TenantId,C.ClanId,E.ElectionId
                             FROM OTP_Registry O
                             INNER JOIN Clan_Members M ON O.memberId = M.memberId
                             INNER JOIN Clans C ON M.ClanId = C.ClanId
                             INNER JOIN Clan_Elections E ON E.ClanId = C.ClanId
                             WHERE M.NationalId = @UserName 
                             AND O.Otp_Code = @Password
                             AND O.IsUsed = 0
                             AND M.IsEligible = 1
                             AND O.IsShow = @IsShowOTP
                             AND M.IsShow = @IsShowM";
            
            var parameters = new[]
            {
                new SqlParameter("@UserName", loginRequest.UserName),
                new SqlParameter("@Password", loginRequest.Password),
                new SqlParameter("@IsShowOTP", 1),
                new SqlParameter("@IsShowM", 1),

            };

             DataTable dt = _dbHelper.Select(query,parameters);       
           
          if (dt.Rows.Count > 0)
          {  
              var row = dt.Rows[0];
              return new UserViewModel
              {
                  UserID = Convert.ToInt32(row["OTPId"]),
                  MemberId = Convert.ToInt32(row["MemberId"]),
                  FullName = row["FullName"].ToString(),
                  TenantId = Convert.ToInt32(row["TenantId"]),
                  ClanId = Convert.ToInt32(row["ClanId"]),
                  ElectionId = Convert.ToInt32(row["ElectionId"])
              };
          }

          throw new InvalidOperationException("Invalid username or password");
        }


        public bool SubmitVote(VotingRegistryDto votingRegistryDto)
        {

            string query = @"INSERT INTO Voting_Registry(MemberId,ElectionId,BoxId,VotedAt,IsShow)
                            VALUES(@MemberId,@ElectionId,@BoxId,@VotedAt,@IsShow);"; 

            var parameters = new[]
            {
                new SqlParameter("@MemberId",votingRegistryDto.MemberId),
                new SqlParameter("@ElectionId",votingRegistryDto.ElectionId),
                new SqlParameter("@BoxId",votingRegistryDto.BoxId),
                new SqlParameter("@VotedAt",DateTime.Now),
                new SqlParameter("@IsShow",1),

            }  ;
            
            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
          
        }


    }
}