
using Microsoft.AspNetCore.Mvc;
using Ejmmaa.Services.Interfaces;
using Ejmmaa.Models.DTOs;


namespace Ejmmaa.Controllers
{

    public class VotersController : Controller
    {

     private readonly IVotersService _votersService; 
     private readonly IMembersService _membersService; 

       public VotersController(IVotersService votersService,IMembersService membersService)
        {
            _votersService = votersService; 
            _membersService = membersService;
        }
        
        public IActionResult Index()
        {

            return View();
        }

      [SessionCheckFilter]     
      public IActionResult Show()
        {
            
            int? ClanId = HttpContext.Session.GetInt32("ClanId"); 

            var voters = new VotersDto
            {
                ClanId = ClanId.Value
            }; 

            var Voters = _membersService.GetAllVoters(voters);
            
            return View(Voters);
        }
      
        public IActionResult VoterBallot()
        {
            return View();
        }

        public IActionResult VoterBallot2()
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
              
           
            var userInfo = _votersService.Login(loginRequest);
           
           if (userInfo == null)
            {
                return Json(new { success = false, message = "اسم المستخدم أو كلمة المرور غير صحيحة." });
            }

            HttpContext.Session.SetInt32("UserId", userInfo.UserID);
            HttpContext.Session.SetInt32("MemberId",userInfo.MemberId);
            HttpContext.Session.SetString("FullName", userInfo.FullName);
            HttpContext.Session.SetInt32("TenantId",userInfo.TenantId); 
            HttpContext.Session.SetInt32("ClanId", userInfo.ClanId);
            HttpContext.Session.SetInt32("ElectionId",userInfo.ElectionId); 


            return Json(new { success = true, data = userInfo, message = "تم تسجيل الدخول بنجاح." });
       
        }

       [SessionCheckFilter]
       [HttpPost]
       public IActionResult GetAllVoters()
        {

            int? ClanId = HttpContext.Session.GetInt32("ClanId"); 

            var voters = new VotersDto
            {
                ClanId = ClanId.Value
            }; 

            var Voters = _membersService.GetAllVoters(voters);

            return  Json(Voters); 
        }
    
    
       [SessionCheckFilter]
       [HttpPost]
       public IActionResult SubmitVote(VotingRegistryDto votingRegistryDto)
        {    
             int? MemberId = HttpContext.Session.GetInt32("MemberId"); 
             int? ElectionId = HttpContext.Session.GetInt32("ElectionId"); 

             votingRegistryDto.MemberId = MemberId.Value;
             votingRegistryDto.ElectionId = ElectionId.Value; 
             votingRegistryDto.BoxId = 1; 

             var result = _votersService.SubmitVote(votingRegistryDto); 

             if(result)
               return Json(new {success = true,message = "تم تسجيل وصوتك بنجاح وسرية تامّة!"}); 
            else
              return Json(new {success = false ,message = "حدث خطأ اثناء التصويت"});  

        }
    }
    
}