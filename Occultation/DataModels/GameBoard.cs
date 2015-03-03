using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Occultation.DAL.EF;


namespace Occultation.DataModels
{
    public class GameBoard
    {
        public int GameId { get; set; }
        public List<MapTile> DivisionOne { get; set; }
        public List<MapTile> DivisionTwo { get; set; }
        public List<MapTile> DivisionThree { get; set; }
        public List<PlacedMapTile> RevealedTiles { get; set; }
        public List<User> Users { get; set; }
        public ScoreCard Card { get; set; }
        public GamePhase Phase { get; set; }
        public User CurrentPlayer { get; set; }





        public GameBoard(int gameId)
        {
            GameId = gameId;
            var allTiles = new AvailableMapTile();
            DivisionOne = allTiles.DivisionOne;
            DivisionTwo = allTiles.DivisionTwo;
            DivisionThree = allTiles.DivisionThree;
        }

        public GameBoard(List<User> users)
        {
            Users = users;
            DetermineOrder();
            var allTiles = new AvailableMapTile();
            DivisionOne = allTiles.DivisionOne;
            DivisionTwo = allTiles.DivisionTwo;
            DivisionThree = allTiles.DivisionThree;
            Card = new ScoreCard();
        }
        
        public void SetPlayers(int count)
        {
            Users = new List<User>();
            for (var i = 0; i < count; i++)
            {
                var human = new User
                {
                    CurrentOrange = 2,
                    CurrentBrown = 3,
                    CurrentPink = 3
                };
                Users.Add(human);
            }
        }

        public void DetermineOrder()
        {
            Users.Shuffle();
            var counter = 0;
            foreach (var player in Users)
            {
                player.TurnOrder = counter;
                counter++;
            }
        }

        public string ExploreMapTile(int userId, int x, int y)
        {
            // check to see if the coordinates are already taken
            // what division are the coordinates in
            var divs = new DivChecker().Divs;
            var division = divs.FirstOrDefault(c => c.X == x && c.Y == y);
            var intDiv = (division != null) ? division.Div : 3;
            
            // get the next item off the division pile 
            // remove from list and create new PlacedMapTile




            return "Success";
        }

        public void ResetUserStatus()
        {
            foreach (var player in Users)
            {
                player.Pass = false;
            }
        }

        public bool AllUsersHavePassed()
        {
            return Users.All(x => x.Pass != false);
        }

        public void Turns()
        {
            //Phase 1
            while(!AllUsersHavePassed())
            {
                foreach (var player in Users)
                {
                    //Set Current Player
                    CurrentPlayer = player;
                    
                    if (!player.Pass)
                    {
                        Card.Turn++;
                        switch (player.Color)
                        {
                            case DiscColor.Black:
                                Card.Black++;
                                break;
                            case DiscColor.Green:
                                Card.Green++;
                                break;
                            case DiscColor.Blue:
                                Card.Blue++;
                                break;
                            case DiscColor.Red:
                                Card.Red++;
                                break;
                            case DiscColor.Yellow:
                                Card.Yellow++;
                                break;
                            case DiscColor.White:
                                Card.White++;
                                break;
                            default:
                                break;
                        }
                        if ((Card.Turn%6) == 0)
                        {
                            player.Pass = true;
                        }
                    }
                    else
                    {
                        //They have passed, do they wan t   
                    }
                }
            }

            Console.WriteLine("Total Turns : " + Card.Turn);
            Console.WriteLine("Black Turns : " + Card.Black);
            Console.WriteLine("Green Turns : " + Card.Green);
            Console.WriteLine("Blue Turns : " + Card.Blue);
            Console.WriteLine("Red Turns : " + Card.Red);
            Console.WriteLine("Yellow Turns : " + Card.Yellow);
            Console.WriteLine("White Turns : " + Card.White);

        }


        public enum GamePhase
        {
            Action,
            Combat,
            Upkeep,
            Cleanup
        }
    }


}
