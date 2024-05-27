using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Business.Entities
{
    public class Notification : Entity
    {
        internal Notification()
        {
            
        }
        public string Title { get; private set; }
        public string Message { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public int Sender { get; private set; }
        public int Receiver { get; private set; }
    }
}
