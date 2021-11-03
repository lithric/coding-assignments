using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck
{
    class Taco : Food, IMexican
    {
        public Taco(string shell,string meat)
        {
            Shell = shell;
            Meat = meat;
        }
        public string Shell { get; set; }
        public string Meat { get; set; }
        public double Price { get; } = 4.25;
        public override string Description()
        {
            return $"this taco has a {Shell} shell, with {Meat} meat. This costs {Price}";
        }
    }
}
