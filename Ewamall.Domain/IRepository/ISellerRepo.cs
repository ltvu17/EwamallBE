using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Business.IRepository
{
    public interface ISellerRepo : IBaseRepo<Seller>
    {
        public bool IsEmailExist(string email);
        public bool IsPhoneExist(string phone);
        public Task<IEnumerable<SellerDTO>> GetSellerDTOs();
    }
    public class SellerDTO
    {
        public int id {  get; set; }
        public string ShopName { get; set; }
        public string Address { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string Email { get; set; }
        public string Description { get; set; }
        public float Revenue { get; set; }
    }
}
