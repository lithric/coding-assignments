using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodecademySharp3
{
    class Program
    {
        public class Tools {
            public static bool Contains(string hostValue, string searchArr) {
                return Array.Exists(hostValue.ToCharArray(),elm => searchArr.Contains(elm));
            }
        }
        static void Main(string[] args)
        {
            int minLength = 6;
            string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowercase = "abcdefghijklmnopqrsttuvwxyz";
            string digits = "0123456789";
            string specialChars = "`~!@#$%^&*()-_=+";
            Console.Write("enter a password: ");
            string userPass = Console.ReadLine();
            int score = 0;
            if (userPass.Length >= minLength) {
                score++;
            }
            if (Tools.Contains(userPass, uppercase)) {
                score++;
            }
            if (Tools.Contains(userPass, lowercase)) {
                score++;
            }
            if (Tools.Contains(userPass, digits)) {
                score++;
            }
            if (Tools.Contains(userPass, specialChars)) {
                score++;
            }
            switch (score) {
            case 5:
            case 4:
                Console.WriteLine("extremely strong");
                break;
            case 3:
                Console.WriteLine("strong");
                break;
            case 2:
                Console.WriteLine("medium");
                break;
            case 1:
                Console.WriteLine("weak");
                break;
            default:
                Console.WriteLine("no requirements met");
                break;
            }
            Console.WriteLine(score);
            Console.ReadKey();
            Console.ReadKey();
        }
    }
}
