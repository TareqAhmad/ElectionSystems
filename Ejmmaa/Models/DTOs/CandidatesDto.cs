namespace Ejmmaa.Models.DTOs
{
    public class CandidatesDto
    {
        
         public int CandidateId { get; set; }
         public int ElectionId {get; set;}
        public int MemberId {get; set;}
        public string? FullName { get; set; }
        public string? NationalId {get; set;}
        public string? PhoneNumber {get; set;}
        public string? TypeName {get; set;}
        public string? CandidateImage {get; set;}
        public char IsApproved {get; set;}

    }
}
