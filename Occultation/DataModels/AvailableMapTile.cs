using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Eclipse.DAL.EF;
using Occultation.DAL.EF;

namespace Occultation.DataModels
{
    public class AvailableMapTile
    {
        public List<MapTile> DivisionOne { get; set; }
        public List<MapTile> DivisionTwo { get; set; }
        public List<MapTile> DivisionThree { get; set; }
        public List<MapTile> AllTheTiles { get; set; }

        public AvailableMapTile()
        {
            SetTiles();
            //Shuffle
            DivisionOne.Shuffle();
        }

        public AvailableMapTile(List<Tile> mapTiles)
        {
            SetTiles();

        }

        private void SetTiles()
        {
            AllTheTiles = new List<MapTile>{ 
            new MapTile
            {
                //This is the terran starting tile
                MapId = 666,
                Orange = 1, 
                OrangeAdvanced = 1, 
                Pink = 1, 
                PinkAdvanced = 1, 
                Brown = 1,
                VictoryPoints = 3, 
                Division = 2,
                Wormholes = new[]{1,1,0,1,1,0}
            }, 
            new MapTile
            {
                MapId = 1,
                Aliens = 2, 
                Orange = 1, 
                OrangeAdvanced = 1, 
                Pink = 1, 
                PinkAdvanced = 1, 
                VictoryPoints = 2, 
                Division = 1,
                Wormholes = new[]{1,1,0,1,1,0}
            }, 
            new MapTile
            {
                MapId = 2,
                Aliens = 1,
                Orange = 1,
                BrownAdvanced = 1,
                Brown = 1,
                VictoryPoints = 2,
                Division = 1,
                Wormholes = new[] { 0, 1, 1, 1, 1, 1 }
            }, 
            new MapTile
            {
                MapId = 3,
                Pink = 1,
                PinkAdvanced = 1,
                VictoryPoints = 3,
                Division = 1,
                Wormholes = new[] { 1, 0, 1, 1, 0, 1 }
            },
                new MapTile
            {
                MapId = 4,
                Brown = 1,
                Pink = 1,
                VictoryPoints = 2,
                Division = 1,
                Wormholes = new[] {1,1,1,1,0,0 }
            },
            new MapTile
            {
                MapId = 5,
                Aliens = 1,
                Orange = 1,
                Pink = 1,
                BrownAdvanced = 1,
                VictoryPoints = 3,
                Division = 1,
                Wormholes = new[] {1,1,0,1,1,1 }
            },
            new MapTile
            {
                MapId = 6,
                Aliens = 1,
                White = 1,
                OrangeAdvanced = 1,
                Pink = 1,
                VictoryPoints = 2,
                Division = 1,
                Wormholes = new[] {1,1,0,1,1,0}
            },
            new MapTile
            {
                MapId = 7,
                Orange = 1,
                BrownAdvanced = 1,
                PinkAdvanced = 1,
                VictoryPoints = 2,

                Wormholes = new[] {1,1,1,1,0,1},
                Division = 1
            },
            new MapTile
            {
                MapId = 8,
                OrangeAdvanced = 1,
                White = 1,
                VictoryPoints = 2,
                Division = 1,
                Wormholes = new[] {1,1,1,0,1,1}
            },
            new MapTile
            {
                MapId = 9,
                PinkAdvanced = 1,
                Pink = 1,
                VictoryPoints = 1,
                Division = 2,
                Wormholes = new[] {0,1,0,1,0,1}
            },
            new MapTile
            {
                MapId = 10,
                Pink = 1,
                Brown = 1,
                Orange = 1,
                VictoryPoints = 1,
                Aliens = 2,
                Wormholes = new [] {1,1,0,1,0,1}
            }};

            DivisionOne = AllTheTiles.Where(x => x.Division == 1).ToList();
            DivisionTwo = AllTheTiles.Where(x => x.Division == 2).ToList();
            DivisionThree = AllTheTiles.Where(x => x.Division == 3).ToList();
        }


        public List<MapTile> BringABunchOfDummyTiles()
        {
            return new List<MapTile>{ new MapTile
            {
                MapId = 1,
                Aliens = 2, 
                Orange = 1, 
                OrangeAdvanced = 1, 
                Pink = 1, 
                PinkAdvanced = 1, 
                VictoryPoints = 2, 
                Wormholes = new[]{1,1,0,1,1,0},
                Occupied = ""
            }, 
            new MapTile
            {
                MapId = 2,
                Aliens = 1,
                Orange = 1,
                BrownAdvanced = 1,
                Brown = 1,
                VictoryPoints = 2,
                Wormholes = new[] { 0, 1, 1, 1, 1, 1 },
                Occupied = ""
            }, 
            new MapTile
            {
                MapId = 3,
                Pink = 1,
                PinkAdvanced = 1,
                VictoryPoints = 3,
                Wormholes = new[] { 1, 0, 1, 1, 0, 1 },
                Occupied = ""
            },
                new MapTile
            {
                MapId = 4,
                Brown = 1,
                Pink = 1,
                VictoryPoints = 2,
                Wormholes = new[] {1,1,1,1,0,0 },
                Occupied = ""
            },
            new MapTile
            {
                MapId = 5,
                Aliens = 1,
                Orange = 1,
                Pink = 1,
                BrownAdvanced = 1,
                VictoryPoints = 3,
                Wormholes = new[] {1,1,0,1,1,1 },
                Occupied = ""
            },
            new MapTile
            {
                MapId = 6,
                Aliens = 1,
                White = 1,
                OrangeAdvanced = 1,
                Pink = 1,
                VictoryPoints = 2,
                Wormholes = new[] {1,1,0,1,1,0},
                Occupied = ""
            },
            new MapTile
            {
                MapId = 7,
                Orange = 1,
                BrownAdvanced = 1,
                PinkAdvanced = 1,
                VictoryPoints = 2,
                Wormholes = new[] {1,1,1,1,0,1},
                Occupied = ""
            },
            new MapTile
            {
                MapId = 8,
                OrangeAdvanced = 1,
                White = 1,
                VictoryPoints = 2,
                Wormholes = new[] {1,1,1,0,1,1},
                Occupied = ""
            }};
        }
        
    }



  
    
    
}
