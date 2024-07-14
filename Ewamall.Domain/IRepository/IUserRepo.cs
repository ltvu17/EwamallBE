using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Business.IRepository
{
    public interface IUserRepo : IBaseRepo<User>
    {
        public bool IsUserExist(int userId);
        public Task<Seller> GetSellerByProduct(int productDetailId);
    }
}
