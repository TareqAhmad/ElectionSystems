
using Microsoft.AspNetCore.Mvc;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Services.Interfaces;
using Microsoft.AspNetCore.Http.Connections;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Controllers
{

    public class TenantsController : Controller
    {

       private readonly ITenantsService _tenantsService;  

         public TenantsController(ITenantsService tenantsService)
          {
                _tenantsService = tenantsService; 
          }


          public IActionResult Index()
          {
                return View(); 
          }

          public IActionResult Create()
          {
                return View(); 
          }
          public IActionResult Edit(int tenantId)
          {
               var tenantObject = new TenantsDto{TenantId = tenantId}; 
                
                var tenant = _tenantsService.GetTenantById(tenantObject); 

                return View(tenant); 
          }
         public IActionResult Delete(int tenantId)
          {
               var tenantObject = new TenantsDto{TenantId = tenantId}; 
                
                var tenant = _tenantsService.GetTenantById(tenantObject); 

                return View(tenant); 
          }
         public IActionResult GetAllTenantsIsActive()
          {
                var tenantsData = new TenantsDto{
                     IsActive = 1
                }; 

                var tenants = _tenantsService.GetAllTenantsIsActive(tenantsData); 
                 
                 if(tenants == null || tenants.Count == 0)
                 {
                     return Json(new{ success = false, message = "No active tenants found." }); 
                 }

                return Json(new{ success = true, data = tenants }); 
          }
         public IActionResult GetTenantIsActiveById(int tenantId)
            {
                 var tenantsData = new TenantsDto{
                     TenantId = tenantId,
                     IsActive = 1
                }; 

                var tenant = _tenantsService.GetTenantById(tenantsData); 
                 
                 if(tenant == null)
                 {
                     return Json(new{ success = false, message = "لا يوجد بيانات" }); 
                 }

                return Json(new{ success = true, data = tenant }); 
                  
            }
         public IActionResult SaveTenant([FromBody] TenantsDto tenantsDto)
            {
                        
                  var Result = _tenantsService.AddTenant(tenantsDto); 

                  if(Result)
                  return Json(new {success = true,message = "تم الاضافة بنجاح"}); 
                  else
                  return Json(new{success = false ,message = "حدث خطأ اثناء الاضافة"});
            }

         public IActionResult UpdateTenant([FromBody] TenantsDto tenantsDto)
            {
                        
                  var Result = _tenantsService.UpdateTenant(tenantsDto); 

                  if(Result)
                  return Json(new {success = true,message = "تم التعديل بنجاح"}); 
                  else
                  return Json(new{success = false ,message = "حدث خطأ اثناء التعديل"});
            }

         public IActionResult DeleteTenant([FromBody] TenantsDto tenantsDto)
            {
                        
                  var Result = _tenantsService.DeleteTenant(tenantsDto); 

                  if(Result)
                  return Json(new {success = true,message = "تم الحذف بنجاح"}); 
                  else
                  return Json(new{success = false ,message = "حدث خطأ اثناء الحذف"});
            }
   
    }

}       