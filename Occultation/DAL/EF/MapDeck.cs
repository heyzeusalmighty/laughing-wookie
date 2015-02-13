namespace Occultation.DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MapDeck")]
    public partial class MapDeck
    {
        public int MapDeckId { get; set; }

        public int MapId { get; set; }

        public int Division { get; set; }

        public int? SortOrder { get; set; }

        public bool Revealed { get; set; }

        public int? XCoords { get; set; }

        public int? YCoords { get; set; }

        [StringLength(25)]
        public string Occupied { get; set; }

        public int GameId { get; set; }
    }
}
