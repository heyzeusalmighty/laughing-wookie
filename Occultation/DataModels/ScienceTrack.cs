using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Occultation.DataModels
{
    public class ScienceTrack
    {
        public List<ScienceTile> StarTiles { get; set; }
        public List<ScienceTile> GridTiles { get; set; }
        public List<ScienceTile> GearTiles { get; set; }

        public ScienceTrack()
        {
            StarTiles = new List<ScienceTile>
            {
                new ScienceTile {Position = 1, CostReduction = 0, Track = Track.Star, VictoryPoints = 0},
                new ScienceTile {Position = 2, CostReduction = 1, Track = Track.Star, VictoryPoints = 0},
                new ScienceTile {Position = 3, CostReduction = 2, Track = Track.Star, VictoryPoints = 0},
                new ScienceTile {Position = 4, CostReduction = 3, Track = Track.Star, VictoryPoints = 1},
                new ScienceTile {Position = 5, CostReduction = 4, Track = Track.Star, VictoryPoints = 2},
                new ScienceTile {Position = 6, CostReduction = 6, Track = Track.Star, VictoryPoints = 3},
                new ScienceTile {Position = 7, CostReduction = 8, Track = Track.Star, VictoryPoints = 5}
            };

            GridTiles = new List<ScienceTile>
            {
                new ScienceTile {Position = 1, CostReduction = 0, Track = Track.Grid, VictoryPoints = 0},
                new ScienceTile {Position = 2, CostReduction = 1, Track = Track.Grid, VictoryPoints = 0},
                new ScienceTile {Position = 3, CostReduction = 2, Track = Track.Grid, VictoryPoints = 0},
                new ScienceTile {Position = 4, CostReduction = 3, Track = Track.Grid, VictoryPoints = 1},
                new ScienceTile {Position = 5, CostReduction = 4, Track = Track.Grid, VictoryPoints = 2},
                new ScienceTile {Position = 6, CostReduction = 6, Track = Track.Grid, VictoryPoints = 3},
                new ScienceTile {Position = 7, CostReduction = 8, Track = Track.Grid, VictoryPoints = 5}
            };

            GearTiles = new List<ScienceTile>
            {
                new ScienceTile {Position = 1, CostReduction = 0, Track = Track.Gear, VictoryPoints = 0},
                new ScienceTile {Position = 2, CostReduction = 1, Track = Track.Gear, VictoryPoints = 0},
                new ScienceTile {Position = 3, CostReduction = 2, Track = Track.Gear, VictoryPoints = 0},
                new ScienceTile {Position = 4, CostReduction = 3, Track = Track.Gear, VictoryPoints = 1},
                new ScienceTile {Position = 5, CostReduction = 4, Track = Track.Gear, VictoryPoints = 2},
                new ScienceTile {Position = 6, CostReduction = 6, Track = Track.Gear, VictoryPoints = 3},
                new ScienceTile {Position = 7, CostReduction = 8, Track = Track.Gear, VictoryPoints = 5}
            };

        }
    }

    public class ScienceTile
    {
        public int Position { get; set; }
        public int CostReduction { get; set; }
        public Track Track { get; set; }
        public ShipComponent Tile { get; set; }
        public int VictoryPoints { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
    }


    public enum Track
    {
        Star,
        Grid,
        Gear
    }
}
