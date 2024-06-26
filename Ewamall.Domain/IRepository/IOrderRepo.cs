﻿using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Business.IRepository
{
    public interface IOrderRepo: IBaseRepo<Order>
    {
        public Task<IEnumerable<Order>> GetOrderByUserId(int userId);
        public Task<IEnumerable<Order>> GetOrderBySellerId(int sellerId);
    }
}
