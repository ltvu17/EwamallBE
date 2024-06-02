using Ewamall.Domain.Entities;
using Ewamall.Domain.Primitives;
using Ewamall.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Business.Entities
{
    [Table("Notifications")]
    public class Notification : Entity
    {
        public Notification()
        {  
        }

        internal Notification(string userName, string title, string message, DateTime createdAt, string notificationType, int sender, int roleId)
        {
            Username = userName;
            Title = title;
            Message = message;
            CreatedAt = createdAt;
            NotificationType = notificationType;
            Sender = sender;
            RoleId = roleId;
        }
        public string Username { get; private set; }

        public string Title { get; private set; }
        public string Message { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string NotificationType { get; set; } = null!;
        public int Sender { get; private set; }
        public int RoleId { get; private set; }

        public static Result<Notification> Create(string userName, string title, string message, DateTime createdAt, string notificationType, int sender, int roleId)
        {
            var notification = new Notification(userName, title, message, createdAt, notificationType, sender, roleId);
            return notification;
        }
    }
}
