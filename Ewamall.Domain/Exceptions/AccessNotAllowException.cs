using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Business.Exceptions
{
    public class AccessNotAllowException : Exception
    {
        public AccessNotAllowException(string message) : base(message)
        { }
    }
}
