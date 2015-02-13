using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Occultation.DataModels
{
    public class DiceRoll
    {
        public static Random Roll = new Random();
        private string Mocking { get; set; }
        public DiceRoll()
        {
            Mocking = "no";
        }
        
        public int RollTheDice()
        {
            if (Mocking == "hit")
            {
                return 6;
            }
            else if (Mocking == "miss")
            {
                return 1;
            }
            else
            {
                return Roll.Next(1, 7);
            }
        }

        public DiceRoll(string msg)
        {
            Mocking = msg;
        }
    }
}
