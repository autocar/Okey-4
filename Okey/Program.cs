using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okey
{
    class Program
    {

        static void Main(string[] args)
        {
            List<Tile> tiles = new List<Tile>();
            List<Tile> tempTiles = new List<Tile>();
            List<Player> players = new List<Player>();
            Player winnerPlayer = new Player("", null, 0);
            Utils util = new Utils();
            Player player1 = new Player("player1", new List<Tile>(), 0);
            Player player2 = new Player("player2", new List<Tile>(), 0);
            Player player3 = new Player("player3", new List<Tile>(), 0);
            Player player4 = new Player("player4", new List<Tile>(), 0);
            players.Add(player1);
            players.Add(player2);
            players.Add(player3);
            players.Add(player4);

            tiles = util.InitTiles();

            players = util.DistributeTiles(players, tiles);
            foreach (var player in players)
            {
                Console.WriteLine(player.Name);

                player.PlayerTiles = player.PlayerTiles.OrderBy(x => x.color).ThenBy(x => x.value).ToList();

                tempTiles = player.PlayerTiles.Distinct(new ItemEqualityComparer()).ToList();//remove duplicates

                //finding consecutive sets
                var consecutiveSets = tempTiles.GroupWhile((x, y) => y.value - x.value == 1)
                 .Select(x => new { i = x.First(), len = x.Count(), listOfSetTiles = x })
                 .Where(x => x.len > 2)
                 .ToList();
                foreach (var set in consecutiveSets)
                {
                    player.Score += set.len;
                    foreach (var setTile in set.listOfSetTiles)
                    {
                        player.PlayerTiles.Remove(setTile);
                    }
                }

                //finding same number sets
                var sameNumberSets = player.PlayerTiles
                    .Distinct(new ItemEqualityComparer())
                    .GroupBy(x => x.value)
                    .Where(x => x.Count() > 2)
                    .Select(x => new { listOfSetTiles = x, len = x.Count() })
                    .ToList();
                foreach (var set in sameNumberSets)
                {
                    player.Score += set.len;
                    foreach (var setTile in set.listOfSetTiles)
                    {
                        player.PlayerTiles.Remove(setTile);
                    }
                }
                if (player.Score > winnerPlayer.Score)
                    winnerPlayer = player;

                foreach (var deste in player.PlayerTiles)
                {
                    Console.WriteLine(deste.color + " " + deste.value);
                }
                Console.WriteLine("----------------------------------------");
            }

            Console.WriteLine(winnerPlayer.Name+" is the winner.");

            Console.ReadLine();
        }

        public enum TileColors
        {
            Undefined = 0,
            Green = 1,
            Blue = 2,
            Red = 3,
            Black = 4
        }
    }

    class ItemEqualityComparer : IEqualityComparer<Tile>
    {
        public bool Equals(Tile x, Tile y)
        {
            return ((x.value == y.value) && (x.color == y.color));
        }

        public int GetHashCode(Tile obj)
        {
            return obj.value.GetHashCode();
        }
    }
}
