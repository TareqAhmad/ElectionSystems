namespace Ejmmaa.Models.ViewModels
{
    public class BallotBoxesViewModel
    {
        public int BoxId { get; set; }
        public string? BoxNumber { get; set; }
        public int StationId { get; set; }
        public string? StationName {get; set;}
        public bool Status { get; set; }
    }
}