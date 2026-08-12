using Microsoft.Data.SqlClient;
using System.Data;
    
using Ejmmaa.Services.Interfaces;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;
using Ejmmaa.Data;

namespace Ejmmaa.Services.Implementations
{
    public class SectionsService : ISectionsService
    {

        private readonly DbHelper _dbHelper; 
         private readonly Helper _helper;

        public SectionsService(DbHelper dbHelper, Helper helper)
        {
                 _dbHelper = dbHelper; 
                 _helper = helper;
        }


       public List<SectionsViewModel> GetAllSections(SectionDto sectionDto)
        {
            List<SectionsViewModel> sections = new List<SectionsViewModel>(); 
            
            string query = @"SELECT S.SectionId,S.SectionName,Count(M.MemberId) AS MemberCount
                            FROM Clan_Sections S
                            LEFT JOIN Clan_Members M ON S.SectionId = M.SectionId
                            WHERE S.ClanId = @ClanId
                            AND S.IsShow = @IsShow
                            GROUP BY S.SectionId,S.SectionName"; 
            
            var parameters = new[]
            {
                new SqlParameter("@ClanId", sectionDto.ClanId),
                new SqlParameter("@IsShow",1)
            };               
            
         DataTable dt = _dbHelper.Select(query,parameters);       
          
        if (dt != null && dt.Rows.Count > 0)
            {  
                foreach (DataRow row in dt.Rows)
                {
                    var section = new SectionsViewModel
                    {
                        // تأكد أن أسماء الخصائص تطابق الـ ViewModel تماماً
                        SectionId = Convert.ToInt32(row["SectionId"]), 
                        SectionName = row["SectionName"].ToString(),
                        MemberCount = Convert.ToInt32(row["MemberCount"]) // تأكد من تحويل القيمة إلى int
                    };

                    sections.Add(section); // حرف A كبير في Add
                }
            }

            return sections; 
        }
      
      public SectionsViewModel GetSectionById(SectionDto sectionDto)
        {
            SectionsViewModel section = null;
            
            string query = @"SELECT S.SectionId,S.SectionName,Count(M.MemberId) AS MemberCount
                            FROM Clan_Sections S
                            LEFT JOIN Clan_Members M ON S.SectionId = M.SectionId
                            WHERE S.ClanId = @ClanId AND S.SectionId = @SectionId
                            AND S.IsShow = @IsShow
                            GROUP BY S.SectionId,S.SectionName"; 
            
            var parameters = new[]
            {
                new SqlParameter("@ClanId", sectionDto.ClanId),
                new SqlParameter("@SectionId", sectionDto.SectionId),
                new SqlParameter("@IsShow",1)
            };               
            
         DataTable dt = _dbHelper.Select(query,parameters);       
          
        if (dt != null && dt.Rows.Count > 0)
            {  
                DataRow row = dt.Rows[0];
                    return section = new SectionsViewModel
                    {
                        // تأكد أن أسماء الخصائص تطابق الـ ViewModel تماماً
                        SectionId = Convert.ToInt32(row["SectionId"]), 
                        SectionName = row["SectionName"].ToString(),
                        MemberCount = Convert.ToInt32(row["MemberCount"]) // تأكد من تحويل القيمة إلى int
                    };

                }

                return null;
            }

      public bool AddSection(SectionDto  sectionDto)
        {
            string query  = @"INSERT INTO Clan_Sections(SectionName,ClanId)
                            VALUES(@SectionName,@ClanId)";
         
            var parameters = new[]
            {
                new SqlParameter("@SectionName", sectionDto.SectionName),
                new SqlParameter("@ClanId", sectionDto.ClanId)
            };


            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
        }
      public bool UpdateSection(SectionDto sectionDto)
        {
            string query = @"UPDATE Clan_Sections
                             SET SectionName = @SectionName
                             WHERE SectionId = @SectionId
                             AND IsShow = @IsShow";

            var parameters = new[]
            {
                new SqlParameter("@SectionName", sectionDto.SectionName),
                new SqlParameter("@SectionId", sectionDto.SectionId),
                 new SqlParameter("@IsShow", 1)
            };

            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
        }
    
      public bool DeleteSection(SectionDto sectionDto)
        {
            string query = @"UPDATE  Clan_Sections
                             SET IsShow = @IsShow
                             WHERE SectionId = @SectionId
                             AND IsShow = 1";

            var parameters = new[]
            {
                new SqlParameter("@SectionId", sectionDto.SectionId),
               new SqlParameter("@IsShow",SqlDbType.Bit){Value = 0}
            };

            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
        }
    }
}
