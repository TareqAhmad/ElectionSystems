using Microsoft.Data.SqlClient;
using System.Data;
    
using Ejmmaa.Services.Interfaces;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;
using Ejmmaa.Data;

namespace Ejmmaa.Services.Implementations
{
    public class MembersService : IMembersService
    {

        private readonly DbHelper _dbHelper; 
         private readonly Helper _helper;

        public MembersService(DbHelper dbHelper, Helper helper)
        {
                 _dbHelper = dbHelper; 
                 _helper = helper;
        }


      
        public List<ClanMembersViewModel> GetClanMembersData(MemberDto member)
        {
            List<ClanMembersViewModel> clanMembers = new List<ClanMembersViewModel>(); 
            
            string query = @"SELECT MemberId,fullName,NationalId,PhoneNumber,BirthDate,Gender,S.SectionName
                            FROM Clan_Members M
                            join Clan_Sections S on M.SectionId = S.SectionId
                            where M.ClanId = @ClanId"; 
            
            var parameters = new[]
            {
                new SqlParameter("@ClanId",member.ClanId),
            };               
            
            DataTable dt = _dbHelper.Select(query,parameters);       
          
            if (dt != null && dt.Rows.Count > 0)
            {  
                foreach (DataRow row in dt.Rows)
                {
                    var clanMember = new ClanMembersViewModel
                    {
                        // تأكد أن أسماء الخصائص تطابق الـ ViewModel تماماً
                        MemberId = Convert.ToInt32(row["MemberId"]), 
                        FullName = row["FullName"].ToString(),
                        NationalId =  row["NationalId"].ToString(),
                        PhoneNumber =  row["PhoneNumber"].ToString(),
                        BirthDate = Convert.ToDateTime(row["BirthDate"]),
                        Gender = Convert.ToChar(row["Gender"]),
                        SectionName = row["SectionName"].ToString()
                    };

                    clanMembers.Add(clanMember); // حرف A كبير في Add
                }
            }

            return clanMembers; 
        }
   
         public ClanMembersViewModel GetClanMember(MemberDto memberDto)
        {
           var  a  = new ClanMembersViewModel();
           return  a; 
        }

        public List<VotersViewModel> GetAllVoters(VotersDto votersDto)
        {
            List<VotersViewModel> voters = new List<VotersViewModel>(); 
            
            string query = @"SELECT MemberId,fullName,NationalId,PhoneNumber,BirthDate,Gender,IsEligible
                            FROM Clan_Members
                            where ClanId = @ClanId
                            AND IsEligible = @IsEligible"; 
            
            var parameters = new[]
            {
                new SqlParameter("@ClanId",votersDto.ClanId),
                new SqlParameter("@IsEligible",1),
            };               
            
             DataTable dt = _dbHelper.Select(query,parameters);       
          
            if (dt != null && dt.Rows.Count > 0)
            {  
                foreach (DataRow row in dt.Rows)
                {
                    var voter = new VotersViewModel
                    {
                        MemberId = Convert.ToInt32(row["MemberId"]), 
                        FullName = row["FullName"].ToString(),
                        NationalId =  row["NationalId"].ToString(),
                        PhoneNumber =  row["PhoneNumber"].ToString(),
                        BirthDate = Convert.ToDateTime(row["BirthDate"]),
                        Gender = Convert.ToChar(row["Gender"]),
                        IsEligible = Convert.ToInt32(row["IsEligible"])
                    };

                    voters.Add(voter); // حرف A كبير في Add
                }
            }

            return voters; 
        }

        public VotersViewModel GetVoter(VotersDto votersDto)
        {
            var  a  = new VotersViewModel();
           return  a; 
        }

        public bool AddMember(MemberDto memberDto)
        {
            string query = @"INSERT INTO Clan_Members (FullName, NationalId, PhoneNumber, SectionId, BirthDate, ClanId) 
                     VALUES (@FullName, @NationalId, @PhoneNumber,@SectionId, @BirthDate, @ClanId)";

            var parameters = new[]
            {
                new SqlParameter("@FullName", memberDto.FullName),
                new SqlParameter("@NationalId", memberDto.NationalId),
                new SqlParameter("@PhoneNumber", memberDto.PhoneNumber),
                new SqlParameter("@SectionId", memberDto.SectionId),
                new SqlParameter("@BirthDate", (object)memberDto.BirthDate ?? DBNull.Value), // لحماية التاريخ إذا كان فارغاً
                new SqlParameter("@ClanId", memberDto.ClanId)
            };

            // افترض أن _dbHelper.Execute تعيد عدد الصفوف المتأثرة أو true/false عند النجاح
            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
        }

        
        public bool UpdateMember(MemberDto memberDto)
        {
            string query = @"UPDATE Clan_Members 
                             SET FullName = @FullName, 
                                 NationalId = @NationalId, 
                                 PhoneNumber = @PhoneNumber, 
                                 SectionId = @SectionId, 
                                 BirthDate = @BirthDate
                             WHERE MemberId = @MemberId";

            var parameters = new[]
            {
                new SqlParameter("@FullName", memberDto.FullName),
                new SqlParameter("@NationalId", memberDto.NationalId),
                new SqlParameter("@PhoneNumber", memberDto.PhoneNumber),
                new SqlParameter("@SectionId", memberDto.SectionId),
                new SqlParameter("@BirthDate", (object)memberDto.BirthDate ?? DBNull.Value), // لحماية التاريخ إذا كان فارغاً
                new SqlParameter("@MemberId", memberDto.MemberId)
            };

            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
        }
  
  
        public bool DeleteMember(MemberDto memberDto)
        {
            string query = @"DELETE FROM Clan_Members WHERE MemberId = @MemberId";

            var parameters = new[]
            {
                new SqlParameter("@MemberId", memberDto.MemberId)
            };

            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
        }
  
  
  
    }
}
