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
                            where M.ClanId = @ClanId
                            AND M.IsShow = @IsShow"; 
            
            var parameters = new[]
            {
                new SqlParameter("@ClanId",member.ClanId),
                 new SqlParameter("@IsShow", 1)
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
   
         public ClanMembersViewModel GetMemberById(MemberDto memberDto)
        {
            if (memberDto.MemberId <= 0 || memberDto.ClanId <= 0 || memberDto == null)
            {
               return null; // أو يمكنك رمي استثناء أو التعامل مع الحالة بطريقة أخرى
            }

            string query = @"SELECT MemberId,fullName,NationalId,PhoneNumber,BirthDate,Gender,
                             S.SectionId, S.SectionName
                            FROM Clan_Members M
                            join Clan_Sections S on M.SectionId = S.SectionId
                            where M.MemberId = @MemberId 
                            AND M.ClanId = @ClanId
                            AND M.IsShow = @IsShow";

            var parameters = new[]
            {
                new SqlParameter("@MemberId", memberDto.MemberId),
                new SqlParameter("@ClanId", memberDto.ClanId),
                new SqlParameter("@IsShow", 1)
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
                        SectionId = Convert.ToInt32(row["SectionId"]),
                        SectionName = row["SectionName"].ToString()
                    };

                    return clanMember; // حرف A كبير في Add
                }
            }

            return null; 
        }

        public List<VotersViewModel> GetAllVoters(VotersDto votersDto)
        {
            List<VotersViewModel> voters = new List<VotersViewModel>(); 
            
            string query = @"SELECT MemberId,fullName,NationalId,PhoneNumber,BirthDate,Gender,IsEligible
                            FROM Clan_Members
                            where ClanId = @ClanId
                            AND IsEligible = @IsEligible
                            AND IsShow = @IsShow"; 
            
            var parameters = new[]
            {
                new SqlParameter("@ClanId",votersDto.ClanId),
                new SqlParameter("@IsEligible",1),
                 new SqlParameter("@IsShow", 1)
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
            if(memberDto == null || memberDto.MemberId <= 0)
            {
                return false; // أو يمكنك رمي استثناء أو التعامل مع الحالة بطريقة أخرى
            };

            string query = @"UPDATE Clan_Members 
                             SET FullName = @FullName, 
                                 NationalId = @NationalId, 
                                 PhoneNumber = @PhoneNumber, 
                                 SectionId = @SectionId, 
                                 BirthDate = @BirthDate
                             WHERE MemberId = @MemberId
                             AND ClanId = @ClanId
                             AND IsShow = @IsShow"; // تأكد من إضافة شرط ClanId إذا كان ذلك ضرورياً

            var parameters = new[]
            {
                new SqlParameter("@MemberId", memberDto.MemberId),
                new SqlParameter("@FullName", memberDto.FullName),
                new SqlParameter("@NationalId", memberDto.NationalId),
                new SqlParameter("@PhoneNumber", memberDto.PhoneNumber),
                new SqlParameter("@BirthDate", (object)memberDto.BirthDate ?? DBNull.Value), // لحماية التاريخ إذا كان فارغاً
                new SqlParameter("@SectionId", memberDto.SectionId),
                new SqlParameter("@ClanId", memberDto.ClanId),
                 new SqlParameter("@IsShow", 1)
            };

            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
        }
  
  
        public bool DeleteMember(MemberDto memberDto)
        {
            string query = @"UPDATE Clan_Members 
                            SET IsShow = @IsShow
                            WHERE ClanId = @ClanId
                            AND MemberId = @MemberId
                            AND IsShow = 1";

            var parameters = new[]
            {
                new SqlParameter("@ClanId", memberDto.ClanId),
                new SqlParameter("@MemberId", memberDto.MemberId),
                new SqlParameter("@IsShow",SqlDbType.Bit){Value = 0}
            };

            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
        }
  
  
  
    }
}
