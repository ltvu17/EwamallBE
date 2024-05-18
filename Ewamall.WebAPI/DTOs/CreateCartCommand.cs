using Ewamall.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ewamall.WebAPI.DTOs
{
    public class CreateCartCommand
    {
        public int Quantity { get; set; }
        public int UserId { get; set; }
        public int SellDetailId { get; set; }
    }
}
