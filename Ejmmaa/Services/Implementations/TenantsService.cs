using Microsoft.Data.SqlClient;
using System.Data;
    
using Ejmmaa.Services.Interfaces;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;
using Ejmmaa.Data;

namespace Ejmmaa.Services.Implementations
{
    public class TenantsService : ITenantsService
    {

        private readonly DbHelper _dbHelper; 
         private readonly Helper _helper;

        public TenantsService(DbHelper dbHelper, Helper helper)
        {
                 _dbHelper = dbHelper; 
                 _helper = helper;
        }


         public List<TenantsViewModel> GetAllTenantsIsActive(TenantsDto tenantsDto)
        {
             List<TenantsViewModel> tenants = new List<TenantsViewModel>();
             
             string query = @"SELECT 
                                    ts.SubscriptionID,
                                    t.TenantName ,
                                    p.PackageName ,
                                    p.Price  ,
                                    ts.StartDate,
                                    ts.EndDate,
                                  CASE 
                                      WHEN ts.IsActive = 1 AND ts.EndDate >= GETDATE() THEN N'فعال'
                                      ELSE N'منتهي أو موقوف'
                                  END AS Status
                              FROM TenantSubscriptions ts
                              INNER JOIN Tenants t ON ts.TenantID = t.TenantID
                              INNER JOIN Packages p ON ts.PackageID = p.PackageID;
                              AND ts.IsShow = @IsShow";

             SqlParameter[] parameters = new SqlParameter[]
             {
                 new SqlParameter("@IsActive", tenantsDto.IsActive),
                  new SqlParameter("@IsShow", 1)
             };

             var dataTable = _dbHelper.Select(query, parameters);

             foreach (DataRow row in dataTable.Rows)
             {
                 tenants.Add(new TenantsViewModel
                 {
                     SubscriptionID = Convert.ToInt32(row["SubscriptionID"]),
                     TenantName = row["TenantName"].ToString(),
                     PackageName = row["PackageName"].ToString(),
                     Price = Convert.ToDecimal(row["Price"]),
                     StartDate = Convert.ToDateTime(row["StartDate"]).ToString("dd/MM/yyyy"),
                     EndDate = Convert.ToDateTime(row["EndDate"]).ToString("dd/MM/yyyy"),
                     Status = row["Status"].ToString()
                 });
             }

             return tenants;
        }

          public TenantsViewModel GetTenantById(TenantsDto tenantsDto)
        {
             var tenant = new TenantsViewModel();
             return tenant;
        }
          public bool AddTenant(TenantsDto  tenantsDto)
        {
            return false;
        }

        public bool UpdateTenant(TenantsDto tenantsDto)
        {
            return false;
        }

        public bool DeleteTenant(TenantsDto tenantsDto)
        {
            return false;
        }
        
    }
}
