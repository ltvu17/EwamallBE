using Ewamall.Business.Entities;
using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Business.IRepository
{
    public interface INotificationRepo : IBaseRepo<Notification>
    {
        public Task<IEnumerable<Notification>> GetAllNotificationByUserName(string userName);
    }
}
