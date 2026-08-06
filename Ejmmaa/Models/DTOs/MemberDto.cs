
namespace Ejmmaa.Models.DTOs
{

  public class MemberDto
    {
        public int MemberId { get; set;}
        public string? FullName { get; set; }
        public string? NationalId { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int SectionId { get; set; }
        public char? Gender {get; set;}
        public int ClanId { get; set; }
    }

}