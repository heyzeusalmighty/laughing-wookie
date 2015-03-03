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
        public int Div { get; set; }
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
                new Division{ X = 6, Y = 5, Div = 0},
                //div one
                new Division{ X = 6, Y = 4, Div = 1},
                new Division{ X = 7, Y = 4, Div = 1},
                new Division{ X = 7, Y = 5, Div = 1},
                new Division{ X = 6, Y = 6, Div = 1},
                new Division{ X = 5, Y = 5, Div = 1},
                new Division{ X = 5, Y = 4, Div = 1},
                //div two
                new Division{ X = 6, Y = 3, Div = 2},
                new Division{ X = 7, Y = 3, Div = 2},
                new Division{ X = 8, Y = 4, Div = 2},
                new Division{ X = 8, Y = 5, Div = 2},
                new Division{ X = 8, Y = 6, Div = 2},
                new Division{ X = 7, Y = 6, Div = 2},
                new Division{ X = 6, Y = 7, Div = 2},
                new Division{ X = 5, Y = 6, Div = 2},
                new Division{ X = 4, Y = 6, Div = 2},
                new Division{ X = 4, Y = 5, Div = 2},
                new Division{ X = 4, Y = 4, Div = 2},
                new Division{ X = 5, Y = 3, Div = 2},
                //div three
                new Division{ X = 6, Y = 2, Div = 3},
                new Division{ X = 7, Y = 2, Div = 3},
                new Division{ X = 8, Y = 3, Div = 3},
                new Division{ X = 9, Y = 3, Div = 3},
                new Division{ X = 9, Y = 4, Div = 3},
                new Division{ X = 9, Y = 5, Div = 3},
                new Division{ X = 9, Y = 6, Div = 3},
                new Division{ X = 8, Y = 7, Div = 3},
                new Division{ X = 7, Y = 7, Div = 3},
                new Division{ X = 6, Y = 8, Div = 3},
                new Division{ X = 5, Y = 7, Div = 3},
                new Division{ X = 4, Y = 7, Div = 3},
                new Division{ X = 3, Y = 6, Div = 3},
                new Division{ X = 3, Y = 5, Div = 3},
                new Division{ X = 3, Y = 4, Div = 3},
                new Division{ X = 3, Y = 3, Div = 3},
                new Division{ X = 4, Y = 3, Div = 3},
                new Division{ X = 5, Y = 2, Div = 3},
        
            };
        }



    }
}
