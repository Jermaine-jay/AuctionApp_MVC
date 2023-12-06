namespace AunctionApp.DAL.Entities
{
    public class Admin : BaseEntity
    {
        public int AdminId { get; set; }
        public string AdminUserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
