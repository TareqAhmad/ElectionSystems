


using Microsoft.AspNetCore.Mvc;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Services.Interfaces;
using Microsoft.AspNetCore.Http.Connections;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Controllers
{

     [SessionCheckFilter]
    public class RegionsController : Controller
    {
           
       private readonly IRegionsService _regionsService; 

       public RegionsController( IRegionsService regionsService)
        {
            _regionsService = regionsService; 
        }
        public IActionResult Index()
        {
            var regions = _regionsService.GetAllRegions(); 

            return View(regions); 
        }


       public IActionResult Create()
        {
            return View(); 
        }

        public IActionResult Edit(int RegionId)
        {
            var regionObject = new RegionDto {RegionId = RegionId}; 
            
            var region = _regionsService.GetRegionById(regionObject);
           
           return View(region);
        }

        public IActionResult Delete(int RegionId)
        {
            var regionObject = new RegionDto {RegionId = RegionId}; 

            var region = _regionsService.GetRegionById(regionObject);
           
           return View(region);
        }
    
       [HttpGet]
       public IActionResult GetAllRegions()
        {
              var regions = _regionsService.GetAllRegions(); 
    
                if (regions == null)
                {
                    return Json(new { success = false, message = "لا توجد بيانات" });
                }

                // إرجاعها بنفس النمط (success و data)
                return Json(new { success = true, data = regions });
        }
     
      public IActionResult GetRegionById(int RegionId)
        {
            var regionObject  = new RegionDto
            {
                RegionId = RegionId
            };

            var region = _regionsService.GetRegionById(regionObject);

            if (region == null)
            {
                return Json(new { success = false, message = "لا توجد بيانات" });
            }

            return Json(new { success = true, data = region });
        }
       public IActionResult AddRegion([FromBody]RegionDto regionDto)
        {
             var Result = _regionsService.AddRegion(regionDto); 

            if(Result)
                return Json(new {success = true,message = "تم الاضافة بنجاح"}); 
            else
                return Json(new{success = false ,message = "حدث خطأ اثناء الاضافة"});
          
        }
       public IActionResult UpdateRegion([FromBody]RegionDto regionDto)
        {
             var Result = _regionsService.UpdateRegion(regionDto); 

            if(Result)
                return Json(new {success = true,message = "تم التعديل بنجاح"}); 
            else
                return Json(new{success = false ,message = "حدث خطأ اثناء التعديل"});
          
        }
       
       public IActionResult DeleteRegion([FromBody]RegionDto regionDto)
        {
             var Result = _regionsService.DeleteRegion(regionDto); 

            if(Result)
                return Json(new {success = true,message = "تم الحذف بنجاح"}); 
            else
                return Json(new{success = false ,message = "حدث خطأ اثناء الحذف"});
          
        }
       


    }


}