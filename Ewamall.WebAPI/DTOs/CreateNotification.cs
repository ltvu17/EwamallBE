namespace Ewamall.WebAPI.DTOs
{
    public class CreateNotification
    {
        public string Username { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public string NotificationType { get; set; } = null!;
        public int Sender { get; set; }
        public int RoleId { get; set; }
    }
}
