namespace Ewamall.WebAPI.DTOs
{
    public class CreateNotification
    {
        public string Title { get; private set; }
        public string Message { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string NotificationType { get; set; } = null!;
        public int Sender { get; private set; }
        public int Receiver { get; private set; }
        public int RoleId { get; set; }
    }
}
