using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okey
{
    class Tile
    {
        public int color;
        public int value;
        public bool isOkey = false;
        public Tile(int color,int value) {
            this.color = color;
            this.value = value;
        }
        public Tile()
        {
        }
    }
}
