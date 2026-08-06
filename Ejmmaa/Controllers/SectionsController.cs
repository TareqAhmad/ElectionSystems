


using Microsoft.AspNetCore.Mvc;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Services.Interfaces;
using Microsoft.AspNetCore.Http.Connections;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Controllers
{
 
     [SessionCheckFilter]
    public class SectionsController : Controller
    {
            
        private readonly ISectionsService _sectionsService; 

        public SectionsController(ISectionsService sectionsService)
        {
            _sectionsService  = sectionsService;
        }
        public IActionResult Index()
        {
            int? clanId = HttpContext.Session.GetInt32("ClanId");

            var sectionObject = new SectionDto
            {
                ClanId = clanId.Value,
            };

            var sections = _sectionsService.GetAllSections(sectionObject);
            
            return View(sections); 
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
       
       public IActionResult SaveSection([FromBody]SectionDto  sectionDto)
        {
            int? clanId = HttpContext.Session.GetInt32("ClanId"); 

            sectionDto.ClanId = clanId.Value; 
             
            var result  = _sectionsService.AddSection(sectionDto); 
             
             if(result)
               return Json(new {success = true,message = "تم الاضافة بنجاح"}); 
            else
             return Json(new {success = false ,message = "حدث خطأ اثناء الاضافة"});  
        }

    }


}