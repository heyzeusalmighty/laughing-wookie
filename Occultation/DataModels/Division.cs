using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Occultation.DataModels
{
    public class Division
    {
        public string Div { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class DivChecker
    {

        public List<Division> Divs { get; set; }

        public DivChecker()
        {
            Divs = new List<Division>
            {
                new Division{ X = 6, Y = 5, Div = "center"},
                //div one
                new Division{ X = 6, Y = 4, Div = "one"},
                new Division{ X = 7, Y = 4, Div = "one"},
                new Division{ X = 7, Y = 5, Div = "one"},
                new Division{ X = 6, Y = 6, Div = "one"},
                new Division{ X = 5, Y = 5, Div = "one"},
                new Division{ X = 5, Y = 4, Div = "one"},
                //div two
                new Division{ X = 6, Y = 3, Div = "two"},
                new Division{ X = 7, Y = 3, Div = "two"},
                new Division{ X = 8, Y = 4, Div = "two"},
                new Division{ X = 8, Y = 5, Div = "two"},
                new Division{ X = 8, Y = 6, Div = "two"},
                new Division{ X = 7, Y = 6, Div = "two"},
                new Division{ X = 6, Y = 7, Div = "two"},
                new Division{ X = 5, Y = 6, Div = "two"},
                new Division{ X = 4, Y = 6, Div = "two"},
                new Division{ X = 4, Y = 5, Div = "two"},
                new Division{ X = 4, Y = 4, Div = "two"},
                new Division{ X = 5, Y = 3, Div = "two"},
                //div three
                new Division{ X = 6, Y = 2, Div = "three"},
                new Division{ X = 7, Y = 2, Div = "three"},
                new Division{ X = 8, Y = 3, Div = "three"},
                new Division{ X = 9, Y = 3, Div = "three"},
                new Division{ X = 9, Y = 4, Div = "three"},
                new Division{ X = 9, Y = 5, Div = "three"},
                new Division{ X = 9, Y = 6, Div = "three"},
                new Division{ X = 8, Y = 7, Div = "three"},
                new Division{ X = 7, Y = 7, Div = "three"},
                new Division{ X = 6, Y = 8, Div = "three"},
                new Division{ X = 5, Y = 7, Div = "three"},
                new Division{ X = 4, Y = 7, Div = "three"},
                new Division{ X = 3, Y = 6, Div = "three"},
                new Division{ X = 3, Y = 5, Div = "three"},
                new Division{ X = 3, Y = 4, Div = "three"},
                new Division{ X = 3, Y = 3, Div = "three"},
                new Division{ X = 4, Y = 3, Div = "three"},
                new Division{ X = 5, Y = 2, Div = "three"},
        
            };
        }



    }
}
