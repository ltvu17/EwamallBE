using Ewamall.Domain.Primitives;
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
        public string Title { get; private set; }
        public string Message { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string NotificationType { get; set; } = null!;
        public int Sender { get; private set; }
        public int Receiver { get; private set; }
        public int RoleId { get; set; }
    }
}
