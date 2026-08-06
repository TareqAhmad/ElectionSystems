namespace Ejmmaa.Models.ViewModels
{
    public class PollingStationsViewModel
    {
        public int StationId { get; set; }
        public string? StationName { get; set; }
        public int RegionId { get; set; }
        public string? RegionName {get; set;}
        public string? LocationDetails { get; set; }
    }
}