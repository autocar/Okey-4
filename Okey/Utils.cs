using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okey
{
    class Utils
    {
        static Random _random = new Random();

        public List<Tile> InitTiles()
        {
            List<Tile> allTiles = new List<Tile>();
            Tile tile;

            for (int t = 0; t < 2; t++)
            {
                for (int i = 1; i <= 13; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        tile = new Tile(j, i);
                        allTiles.Add(tile);
                    }
                }
                allTiles.Add(new Tile(0, 0));//For indicators
            }

            return allTiles;
        }

        public List<Player> DistributeTiles(List<Player> players, List<Tile> tiles)
        {
            List<Tile> shuffledTiles = Shuffle(tiles);//shuffling tiles 
            int skip = 0;
            foreach (var item in shuffledTiles)
            {
                Console.WriteLine(item.color + " " + item.value);
            }

            int indicatorIndex = _random.Next(0, shuffledTiles.Count);//Selecting indicator index

            Tile indicator = shuffledTiles[indicatorIndex];//Find indicator tile

            Tile okeyTile = new Tile(indicator.color, indicator.value == 13 ? 1 : indicator.value + 1);//Find okey tile

            shuffledTiles.RemoveAt(indicatorIndex);//remove indicator tile from all tiles

            //set isOkey property true for okey tiles
            foreach (var item in shuffledTiles.Where(x => x.value == okeyTile.value && x.color == okeyTile.color))
            {
                item.isOkey = true;
            }

            //distribute tiles to players
            for (int i = 0; i < players.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        skip = 0;
                        break;
                    case 1:
                        skip = 15;
                        break;
                    case 2:
                        skip = 29;
                        break;
                    case 3:
                        skip = 43;
                        break;
                    default:
                        break;
                }
                var selectedTiles = shuffledTiles.Skip(skip).Take(i == 0 ? 15 : 14).ToList();
                players[i].PlayerTiles = selectedTiles;
            }

            return players;
        }

        private List<Tile> Shuffle(List<Tile> tiles)
        {
            int n = tiles.Count;
            for (int i = 0; i < n; i++)
            {
                int r = i + _random.Next(n - i);
                Tile t = tiles[r];
                tiles[r] = tiles[i];
                tiles[i] = t;
            }
            return tiles;
        }

    }
}
