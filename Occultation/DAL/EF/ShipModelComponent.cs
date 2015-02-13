namespace Occultation.DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ShipModelComponent
    {
        [Key]
        public int ComId { get; set; }

        public int? ComponentId { get; set; }

        public int? ShipId { get; set; }
    }
}
