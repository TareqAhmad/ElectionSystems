

namespace Ejmmaa.Models.Entities
{
    public class SystemUsers 
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int RoleID { get; set; }
        public int? ClanID { get; set; }
        public bool IsActive { get; set; }
    }
}