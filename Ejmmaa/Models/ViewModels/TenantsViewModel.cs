namespace Ejmmaa.Models.ViewModels
{
    public class TenantsViewModel
    {
        
        public int SubscriptionID { get; set; }
        public int TenantId { get; set; }

        public string? PackageName { get; set; }

        public decimal Price { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Status { get; set; }
        public string? TenantName { get; set; }
        public int IsActive { get; set; }
        public string? CreatedAt { get; set; }

    
    }
}
