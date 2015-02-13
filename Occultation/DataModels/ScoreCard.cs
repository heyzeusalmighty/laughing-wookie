using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Occultation.DataModels
{
    public class ScoreCard
    {
        public int Black { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public int Red { get; set; }
        public int Yellow { get; set; }
        public int White { get; set; }

        public int Turn { get; set; }
    }
}
