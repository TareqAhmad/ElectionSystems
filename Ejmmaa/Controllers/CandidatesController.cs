using Microsoft.AspNetCore.Mvc;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Services.Interfaces;
using Microsoft.AspNetCore.Http.Connections;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Controllers
{
 
     [SessionCheckFilter]
    public class CandidatesController : Controller
    {

        private readonly ICandidatesService _candidatesService; 

        public CandidatesController(ICandidatesService candidatesService)
        {
            _candidatesService = candidatesService; 
        }
        public IActionResult Index()
        {
           var candidatesObject = new CandidatesDto
           {
               ElectionId = 1
           }; 

           var candidates = _candidatesService.GetAllCandidates(candidatesObject);
           
            return View(candidates); 
        }


       public IActionResult Create()
        {
            return View(); 
        }

        public IActionResult Edit(int CandidateId)
        {
            Console.WriteLine($"CandidateId: {CandidateId}"); // Debugging line
            var candidate = _candidatesService.GetCandidateById(CandidateId);

            return View(candidate); 
        }

        public IActionResult Delete(int CandidateId)
        {
            Console.WriteLine($"CandidateId: {CandidateId}"); // Debugging line
           
           var candidate = _candidatesService.GetCandidateById(CandidateId);

            return View(candidate); 
        }

       public IActionResult GetAllCandidates()
        {
            var candidatesObject = new CandidatesDto
           {
               ElectionId = 1
           }; 

           var candidates = _candidatesService.GetAllCandidates(candidatesObject);
           
           if(candidates == null || candidates.Count == 0)
                return Json(new {success = false,message = "لا يوجد مرشحين"});

            return Json(new {success = true,data = candidates}); 
        }
        
        public IActionResult AddCandidate([FromBody] CandidatesDto candidatesDto)
        {
           var Result = _candidatesService.AddCandidate(candidatesDto); 

            if(Result)
                return Json(new {success = true,message = "تم الاضافة بنجاح"}); 
            else
                return Json(new{success = false ,message = "حدث خطأ اثناء الاضافة"});
          
        }
       
    }

}
