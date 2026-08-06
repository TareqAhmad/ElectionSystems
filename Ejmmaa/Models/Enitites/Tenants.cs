namespace Ejmmaa.Models.Entities
{
    public class Tenants 
    {

    public int TenantId { get; set; }
    public string? TenantName { get; set; }
    public int IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    
    }
}
