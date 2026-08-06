

namespace Ejmmaa.Models.Entities
{
    public class ClanMembers 
    {
        public int MemberID { get; set; }
        public string? FullName { get; set; }
        public string? NationalID { get; set; }
        public string? PhoneNumber { get; set; }
        public int SectionID { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; } 
        public bool IsEligible { get; set; }
    }
}