namespace Occultation.DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlayerShipModel")]
    public partial class PlayerShipModel
    {
        [Key]
        public int ModelId { get; set; }

        [Required]
        [StringLength(25)]
        public string ModelName { get; set; }

        public int PlayerId { get; set; }
    }
}
