namespace Ejmmaa.Models.ViewModels
{
    public class CandidatesViewModel
    {
        
         public int CandidateId { get; set; }
         public int ElectionId {get; set;}

         public string? ElectionTitle {get; set;}
        public string? FullName { get; set; }
        public string? NationalId {get; set;}
        public string? PhoneNumber {get; set;}
        public char? Gender {get; set; }
        public string? BirthDate {get; set;}
        public string? TypeName {get; set;}
        public string? CandidateImage {get; set;}
        public string? Slogan {get; set;}
        public char IsApproved {get; set;}

    }
}
