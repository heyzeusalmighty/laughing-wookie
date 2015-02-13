namespace Occultation.DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tile
    {
        public int Id { get; set; }

        public int XCoords { get; set; }

        public int YCoords { get; set; }

        public int TileId { get; set; }

        [StringLength(50)]
        public string ControlledBy { get; set; }
    }
}
