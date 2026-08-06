
namespace Ejmmaa.Models.ViewModels
{

  public class VotersViewModel
    {
        public int MemberId { get; set;}
        public string? FullName { get; set; }
        public string? NationalId { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public char? Gender {get; set;}
        public int IsEligible {get; set;}
    }

}