using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck
{
    abstract class Food
    {
        public decimal Price { get; set; }
        public abstract string Description();
    }
}
