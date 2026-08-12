
using Microsoft.AspNetCore.Mvc;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Services.Interfaces;

namespace Ejmmaa.Controllers
{
    public class SupervisorsController : Controller
    {
       private readonly ISupervisorsService _supervisorsService; 

       public SupervisorsController(ISupervisorsService supervisorsService)
        {
            _supervisorsService = supervisorsService; 
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Panel()
        {
            return View();
        }

        public IActionResult Manage()
        {
            var supervisors = _supervisorsService.GetAllElectionSupervisors();

            return View(supervisors);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int supervisorId)
        {
             var supervisorObject = new ElectionSupervisorsDto {SupervisorId = supervisorId};
             
             var supervisor = _supervisorsService.GetSupervisorById(supervisorObject);

             return View(supervisor);
        }

        public IActionResult Delete(int supervisorId)
        {

             var supervisorObject = new ElectionSupervisorsDto {SupervisorId = supervisorId};
             
             var supervisor = _supervisorsService.GetSupervisorById(supervisorObject);

            return View(supervisor);
        }
        

        [HttpPost]
        public IActionResult Login([FromBody]LoginRequest loginRequest)
        {
            if (loginRequest.UserName == null || loginRequest.Password == null)
            {
               return  Json(new { success = false, message = "اسم المستخدم أو كلمة المرور غير صحيحة." });
            }
              
           
            var userInfo = _supervisorsService.Login(loginRequest);
           
           if (userInfo == null)
            {
                return Json(new { success = false, message = "اسم المستخدم أو كلمة المرور غير صحيحة." });
            }

            return Json(new { success = true, data = userInfo, message = "تم تسجيل الدخول بنجاح." });
       
        }



        public IActionResult AddSupervisor([FromBody]ElectionSupervisorsDto electionSupervisorsDto)
        {
            
             var Result = _supervisorsService.AddSupervisor(electionSupervisorsDto); 

            if(Result)
                return Json(new {success = true,message = "تم الاضافة بنجاح"}); 
            else
                return Json(new{success = false ,message = "حدث خطأ اثناء الاضافة"});
            
        }

        public IActionResult UpdateSupervisor([FromBody]ElectionSupervisorsDto electionSupervisorsDto)
        {
            
             var Result = _supervisorsService.UpdateSupervisor(electionSupervisorsDto); 

            if(Result)
                return Json(new {success = true,message = "تم التحديث بنجاح"}); 
            else
                return Json(new{success = false ,message = "حدث خطأ اثناء التحديث"});
            
        }
        public IActionResult DeleteSupervisor([FromBody] ElectionSupervisorsDto electionSupervisorsDto)
        {
                        
             var Result = _supervisorsService.DeleteSupervisor(electionSupervisorsDto); 

            if(Result)
                return Json(new {success = true,message = "تم الحذف بنجاح"}); 
            else
                return Json(new{success = false ,message = "حدث خطأ اثناء الحذف"});
            
        }
   
    }
    
}