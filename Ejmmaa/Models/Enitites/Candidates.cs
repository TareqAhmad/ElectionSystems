

namespace Ejmmaa.Models.Entities
{
    public class Candidates 
    {
        public int CandidateID { get; set; }
        public int MemberID { get; set; }
        public int TypeID { get; set; }
        public int ClanID { get; set; }
        public string? Slogan { get; set; }
        public string? CandidateImage { get; set; }
        public bool IsApproved { get; set; }
    }
}