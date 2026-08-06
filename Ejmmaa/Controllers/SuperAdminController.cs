
using Microsoft.AspNetCore.Mvc;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Services.Interfaces;


namespace Ejmmaa.Controllers
{
    public class SuperAdminController : Controller
    {

       private readonly ISuperAdminService _superAdminService; 

       public SuperAdminController(ISuperAdminService superAdminService)
        {
            _superAdminService = superAdminService; 
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }


         [HttpPost]
        public IActionResult Login([FromBody]LoginRequest loginRequest)
        {
            if (loginRequest.UserName == null || loginRequest.Password == null)
            {
               return  Json(new { success = false, message = "اسم المستخدم أو كلمة المرور غير صحيحة." });
            }
              
           
            var userInfo = _superAdminService.Login(loginRequest);
           
           if (userInfo == null)
            {
                return Json(new { success = false, message = "اسم المستخدم أو كلمة المرور غير صحيحة." });
            }

            return Json(new { success = true, data = userInfo, message = "تم تسجيل الدخول بنجاح." });
       
        }

    }
    
}