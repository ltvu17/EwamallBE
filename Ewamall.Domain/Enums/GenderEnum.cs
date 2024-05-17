using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Business.Enums
{
    public enum GenderEnum
    {
        MALE = 0,
        FEMALE = 1,
    }
    public static class Gender
    {
        public static string? ToString(GenderEnum gender)
        {
            if(gender == GenderEnum.MALE)
            {
                return "Male";
            }
            if (gender == GenderEnum.FEMALE)
            {
                return "Female";
            }
            return null;
        }
    }


}
