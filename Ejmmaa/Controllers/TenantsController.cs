
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
          public IActionResult Edit()
          {
                return View(); 
          }
         public IActionResult Delete()
          {
                return View(); 
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


    }

}       