using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck
{
    class Hotdog : Food
    {
        public Hotdog(string bun, string topping)
        {
            Topping = topping;
            Bun = bun;
        }
        public string Topping { get; set; }
        public string Bun { get; set; }
        public double Price { get; } = 2.5;
        public override string Description()
        {
            return $"This hotdog has a {Bun} bun, with {Topping}. This costs {Price}";
        }
    }
}
