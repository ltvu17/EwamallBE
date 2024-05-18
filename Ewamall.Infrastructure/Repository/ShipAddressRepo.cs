using Ewamall.Business.IRepository;
using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using Ewamall.Infrastructure.Dbcontext;
using Ewamall.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.DataAccess.Repository
{
    public class ShipAddressRepo : BaseRepo<ShipAddress>, IShipAddressRepo
    {
        private readonly EwamallDBContext _context;
        public ShipAddressRepo(EwamallDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
