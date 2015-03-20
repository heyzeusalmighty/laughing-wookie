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
            DivisionTwo.Shuffle();
            DivisionThree.Shuffle();
        }

        public AvailableMapTile(List<Tile> mapTiles)
        {
            SetTiles();
            DivisionOne.Shuffle();
            DivisionTwo.Shuffle();
            DivisionThree.Shuffle();
        }

        private void SetTiles()
        {
            DivisionOne = Div1Tiles();
            DivisionTwo = Div2Tiles();
            DivisionThree = Div3Tiles();

            foreach (var one in DivisionOne)
            {
                one.Division = 1;
            }

            foreach (var two in DivisionTwo)
            {
                two.Division = 2;
            }

            foreach (var three in DivisionThree)
            {
                three.Division = 3;
            }



            //DivisionOne = AllTheTiles.Where(x => x.Division == 1).ToList();
            //DivisionTwo = AllTheTiles.Where(x => x.Division == 2).ToList();
            //DivisionThree = AllTheTiles.Where(x => x.Division == 3).ToList();
        }

        public List<MapTile> Div1Tiles()
        {
            return new List<MapTile>
            {
                new MapTile
                {
                    MapId = 104,
                    Reward = true,
                    Occupied = "Aliens",
                    Aliens =2,
                    Orange =1,
                    OrangeAdvanced = 1,
                    Pink = 1,
                    PinkAdvanced = 1,
                    VictoryPoints = 2,
                    Wormholes = new []{1,1,0,1,1,0}
                },
                new MapTile
                {
                    MapId = 106,
                    Reward = false,
                    Occupied = "",
                    Pink =1,
                    Brown = 1,
                    VictoryPoints = 2,
                    Wormholes = new []{1,1,1,1,0,0}
                },
                new MapTile
                {
                    MapId = 107,
                    Reward = false,
                    Occupied = "",
                    Orange = 1,
                    PinkAdvanced = 1,
                    BrownAdvanced = 1,
                    VictoryPoints = 2,
                    Wormholes = new []{1,1,1,1,0,1}
                },
                new MapTile
                {
                    MapId = 101,
                    Reward = true,
                    Occupied = "Aliens",
                    Brown = 1,
                    BrownAdvanced = 1,
                    Orange = 1,
                    VictoryPoints = 2,
                    Wormholes = new []{0,1,1,1,1,1}
                },
                new MapTile
                {
                    MapId = 105,
                    Reward = true,
                    Occupied = "Aliens",
                    Orange = 1,
                    BrownAdvanced = 1,
                    Pink = 1,
                    VictoryPoints = 3,
                    Wormholes = new []{1,1,0,1,1,1}
                },
                new MapTile
                {
                    MapId = 103,
                    Reward = false,
                    Occupied = "",
                    White =1,
                    OrangeAdvanced = 1,
                    VictoryPoints = 2,
                    Wormholes = new []{1,1,1,0,1,1}
                },
                new MapTile
                {
                    MapId = 102,
                    Reward = false,
                    Occupied = "",
                    VictoryPoints = 3,
                    Pink = 1,
                    PinkAdvanced = 1,
                    Wormholes = new []{1,0,1,1,0,1}
                },
                new MapTile
                {
                    MapId = 108,
                    Reward = true,
                    Occupied = "Aliens",
                    Aliens = 1,
                    OrangeAdvanced = 1,
                    Pink = 1,
                    White =1,
                    VictoryPoints = 2,
                    Wormholes = new []{1,1,0,1,1,0}
                },
            };
        }

        public List<MapTile> Div2Tiles()
        {
            return new List<MapTile>
            {
                new MapTile
                {
                    MapId = 202,
                    Reward = false,
                    Occupied = "",
                    VictoryPoints = 1,
                    Pink =1,
                    PinkAdvanced = 1,
                    Wormholes = new []{0,1,0,1,0,1}
                },
                new MapTile
                {
                    MapId = 203,
                    Reward = true,
                    Occupied = "Aliens",
                    Aliens = 2,
                    Brown = 1,
                    Pink =1,
                    Orange = 1,
                    VictoryPoints = 1,
                    Wormholes = new []{1,1,0,1,0,1}
                },
                new MapTile
                {
                    MapId = 205,
                    Reward = false,
                    Occupied = "",
                    VictoryPoints = 1,
                    Orange = 1,
                    OrangeAdvanced = 1,
                    PinkAdvanced = 1,
                    Wormholes = new []{0,0,1,1,1,0}
                },
                new MapTile
                {
                    MapId = 206,
                    Reward = true,
                    Occupied = "",
                    Brown = 1,
                    Wormholes = new []{0,1,1,1,0,1}
                },
                new MapTile
                {
                    MapId = 201,
                    Reward = false,
                    Occupied = "",
                    VictoryPoints = 1,
                    Orange = 1,
                    Brown = 1,
                    Wormholes = new []{0,1,0,1,0,1}
                },
                new MapTile
                {
                    MapId = 207,
                    Reward = true,
                    Occupied = "",
                    VictoryPoints = 1,
                    Wormholes = new []{1,1,0,1,0,0}
                },
                new MapTile
                {
                    MapId = 209,
                    Reward = false,
                    Occupied = "",
                    Pink =1,
                    OrangeAdvanced = 1,
                    VictoryPoints = 1,
                    Wormholes = new []{1,1,0,1,0,1}
                },
                new MapTile
                {
                    MapId = 211,
                    Reward = true,
                    Occupied = "Aliens",
                    Aliens = 1,
                    Orange = 1,
                    BrownAdvanced = 1,
                    VictoryPoints = 2,
                    White = 1,
                    Wormholes = new []{1,1,1,1,0,0}
                },
                new MapTile
                {
                    MapId = 210,
                    Reward = false,
                    Occupied = "",
                    VictoryPoints = 1,
                    Orange = 1,
                    Brown =1,
                    Wormholes = new []{1,0,0,1,0,1}
                },
                new MapTile
                {
                    MapId = 204,
                    Reward = true,
                    Occupied = "Aliens",
                    OrangeAdvanced = 1,
                    BrownAdvanced = 1,
                    White = 1,
                    VictoryPoints = 2,
                    Wormholes = new []{1,1,0,1,0,1}
                },
                new MapTile
                {
                    MapId = 208,
                    Reward = true,
                    Occupied = "",
                    VictoryPoints = 1,
                    Wormholes = new []{1,0,1,1,0,1}
                }
            };
        }

        public List<MapTile> Div3Tiles()
        {
            return new List<MapTile>
            {
                new MapTile
                {
                    MapId = 315,
                    VictoryPoints = 1,
                    Reward = true,
                    Wormholes = new []{0,1,1,0,0,1},
                    Occupied = ""
                },
                new MapTile
                {
                    MapId = 309,
                    VictoryPoints = 1,
                    Orange = 1,
                    PinkAdvanced = 1,
                    Occupied = "",
                    Wormholes = new []{1,0,0,1,0,1}
                },
                new MapTile
                {
                    MapId = 305,
                    VictoryPoints = 1,
                    Aliens = 1,
                    Occupied = "Aliens",
                    Pink = 1,
                    Brown = 1,
                    Wormholes = new []{1,1,0,1,0,0},
                    Reward = true,
                },
                new MapTile
                {
                    MapId   = 316,
                    Reward = true,
                    Occupied = "",
                    VictoryPoints = 1,
                    Wormholes = new []{1,1,0,1,0,0}
                },
                new MapTile
                {
                    MapId = 303,
                    VictoryPoints = 2,
                    Aliens = 1,
                    Occupied = "Aliens",
                    White = 1,
                    Wormholes = new []{0,0,0,1,0,1},
                    Reward = true
                },
                new MapTile
                {
                    MapId = 301,
                    Aliens = 2,
                    Occupied = "Aliens",
                    VictoryPoints = 2,
                    Pink = 1,
                    Orange = 1,
                    BrownAdvanced = 1,
                    Wormholes = new []{1,0,1,1,0,0},
                    Reward = true

                },
                new MapTile
                {
                    MapId = 313,
                    Occupied = "",
                    Reward = true,
                    White = 1,
                    Wormholes = new []{1,0,0,1,0,0},
                    VictoryPoints = 1
                },
                new MapTile
                {
                    MapId = 314,
                    Occupied = "",
                    Reward = true,
                    White = 1,
                    VictoryPoints = 1,
                    Wormholes = new []{0,0,1,1,1,0}
                },
                new MapTile
                {
                    MapId = 311,
                    Occupied = "",
                    Reward = true,
                    Brown = 1,
                    VictoryPoints = 1,
                    Wormholes = new []{1,0,1,1,0,0}
                },
                new MapTile
                {
                    MapId = 307,
                    Occupied = "",
                    Reward = false,
                    VictoryPoints = 1,
                    Orange = 1,
                    PinkAdvanced = 1,
                    Wormholes = new []{1,0,1,1,0,0}

                },
                new MapTile
                {
                    MapId  = 302,
                    Occupied = "Aliens",
                    Reward = true,
                    Aliens = 1,
                    OrangeAdvanced = 1,
                    Brown = 1,
                    VictoryPoints = 2,
                    Wormholes = new []{1,0,0,1,1,0}
                },
                new MapTile
                {
                    MapId = 308,
                    Occupied = "",
                    Reward = false,
                    BrownAdvanced = 1,
                    Pink = 1,
                    VictoryPoints = 1,
                    Wormholes = new []{0,0,1,1,0,1}
                },
                new MapTile
                {
                    MapId = 306,
                    Occupied = "",
                    Reward = false,
                    Orange = 1,
                    Brown = 1,
                    VictoryPoints = 1,
                    Wormholes = new []{0,1,0,1,0,0}
                },
                new MapTile
                {
                    MapId = 310,
                    Occupied = "",
                    Reward = false,
                    Pink = 1,
                    Brown =1,
                    VictoryPoints = 1,
                    Wormholes = new []{1,0,0,1,0,0}
                },
                new MapTile
                {
                    MapId = 312,
                    Occupied = "",
                    Reward = true,
                    Brown = 1,
                    VictoryPoints = 1,
                    Wormholes = new []{1,1,0,1,0,0}
                },
                new MapTile
                {
                    MapId = 318,
                    Occupied = "",
                    Reward = false,
                    VictoryPoints = 1,
                    BrownAdvanced = 1,
                    White = 1,
                    Wormholes = new []{0,0,1,1,0,0}
                },
                new MapTile
                {
                    MapId = 304,
                    Occupied = "",
                    Reward = false,
                    OrangeAdvanced = 1,
                    Brown = 1,
                    VictoryPoints = 1,
                    Wormholes = new []{1,0,0,1,0,0}
                },
                new MapTile
                {
                    MapId = 317,
                    Occupied = "",
                    Reward = false,
                    VictoryPoints = 1,
                    Orange = 1,
                    OrangeAdvanced = 1,
                    Wormholes = new []{0,0,0,1,1,0}
                }
            };
        }

        public List<MapTile> PlayerTiles()
        {
            return new List<MapTile>
            {
                new MapTile{
                    MapId = 230,
                    Reward = false,
                    Occupied = "Blue",
                    VictoryPoints = 3,
                    Pink = 1,
                    PinkAdvanced = 1,
                    Orange = 1,
                    OrangeAdvanced = 1,
                    Brown = 1,
                    Wormholes = new[] { 1,1,0,1,1,0}
                },
                 new MapTile{
                    MapId = 231,
                    Reward = false,
                    Occupied = "Red",
                    VictoryPoints = 3,
                    Pink = 1,
                    PinkAdvanced = 1,
                    Orange = 1,
                    OrangeAdvanced = 1,
                    Brown = 1,
                    Wormholes = new[] { 1,1,0,1,1,0}
                },
                 new MapTile{
                    MapId = 232,
                    Reward = false,
                    Occupied = "Green",
                    VictoryPoints = 3,
                    Pink = 1,
                    PinkAdvanced = 1,
                    Orange = 1,
                    OrangeAdvanced = 1,
                    Brown = 1,
                    Wormholes = new[] { 1,1,0,1,1,0}
                },
                 new MapTile{
                    MapId = 233,
                    Reward = false,
                    Occupied = "Yellow",
                    VictoryPoints = 3,
                    Pink = 1,
                    PinkAdvanced = 1,
                    Orange = 1,
                    OrangeAdvanced = 1,
                    Brown = 1,
                    Wormholes = new[] { 1,1,0,1,1,0}
                },
                 new MapTile{
                    MapId = 234,
                    Reward = false,
                    Occupied = "White",
                    VictoryPoints = 3,
                    Pink = 1,
                    PinkAdvanced = 1,
                    Orange = 1,
                    OrangeAdvanced = 1,
                    Brown = 1,
                    Wormholes = new[] { 1,1,0,1,1,0}
                },
                 new MapTile{
                    MapId = 235,
                    Reward = false,
                    Occupied = "Black",
                    VictoryPoints = 3,
                    Pink = 1,
                    PinkAdvanced = 1,
                    Orange = 1,
                    OrangeAdvanced = 1,
                    Brown = 1,
                    Wormholes = new[] { 1,1,0,1,1,0}
                },
            };
        }

        public MapTile GetCentralTile()
        {
            return new MapTile
            {
                MapId = 1,
                Reward = true,
                Occupied = "Aliens",
                VictoryPoints = 4,
                White = 2,
                Orange = 1,
                OrangeAdvanced = 1,
                Pink = 1,
                PinkAdvanced = 1,
                Wormholes = new[] { 1,1,1,1,1,1}
            };
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
