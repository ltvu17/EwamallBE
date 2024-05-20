using Ewamall.Business.IRepository;
using Ewamall.Domain.Entities;
using Ewamall.Infrastructure.Dbcontext;
using Ewamall.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.DataAccess.Repository
{
    public class OrderRepo : BaseRepo<Order>, IOrderRepo
    {
        private readonly EwamallDBContext _context;
        public OrderRepo(EwamallDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
