


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
            var pollingStation = _pollingStationsService.GetPollingStationById(pollingStationId);

            return View(pollingStation); 
        }

        public IActionResult Delete(int pollingStationId)
        { 
            var pollingStations = _pollingStationsService.GetPollingStationById(pollingStationId);
            
            return View(pollingStations); 
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

        
       public IActionResult AddPollingStation([FromBody]PollingStationsDto pollingStationsDto)
        {
           
            var Result = _pollingStationsService.AddPollingStation(pollingStationsDto); 

            if(Result)
                return Json(new {success = true,message = "تم الاضافة بنجاح"}); 
            else
                return Json(new{success = false ,message = "حدث خطأ اثناء الاضافة"});
          
        }
       





    }


}