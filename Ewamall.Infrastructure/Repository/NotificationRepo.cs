using Ewamall.Business.Entities;
using Ewamall.Business.IRepository;
using Ewamall.Infrastructure.Dbcontext;
using Ewamall.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.DataAccess.Repository
{
    public class NotificationRepo : BaseRepo<Notification>, INotificationRepo
    {
        private readonly EwamallDBContext _context;
        public NotificationRepo(EwamallDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
