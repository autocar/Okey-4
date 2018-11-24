using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okey
{
    class Player
    {
        private string name;
        private List<Tile> playerTiles;
        private int score;

        public Player()
        {

        }

        public Player(string name, List<Tile> playerTiles, int score)
        {
            this.Name = name;
            this.PlayerTiles = playerTiles;
            this.score = score;
        }

        public int Score { get => score; set => score = value; }
        public string Name { get => name; set => name = value; }
        internal List<Tile> PlayerTiles { get => playerTiles; set => playerTiles = value; }
    }
}
