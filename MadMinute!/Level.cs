using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadMinute_
{
    class Level
    {
        //Instantiate components
        protected int mult1, mult2, product;
        protected Random rand;

        //Initialize components
        public Level()
        {
            mult1 = 0;
            mult2 = 0;
            product = 0;
            rand = new Random();
        }

        //Return the product
        public int getProduct()
        { return product; }

    }
}
