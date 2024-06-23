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
    public class DashBoardRepo : BaseRepo<DashBoard>, IDashBoardRepo
    {
        public DashBoardRepo(EwamallDBContext context): base(context)
        {
            
        }
    }
}
