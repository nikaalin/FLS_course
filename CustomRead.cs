using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NoteBookConsole
{
    public static class CustomRead
    {
        public static string ReadNullSafeString(string welcomeText)
        {
            string s = null;
            while (string.IsNullOrEmpty(s))
            {
                Console.WriteLine(welcomeText);
                s = Console.ReadLine();
            }
            return s;
        }

        public static string ReadNumber(string welcomeText)
        {
            Console.WriteLine(welcomeText);
            var reg = new Regex(@"[\d]");

            string num = Console.ReadLine();
            while (!reg.IsMatch(num))
            {
                Console.WriteLine("Incorrect number. Try again");
                num = Console.ReadLine();
            }

            return num;
        }

        public static DateTime ReadDate(string welcomeText)
        {
            Console.WriteLine(welcomeText);
            DateTime bth = new DateTime();
            string bthStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(bthStr))
            {
                while (!DateTime.TryParse(bthStr, out bth))
                {
                    Console.WriteLine("Incorrect date format. Try again");
                }
            }

            return bth;
        }

        public static string ReadString(string welcomeText)
        {
            Console.WriteLine(welcomeText);
            return Console.ReadLine();
        }

    }
}
