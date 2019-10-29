using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace uadec.BusinessLogic
{
    public static class StudentsManager
    {
        public static bool GetSuperError(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Extension method to compare two strings, null friendly, case insensitive and ignore accents
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public static bool IsEqualTo(this string str1, string str2)
        {
            if (str1 == null)
            {
                return false;
            }

            str1 = str1.Trim();
            str2 = str2.Trim();
            
            return String.Compare(str1, str2, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0;            
        }
    }
}
