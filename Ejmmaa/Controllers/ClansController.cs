


using Microsoft.AspNetCore.Mvc;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Services.Interfaces;
using Microsoft.AspNetCore.Http.Connections;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Controllers
{
 
    [SessionCheckFilter]
    public class ClansController : Controller
    {
            
        private readonly IClansService _clansService; 

        public ClansController(IClansService clansService)
        {
            _clansService  = clansService;
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
       
       public IActionResult SaveClan([FromBody]ClanDto  clanDto)
        {
  
            var result  = _clansService.AddClan(clanDto); 
             
             if(result)
               return Json(new {success = true,message = "تم الاضافة بنجاح"}); 
            else
             return Json(new {success = false ,message = "حدث خطأ اثناء الاضافة"});  
        }

    }


}