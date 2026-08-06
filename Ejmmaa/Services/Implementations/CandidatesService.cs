using Microsoft.Data.SqlClient;
using System.Data;
    
using Ejmmaa.Services.Interfaces;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;
using Ejmmaa.Data;

namespace Ejmmaa.Services.Implementations
{
    public class CandidatesService : ICandidatesService
    {
         private readonly DbHelper _dbHelper; 
         private readonly Helper _helper;

        public CandidatesService(DbHelper dbHelper, Helper helper)
            {
                 _dbHelper = dbHelper; 
                 _helper = helper;
            }

      public List<CandidatesViewModel> GetAllCandidates(CandidatesDto candidatesDto)
    {
        List<CandidatesViewModel> candidates = new  List<CandidatesViewModel>(); 
        string query = @"SELECT 
                                c.CandidateID,
                                c.ElectionId,
                                m.FullName,
                                m.NationalID,
                                m.PhoneNumber,
                                m.Gender,
                                m.BirthDate,
                                t.TypeName ,
                                c.Slogan,
                                c.CandidateImage,
                                c.IsApproved
                            FROM Candidates c
                            INNER JOIN Clan_Members m ON c.MemberID = m.MemberID
                            INNER JOIN Candidacy_Types t ON c.TypeID = t.TypeID
                            WHERE c.ElectionId = @ElectionId;"; 

            var parameters = new[]
            {
                new SqlParameter("@ElectionId",candidatesDto.ElectionId)
            };
                            
                            
            DataTable dt = _dbHelper.Select(query,parameters);       
          
            if (dt != null && dt.Rows.Count > 0)
            {  
                foreach (DataRow row in dt.Rows)
                {
                    var candidate = new CandidatesViewModel
                    {
                        CandidateId = Convert.ToInt32(row["CandidateID"]), 
                        FullName = row["FullName"].ToString(),
                        NationalId =  row["NationalId"].ToString(),
                        PhoneNumber =  row["PhoneNumber"].ToString(),
                        Gender = Convert.ToChar(row["Gender"]),
                        BirthDate = row["BirthDate"].ToString(),
                        Slogan = row["Slogan"].ToString(),
                        CandidateImage = row["CandidateImage"].ToString(),
                    };

                    candidates.Add(candidate); 
                }
            }

           return candidates;                      
    }
     
    
      public CandidatesViewModel GetCandidateById(int candidateId)
        {
            string query = @"SELECT 
                                c.CandidateID,
                                e.ElectionId,
                                e.ElectionTitle,
                                m.FullName,
                                m.NationalID,
                                m.PhoneNumber,
                                m.Gender,
                                m.BirthDate,
                                t.TypeName ,
                                c.Slogan,
                                c.CandidateImage,
                                c.IsApproved
                            FROM Candidates c
                            INNER JOIN Clan_Members m ON c.MemberID = m.MemberID
                            INNER JOIN Candidacy_Types t ON c.TypeID = t.TypeID
                            Inner JOIN Clan_Elections e ON c.ElectionId = e.ElectionId
                            WHERE c.CandidateID = @CandidateId;"; 

            var parameters = new[]
            {
                new SqlParameter("@CandidateId", candidateId)
            };

            DataTable dt = _dbHelper.Select(query, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                return new CandidatesViewModel
                {
                    CandidateId = Convert.ToInt32(row["CandidateID"]),
                    ElectionId = Convert.ToInt32(row["ElectionId"]),
                    ElectionTitle = row["ElectionTitle"].ToString(),
                    FullName = row["FullName"].ToString(),
                    NationalId = row["NationalId"].ToString(),
                    PhoneNumber = row["PhoneNumber"].ToString(),
                    Gender = Convert.ToChar(row["Gender"]),
                    BirthDate = row["BirthDate"].ToString(),
                    TypeName = row["TypeName"].ToString()
                   // Slogan = row["Slogan"].ToString(),
                    //CandidateImage = row["CandidateImage"].ToString()
                };
            }

            return null;
        }

      public bool AddCandidate(CandidatesDto candidatesDto)
      {
            if (candidatesDto == null) return false;

            string query  = @"INSERT INTO Candidates(ElectionId,MemberId,TypeId)
                            VALUES(@ElectionId,@MemberId,@TypeId)";
         
            var parameters = new[]
            {
                new SqlParameter("@ElectionId", candidatesDto.ElectionId),
                new SqlParameter("@MemberId", candidatesDto.MemberId),
               new SqlParameter("@TypeId", 1)

            };


            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
      }


      public bool UpdateCandidate(CandidatesDto candidatesDto)
      {
            if (candidatesDto == null) return false;

            string query  = @"UPDATE Candidates
                            SET ElectionId = @ElectionId,
                                MemberId = @MemberId,
                                TypeId = @TypeId
                            WHERE CandidateId = @CandidateId";
         
            var parameters = new[]
            {
                new SqlParameter("@ElectionId", candidatesDto.ElectionId),
                new SqlParameter("@MemberId", candidatesDto.MemberId),
                //new SqlParameter("@TypeId", candidatesDto.TypeId),
                new SqlParameter("@CandidateId", candidatesDto.CandidateId)
            };

            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
      }        
   
   

     public bool DeleteCandidate(CandidatesDto candidatesDto)
        {
                if (candidatesDto == null) return false;
    
                string query  = @"DELETE FROM Candidates
                                WHERE CandidateId = @CandidateId";
             
                var parameters = new[]
                {
                    new SqlParameter("@CandidateId", candidatesDto.CandidateId)
                };
    
                int rowsAffected = _dbHelper.Execute(query, parameters);
    
                return rowsAffected > 0;
        }   
   
   
   
   
   
   
    }

}

