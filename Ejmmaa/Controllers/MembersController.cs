


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

        public IActionResult Edit(int memberId)
        {
            int? clanId = HttpContext.Session.GetInt32("ClanId"); 
             
             var memberData = new MemberDto{
                    MemberId = memberId,
                    ClanId = clanId.Value
             }; 

             var Member = _membersService.GetMemberById(memberData);

            return View(Member); 
        }

        public IActionResult Delete(int memberId)
        {
            int? clanId = HttpContext.Session.GetInt32("ClanId"); 
             
             var memberData = new MemberDto{
                    MemberId = memberId,
                    ClanId = clanId.Value
             }; 

             var Member = _membersService.GetMemberById(memberData);

            return View(Member); 
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
       public IActionResult GetMemberById(int memberId)
        {
            int? clanId = HttpContext.Session.GetInt32("ClanId"); 
             
             var memberData = new MemberDto{
                    MemberId = memberId,
                    ClanId = clanId.Value
             }; 

             var Member = _membersService.GetMemberById(memberData);

             if (Member == null)
                {
                    return Json(new { success = false, message = "لا توجد بيانات" });
                }

            return Json(new { success = true, data = Member });

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
       public IActionResult UpdateMember([FromBody]MemberDto memberDto)
        {
            int? clanId = HttpContext.Session.GetInt32("ClanId"); 


            memberDto.ClanId = clanId.Value; 
             
            var result  = _membersService.UpdateMember(memberDto); 
             
             if(result)
               return Json(new {success = true,message = "تم التحديث بنجاح"}); 
            else
             return Json(new {success = false ,message = "حدث خطأ اثناء التحديث"});  
        }
       public IActionResult DeleteMember([FromBody]MemberDto memberDto)
        {
            int? clanId = HttpContext.Session.GetInt32("ClanId");

            memberDto.ClanId = clanId.Value;

            var result = _membersService.DeleteMember(memberDto);

            if (result)
                return Json(new { success = true, message = "تم الحذف بنجاح" });
            else
                return Json(new { success = false, message = "حدث خطأ اثناء الحذف" });
        }

    }


}