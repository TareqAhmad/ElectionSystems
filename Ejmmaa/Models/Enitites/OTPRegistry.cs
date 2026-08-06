namespace Ejmmaa.Models.Entities
{
public class OTPRegistry { public int OTPID { get; set; } public int MemberID { get; set; } public string OTP_Code { get; set; } public DateTime SentAt { get; set; } public DateTime ExpiresAt { get; set; } public bool IsUsed { get; set; } }
}
