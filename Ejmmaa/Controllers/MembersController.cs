


using Microsoft.AspNetCore.Mvc;
using Ejmmaa.Models.DTOs;
using Ejmmaa.Services.Interfaces;
using Microsoft.AspNetCore.Http.Connections;
using Ejmmaa.Models.ViewModels;

namespace Ejmmaa.Controllers
{

     [SessionCheckFilter]
    public class MembersController : Controller
    {
           
       private readonly IMembersService _membersService; 

       public MembersController(IMembersService membersService)
        {
            _membersService = membersService; 
        }
        public IActionResult Index()
        {
            int? clanId = HttpContext.Session.GetInt32("ClanId"); 
             
             var memberData = new MemberDto{
                    ClanId = clanId.Value
             }; 

             var Members = _membersService.GetClanMembersData(memberData); 

            return View(Members); 
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



       public IActionResult GetAllMembers()
        {
            int? clanId = HttpContext.Session.GetInt32("ClanId"); 
             
             var memberData = new VotersDto{
                    ClanId = clanId.Value
             }; 
             var Members = _membersService.GetAllVoters(memberData); 

             if (Members == null)
                {
                    return Json(new { success = false, message = "لا توجد بيانات" });
                }

            return Json(new { success = true, data = Members });

        }




       public IActionResult SaveMember([FromBody]MemberDto memberDto)
        {
            int? clanId = HttpContext.Session.GetInt32("ClanId"); 


            memberDto.ClanId = clanId.Value; 
             
            var result  = _membersService.AddMember(memberDto); 
             
             if(result)
               return Json(new {success = true,message = "تم الاضافة بنجاح"}); 
            else
             return Json(new {success = false ,message = "حدث خطأ اثناء الاضافة"});  
        }


    }


}