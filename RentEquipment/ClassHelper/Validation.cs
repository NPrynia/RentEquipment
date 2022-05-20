using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RentEquipment.ClassHelper
{
    static public class Validation
    {

        public static bool validationFIO(string a)
        {
            if (string.IsNullOrWhiteSpace(a) == true)
            {
                return false;
            }

            if (!a.All(Char.IsLetter))
            {
                return false;
            }

            if (a.Contains(" "))
            {
                return false;
            }

            return true;

        }

        public static bool validationNum(string a)
        {

            if (ulong.TryParse(a, out ulong ulonga))
            {

            }
            else
            {
                return false;
            }
            return true;
        }
        public static bool validationEmail(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

    }
}
