namespace Occultation.DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlayerTrack")]
    public partial class PlayerTrack
    {
        [Key]
        public int TrackId { get; set; }

        public int PlayerId { get; set; }

        [Required]
        [StringLength(25)]
        public string Track { get; set; }

        public int TileId { get; set; }

        public int Position { get; set; }
    }
}
