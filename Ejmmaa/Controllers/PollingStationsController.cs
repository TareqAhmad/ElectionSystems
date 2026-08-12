


using Microsoft.AspNetCore.Mvc;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Services.Interfaces;
using Microsoft.AspNetCore.Http.Connections;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Controllers
{

     [SessionCheckFilter]
    public class PollingStationsController : Controller
    {
           
       private readonly IPollingStationsService _pollingStationsService; 

       public PollingStationsController( IPollingStationsService pollingStationsService)
        {
            _pollingStationsService = pollingStationsService; 
        }
        public IActionResult Index()
        {
            var pollingStations = _pollingStationsService.GetAllPollingStations(); 

            return View(pollingStations); 
        }

        public IActionResult Create()
        {
            return View(); 
        }

        public IActionResult Edit(int pollingStationId)
        {
            var pollingStationObject = new PollingStationsDto { StationId = pollingStationId };

            var pollingStation = _pollingStationsService.GetPollingStationById(pollingStationObject);

            return View(pollingStation); 
        }

        public IActionResult Delete(int pollingStationId)
        { 
            var pollingStationObject = new PollingStationsDto { StationId = pollingStationId };

            var pollingStation = _pollingStationsService.GetPollingStationById(pollingStationObject);

            return View(pollingStation); 
        }

       [HttpGet]
       public IActionResult GetAllPollingStations()
        {
              var pollingStations = _pollingStationsService.GetAllPollingStations(); 
    
                if (pollingStations == null)
                {
                    return Json(new { success = false, message = "لا توجد بيانات" });
                }
                
                var Pollings =  pollingStations.Select(p => new {
                     stationId = p.StationId,
                     stationName = p.StationName
                }).ToList();
                
                // إرجاعها بنفس النمط (success و data)
                return Json(new { success = true, data = Pollings });
        }
       public IActionResult GetPollingStationById(int pollingStationId)
        {
            var pollingStationObject = new PollingStationsDto { StationId = pollingStationId };
            
            var pollingStation = _pollingStationsService.GetPollingStationById(pollingStationObject);

            if (pollingStation == null)
            {
                return Json(new { success = false, message = "لا توجد بيانات" });
            }

            return Json(new { success = true, data = pollingStation });
        }
       public IActionResult AddPollingStation([FromBody]PollingStationsDto pollingStationsDto)
        {
           
            var Result = _pollingStationsService.AddPollingStation(pollingStationsDto); 

            if(Result)
                return Json(new {success = true,message = "تم الاضافة بنجاح"}); 
            else
                return Json(new{success = false ,message = "حدث خطأ اثناء الاضافة"});
          
        }
       public IActionResult UpdatePollingStation([FromBody]PollingStationsDto pollingStationsDto)
        {
           
            var Result = _pollingStationsService.UpdatePollingStation(pollingStationsDto); 

            if(Result)
                return Json(new {success = true,message = "تم التعديل بنجاح"}); 
            else
                return Json(new{success = false ,message = "حدث خطأ اثناء التعديل"});
          
        }      
       public IActionResult DeletePollingStation([FromBody]PollingStationsDto pollingStationsDto)
        {
           
            var Result = _pollingStationsService.DeletePollingStation(pollingStationsDto); 

            if(Result)
                return Json(new {success = true,message = "تم الحذف بنجاح"}); 
            else
                return Json(new{success = false ,message = "حدث خطأ اثناء الحذف"});
          
        }




    }


}