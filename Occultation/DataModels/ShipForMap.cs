﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Occultation.DataModels
{
    public class ShipForMap
    {
        public int ShipId { get; set; }
        public int? XCoords { get; set; }
        public int? YCoords { get; set; }
        public string Color { get; set; }
        public string ShipType { get; set; }
    }
}
