using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck
{
    class Program
    {
        static void Main(string[] args)
        {
            Taco chicken = new Taco("soft","chicken");
            Console.WriteLine(chicken.Description());

            Hotdog hotdog = new Hotdog("white", "ketchup");
            Console.WriteLine(hotdog.Description());
            Console.ReadKey();
        }
    }
}
