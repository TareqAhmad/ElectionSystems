


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

        public IActionResult Edit(int sectionId)
        {
            int? clanId = HttpContext.Session.GetInt32("ClanId");

            var sectionObject = new SectionDto
            {
                SectionId = sectionId,
                ClanId = clanId.Value
            };

            var section = _sectionsService.GetSectionById(sectionObject); 

            var sectionViewModel = new SectionsViewModel
            {
                SectionId = section.SectionId,
                SectionName = section.SectionName,
            };

            return View(sectionViewModel); 
        }


        public IActionResult Delete(int sectionId)
        {
           int? clanId =HttpContext.Session.GetInt32("ClanId"); 

            var sectionObject = new SectionDto
            {
                SectionId = sectionId,
                ClanId = clanId.Value
            };

            var section = _sectionsService.GetSectionById(sectionObject); 

            var sectionViewModel = new SectionsViewModel
            {
                SectionId = section.SectionId,
                SectionName = section.SectionName,
            };

            return View(sectionViewModel); 
        }
       
       public IActionResult GetAllSections()
        {
            int? clanId = HttpContext.Session.GetInt32("ClanId");

            var sectionObject = new SectionDto
            {
                ClanId = clanId.Value,
            };

            var sections = _sectionsService.GetAllSections(sectionObject);

            if(sections == null)
               return Json(new {success = false, message = "لا يوجد بيانات"});

               return Json(new {success = true , data = sections});
        }
      
       public IActionResult GetSectionsById(int sectionId)
        {
                   int? clanId = HttpContext.Session.GetInt32("ClanId");

            var sectionObject = new SectionDto
            {
                ClanId = clanId.Value,
            };

            var sections = _sectionsService.GetSectionById(sectionObject);

            if(sections == null)
               return Json(new {success = false, message = "لا يوجد بيانات"});

               return Json(new {success = true , data = sections});  
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

       public IActionResult UpdateSection([FromBody]SectionDto  sectionDto)
        {
            int? clanId = HttpContext.Session.GetInt32("ClanId"); 

              sectionDto.ClanId = clanId.Value; 
             
              var result  = _sectionsService.UpdateSection(sectionDto); 
             
             if(result)
               return Json(new {success = true,message = "تم التعديل بنجاح"}); 
            else
             return Json(new {success = false ,message = "حدث خطأ اثناء التعديل"});

  
  
    }

       public IActionResult DeleteSection([FromBody] SectionDto sectionDto)
        {
             int? clanId = HttpContext.Session.GetInt32("ClanId"); 

              sectionDto.ClanId = clanId.Value; 
             
            var result  = _sectionsService.DeleteSection(sectionDto); 
             
             if(result)
               return Json(new {success = true,message = "تم الحذف بنجاح"}); 
            else
             return Json(new {success = false ,message = "حدث خطأ اثناء الحذف"});
        }
  
    }
}