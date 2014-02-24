using libtcod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraLightRogue
{
    class Program
    {
        public static void Main(string[] args)
        {
            TCODConsole.initRoot(80, 60, "ULR", false);
            Control con = new Control();
            con.run();
        }
    }
}
