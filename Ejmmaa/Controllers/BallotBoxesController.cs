


using Microsoft.AspNetCore.Mvc;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Services.Interfaces;
using Microsoft.AspNetCore.Http.Connections;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Controllers
{

     [SessionCheckFilter]
    public class BallotBoxesController : Controller
    {
           
       private readonly IBallotBoxesService _ballotBoxesService; 

       public BallotBoxesController(IBallotBoxesService ballotBoxesService)
        {
            _ballotBoxesService = ballotBoxesService; 
        }
        public IActionResult Index()
        {
            var ballotBoxes = _ballotBoxesService.GetAllBallotBoxes(); 

            return View(ballotBoxes); 
        }


       public IActionResult Create()
        {
            return View(); 
        }

        public IActionResult Edit(int ballotBoxId)
        {
           // var ballotBox = _ballotBoxesService.GetBallotBoxById(ballotBoxId);
            return View();
        }

        public IActionResult Delete(int ballotBoxId)
        {
            // var ballotBox = _ballotBoxesService.GetBallotBoxById(ballotBoxId);
            return View();
        }


       public IActionResult GetAllBallotBoxes()
        {
            var ballotBoxes = _ballotBoxesService.GetAllBallotBoxes(); 

            if(ballotBoxes == null)
               return Json(new {success = false , message = "لا يوجد بيانات"});


            return Json(new {success  = true , data =  ballotBoxes}); 
            
        }

       [HttpPost]
       public IActionResult AddBallotBox([FromBody]BallotBoxesDto ballotBoxesDto)
        {
           
            var Result = _ballotBoxesService.AddBallotBox(ballotBoxesDto); 

            if(Result)
                return Json(new {success = true,message = "تم الاضافة بنجاح"}); 
            else
                return Json(new{success = false ,message = "حدث خطأ اثناء الاضافة"});
          
        }

         [HttpPost]
         public IActionResult UpdateBallotBox([FromBody]BallotBoxesDto ballotBoxesDto)
        {
            var Result = _ballotBoxesService.UpdateBallotBox(ballotBoxesDto); 

            if(Result)
                return Json(new {success = true,message = "تم التعديل بنجاح"}); 
            else
                return Json(new{success = false ,message = "حدث خطأ اثناء التعديل"});
        }

        
         [HttpPost]
         public IActionResult DeleteBallotBox([FromBody]BallotBoxesDto ballotBoxesDto)
        {
            var Result = _ballotBoxesService.DeleteBallotBox(ballotBoxesDto); 

            if(Result)
                return Json(new {success = true,message = "تم الحذف بنجاح"}); 
            else
                return Json(new{success = false ,message = "حدث خطأ اثناء الحذف"});
        }
        
    }


}