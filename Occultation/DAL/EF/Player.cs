namespace Occultation.DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Player")]
    public partial class Player
    {
        public int PlayerId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        public int GameId { get; set; }

        [Required]
        [StringLength(12)]
        public string DiscColor { get; set; }

        public int CurrentOrange { get; set; }

        public int CurrentBrown { get; set; }

        public int CurrentPink { get; set; }

        public int OrangeIncome { get; set; }

        public int BrownIncome { get; set; }

        public int PinkIncome { get; set; }

        public bool Pass { get; set; }

        public int TurnOrder { get; set; }

        public int TotalDiscs { get; set; }

        public int AvailableDiscs { get; set; }

        public int UserId { get; set; }
    }
}
