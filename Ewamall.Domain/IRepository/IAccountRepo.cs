using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Business.IRepository
{
    public interface IAccountRepo : IBaseRepo<Account>
    {
        public bool IsEmailExist(string email);
        public bool ConfirmAccount(string email);
    }
}
