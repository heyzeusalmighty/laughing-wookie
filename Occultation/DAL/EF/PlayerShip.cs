

namespace Occultation.DAL.EF
{
    using System.ComponentModel.DataAnnotations;

    public partial class PlayerShip
    {
        [Key]
        public int PlayerShipId { get; set; }
        public int ModelId { get; set; }
        public int PlayerId { get; set; }
        public int? XCoords { get; set; }
        public int? YCoords { get; set; }

    }
}

