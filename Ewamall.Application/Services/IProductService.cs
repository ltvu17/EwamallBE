using Ewamall.Domain.Entities;
using Ewamall.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Application.Services
{
    public interface IProductService
    {
        public Task<Result<IEnumerable<Product>>> GetAllProduct();
    }
}
