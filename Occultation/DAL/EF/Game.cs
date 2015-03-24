namespace Occultation.DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Game")]
    public partial class Game
    {
        public int GameId { get; set; }

        public int Round { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public int? CurrentPlayer { get; set; }

        [Required]
        [StringLength(50)]
        public string GameIdentifier { get; set; }

        public string Title { get; set; }

    }
}
