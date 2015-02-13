using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Occultation.DataModels;

namespace Occultation.DataModels
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DiscColor Color { get; set; }
        public int CurrentOrange { get; set; }
        public int CurrentBrown { get; set; }
        public int CurrentPink { get; set; }
        public Income OrangeIncome { get; set; }
        public Income BrownIncome { get; set; }
        public Income PinkIncome { get; set; }
        public bool Pass { get; set; }
        public int TurnOrder { get; set; }
        public int TotalDiscs { get; set; }
        public int AvailableDiscs { get; set; }


        public void TakeTurn(string action)
        {
            switch (action)
            {
                case "explore":
                    ExploreInit();
                    break;
                default:
                    break;
            }
        }



        public void ExploreInit()
        {
            
        }

}

    public enum DiscColor
    {
        Black,
        Green,
        Blue,
        Red,
        Yellow,
        White
    }
}
