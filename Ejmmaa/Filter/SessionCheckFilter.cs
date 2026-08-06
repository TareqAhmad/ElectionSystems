using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

public class SessionCheckFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // 1. الوصول إلى الجلسة من خلال السياق (Context)
        var session = context.HttpContext.Session;
        int? userId = session.GetInt32("UserId");
        string? fullName = session.GetString("FullName");
        int? clanId = session.GetInt32("ClanId");

        // 2. التحقق: إذا كانت القيم فارغة (انتهت الجلسة)
        if (userId == null || fullName == null || clanId == null)
        {
            // 🌟 الفحص السحري: هل الطلب الحالي قادم عبر AJAX؟
            bool isAjaxRequest = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (isAjaxRequest)
            {
                // إذا كان طلب جافاسكريبت، نرسل كائن JSON واضح وصريح
                context.Result = new JsonResult(new { 
                    success = false, 
                    isSessionExpired = true, 
                    message = "انتهت صلاحية الجلسة، سيتم تحويلك لصفحة تسجيل الدخول فوراً." 
                });
            }
            else
            {
                // 3. توجيه المستخدم لصفحة تسجيل الدخول أو الرئيسية للطلبات العادية
                context.Result = new RedirectToActionResult("index", "Home", null);
            }
            
            // نوقف التنفيذ هنا ولا نترك المجال للوصول للدوال اللاحقة
            return; 
        }

        base.OnActionExecuting(context);
    }
}