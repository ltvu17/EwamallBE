using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Business.Entities
{
    public class DashBoard : Entity
    {
        public DashBoard()
        {
            
        }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Year { get; set; }   
    }
}
