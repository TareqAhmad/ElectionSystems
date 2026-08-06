using Microsoft.Data.SqlClient;
using System.Data;
    
using Ejmmaa.Services.Interfaces;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;
using Ejmmaa.Data;

namespace Ejmmaa.Services.Implementations
{
    public class SupervisorsService : ISupervisorsService
    {
            private readonly DbHelper _dbHelper; 
            private readonly Helper _helper;

            public SupervisorsService(DbHelper dbHelper, Helper helper)
            {
                 _dbHelper = dbHelper; 
                 _helper = helper;
            }


        public UserViewModel Login(LoginRequest loginRequest)
        {
            string passwordHash = _helper.ComputeMd5Hash(loginRequest.Password);
            
            string query = @"SELECT supervisorId,FullName
                             FROM Election_Supervisors
                             WHERE userName = @UserName 
                             AND PasswordHash = @Password";
            
            var parameters = new[]
            {
                new SqlParameter("@UserName", loginRequest.UserName),
                new SqlParameter("@Password", passwordHash)
            };

          DataTable dt = _dbHelper.Select(query,parameters);       
          
          if (dt.Rows.Count > 0)
          {  
              var row = dt.Rows[0];
              return new UserViewModel
              {
                  UserID = Convert.ToInt32(row["supervisorId"]),
                  FullName = row["FullName"].ToString(),
              };
          }

          throw new InvalidOperationException("Invalid username or password");
        }
       
        public List<ElectionSupervisorsViewModel> GetAllElectionSupervisors()
        {
            List<ElectionSupervisorsViewModel> electionSupervisors = new List<ElectionSupervisorsViewModel>(); 


            string query  = @"SELECT S.SupervisorId,S.FullName,S.NationalId,S.PhoneNumber,B.BoxNumber
                              FROM Election_Supervisors S
                              JOIN Ballot_Boxes B ON S.BoxId = B.BoxId
                              WHERE IsActive = @IsActive"; 

                      
                              
            var parameters = new[]
            {
                new SqlParameter("@IsActive", 1)
            };               
             
             
             DataTable dt = _dbHelper.Select(query,parameters);       
          
            if (dt.Rows.Count > 0)
            {  
                foreach(DataRow row in dt.Rows)
                    {
                    var supervisor =  new ElectionSupervisorsViewModel
                        {
                            SupervisorID  = Convert.ToInt32(row["SupervisorId"]),
                            FullName = row["FullName"].ToString(),
                            NationalId = row["NationalId"].ToString(),
                            PhoneNumber = row["PhoneNumber"].ToString(),
                            BoxNumber = row["BoxNumber"].ToString()
                        };

                        electionSupervisors.Add(supervisor); 
                    }

            }

            return electionSupervisors; 
        } 
        
        public ElectionSupervisorsViewModel GetSupervisorById(int supervisorId)
        {
            string query  = @"SELECT S.SupervisorId,S.FullName,S.NationalId,S.PhoneNumber,B.BoxId,B.BoxNumber
                              FROM Election_Supervisors S
                              JOIN Ballot_Boxes B ON S.BoxId = B.BoxId
                              WHERE SupervisorId = @SupervisorId AND IsActive = @IsActive"; 

                      
                              
            var parameters = new[]
            {
                new SqlParameter("@SupervisorId", supervisorId),
                new SqlParameter("@IsActive", 1)
            };               
             
             
             DataTable dt = _dbHelper.Select(query,parameters);       
          
            if (dt.Rows.Count > 0)
            {  
                var row = dt.Rows[0];
                return new ElectionSupervisorsViewModel
                        {
                            SupervisorID  = Convert.ToInt32(row["SupervisorId"]),
                            FullName = row["FullName"].ToString(),
                            NationalId = row["NationalId"].ToString(),
                            PhoneNumber = row["PhoneNumber"].ToString(),
                            BoxId = Convert.ToInt32(row["BoxId"]),
                            BoxNumber = row["BoxNumber"].ToString()
                        };
            }

            throw new InvalidOperationException("Supervisor not found");
        }
        public bool AddSupervisor(ElectionSupervisorsDto electionSupervisorsDto)
        {
             if (electionSupervisorsDto == null) return false;

            string query  = @"INSERT INTO Election_Supervisors(FullName, NationalId, PhoneNumber, BoxId,UserName,PasswordHash,IsActive)
                            VALUES(@FullName,@NationalId,@PhoneNumber,@BoxId,@UserName,@PasswordHash,@IsActive)";

        string passwordHash = _helper.ComputeMd5Hash(electionSupervisorsDto.PasswordHash);
              
         
            var parameters = new[]
            {
                new SqlParameter("@FullName", electionSupervisorsDto.FullName),
                new SqlParameter("@NationalId",electionSupervisorsDto.NationalId),
                new SqlParameter("@PhoneNumber",electionSupervisorsDto.PhoneNumber),
                new SqlParameter("@BoxId", electionSupervisorsDto.BoxId),
                new SqlParameter("@UserName", electionSupervisorsDto.Username),    
                new SqlParameter("@PasswordHash", passwordHash),                
                new SqlParameter("@IsActive",1),
            };


            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;

        }

        public bool UpdateSupervisor(ElectionSupervisorsDto electionSupervisorsDto)
        {
            if (electionSupervisorsDto == null) return false;

            string query  = @"UPDATE Election_Supervisors
                              SET FullName = @FullName,
                                  NationalId = @NationalId,
                                  PhoneNumber = @PhoneNumber,
                                  BoxId = @BoxId
                              WHERE SupervisorId = @SupervisorId";
         
            var parameters = new[]
            {
                new SqlParameter("@SupervisorId", electionSupervisorsDto.SupervisorID),
                new SqlParameter("@FullName", electionSupervisorsDto.FullName),
                new SqlParameter("@NationalId",electionSupervisorsDto.NationalId),
                new SqlParameter("@PhoneNumber",electionSupervisorsDto.PhoneNumber),
                new SqlParameter("@BoxId", electionSupervisorsDto.BoxId)
            };  

            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;

        }

        public bool DeleteSupervisor(ElectionSupervisorsDto electionSupervisorsDto)
        {
            if (electionSupervisorsDto == null) return false;

            string query  = @"UPDATE Election_Supervisors
                              SET IsActive = @IsActive
                              WHERE SupervisorId = @SupervisorId";
         
            var parameters = new[]
            {
                new SqlParameter("@SupervisorId", electionSupervisorsDto.SupervisorID),
                new SqlParameter("@IsActive", 0)
            };  

            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
        }
      


    }
}