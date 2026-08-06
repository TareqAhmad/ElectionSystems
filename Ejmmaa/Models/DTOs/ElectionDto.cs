
namespace Ejmmaa.Models.DTOs
{
    public class ElectionDto
    {
        public int ElectionId { get; set; }
        public string? ElectionTitle { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int IsActive { get; set; }
        public int ClanID { get; set; }
    }
}