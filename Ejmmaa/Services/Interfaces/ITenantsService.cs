
using Ejmmaa.Models.DTOs;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Services.Interfaces
{
    public interface ITenantsService
    {

          public List<TenantsViewModel> GetAllTenantsIsActive(TenantsDto tenantsDto); 

          public TenantsViewModel GetTenantById(TenantsDto tenantsDto); 
          public bool AddTenant(TenantsDto  tenantsDto); 

          public bool UpdateTenant(TenantsDto tenantsDto);

          public bool DeleteTenant(TenantsDto tenantsDto);

    }
}