using libtcod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraLightRogue
{
	class Transision
	{
		private String name;
		private int x;
		private int y;
		private char c;
		private TCODColor color;

		private static String[] firstNames = { "Orb", "Scroll", "Pillar", "Amulet", "Gem", "Staff", "Wand", "Door", "Potion" };
		private static String[] lastNames = { "Teleportation", "Transition", "Dimension Jump", "Digging", "Traveling", "Moving" };

		private static Random rand;

		public Transision(int px, int py)
		{
			rand = new Random();

			name = firstNames[rand.Next(0, firstNames.Length)] + " of " + lastNames[rand.Next(0, lastNames.Length)];

			x = px;
			y = py;

			c = '*';
			color = TCODColor.lightViolet;
		}

		public String getName()
		{
			return name;
		}

		public char getC()
		{
			return c;
		}

		public int getX()
		{
			return x;
		}

		public int getY()
		{
			return y;
		}

		public TCODColor getColor()
		{
			return color;
		}
	}
}
