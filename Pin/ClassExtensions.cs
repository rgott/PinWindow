using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pin
{
    static class ClassExtensions
    {
        public static bool contains(this string[] strCollection, string str)
        {
            foreach (string item in strCollection)
            {
                if(str.Equals(item))
                    return true;
            }
            return false;
        }
    }
}
