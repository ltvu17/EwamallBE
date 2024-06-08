using Ewamall.Business.Entities;
using Ewamall.Business.IRepository;
using Ewamall.Infrastructure.Dbcontext;
using Ewamall.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Notification>> GetAllNotificationByUserName(string userName)
        {
            var notifications = await _context.Notifications
        .Where(s => s.Username == userName || s.NotificationType == "All")
        .ToListAsync();
            notifications.Reverse();
            return notifications;
        }
    }
}
