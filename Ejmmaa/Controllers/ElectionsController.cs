


using Microsoft.AspNetCore.Mvc;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Services.Interfaces;
using Microsoft.AspNetCore.Http.Connections;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Controllers
{
 
     [SessionCheckFilter]
    public class ElectionsController : Controller
    {
        private readonly IElectionsService _electionsService; 

        public ElectionsController(IElectionsService electionsService)
        {
            _electionsService  = electionsService;
        }
        public IActionResult Index()
        {
            int? clanId = HttpContext.Session.GetInt32("ClanId"); 

            var electionData = new ElectionDto
            {
                ClanID = clanId.Value
            }; 

            var elections = _electionsService.GetAllElections(electionData); 
            
            return View(elections); 
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
       

        public IActionResult GetAllElections()
        {
            int? clanId = HttpContext.Session.GetInt32("ClanId"); 

            var electionData = new ElectionDto
            {
                ClanID = clanId.Value
            }; 

            var elections = _electionsService.GetAllElections(electionData); 
             
             if (elections == null)
                {
                    return Json(new { success = false, message = "لا توجد بيانات" });
                }

                var Elections =  elections.Select(e => new {
                     electionId = e.ElectionId,
                     electionTitle = e.ElectionTitle
                }).ToList();

               return Json(new { success = true, data = Elections });


        }

       public IActionResult AddElection([FromBody]ElectionDto electionDto)
        {
            int? clanId = HttpContext.Session.GetInt32("ClanId"); 

             electionDto.ClanID = clanId.Value; 
             
             var Result = _electionsService.AddElection(electionDto); 

            if(Result)
                return Json(new {success = true,message = "تم الاضافة بنجاح"}); 
            else
                return Json(new{success = false ,message = "حدث خطأ اثناء الاضافة"});
          
        }


    }


}