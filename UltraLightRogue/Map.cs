using libtcod;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraLightRogue
{
	class Map
	{
		private String name;
		private byte[,] terrain;
		private Item[,] items;
		private TCODMap tcodMap;
		private ArrayList monsters;
		private int level;

		private static Random rand;

		public Map(String name, int level)
		{
			rand = new Random();

			this.name = name;
			this.terrain = new byte[45, 40];
			this.items = new Item[45, 40];
			this.tcodMap = new TCODMap(45, 40);
			this.monsters = new ArrayList();
			this.level = level;

			generateTerrainMap();
			generateTCODMap();
			generateMonsters();
		}

		private void generateTerrainMap()
		{
			for (int y = 0; y < tcodMap.getHeight(); y++)
			{
				for (int x = 0; x < tcodMap.getWidth(); x++)
				{
					if (y == 0 || y == 39 || x == 0 || x == 44)
					{
						terrain[x, y] = 1;
					}
					else
					{
						terrain[x, y] = 0;
					}
				}
			}
		}

		private void generateTCODMap()
		{
			for (int y = 0; y < tcodMap.getHeight(); y++)
			{
				for (int x = 0; x < tcodMap.getWidth(); x++)
				{
					if (terrain[x, y] == 0)
					{
						tcodMap.setProperties(x, y, true, true);
					}
					else
					{
						tcodMap.setProperties(x, y, false, false);
					}
				}
			}
		}

		private void generateMonsters()
		{
			for (int i = 0; i < 10; i++)
			{
				monsters.Add(new Monster(AIType.forgetful, rand.Next(1, 43), rand.Next(1, 38), "Test Monster", level, TCODColor.red, 'M', false, false));
			}
		}

		public void generateFOV(int playerX, int playerY)
		{
			tcodMap.computeFov(playerX, playerY, 20, true, TCODFOVTypes.ShadowFov);
		}

		public void setItem(int x, int y, Item item)
		{
			items[x, y] = item;
		}

		public String getName()
		{
			return name;
		}

		public byte getTile(int x, int y)
		{
			return terrain[x, y];
		}

		public Item getItem(int x, int y)
		{
			return items[x, y];
		}

		public bool getInFOV(int x, int y)
		{
			return tcodMap.isInFov(x, y);
		}

		public bool getPassable(int x, int y)
		{
			return tcodMap.isWalkable(x, y);
		}

		public Object[] getMonsters()
		{
			Object[] temp = monsters.ToArray();
			return  temp;
		}

		public int getLevel()
		{
			return level;
		}

		public String checkMonsterHealth()
		{
			String ret;
			for (int i = 0; i < monsters.Count; i++)
			{
				Monster tmp = (Monster) monsters[i];
				if (tmp.checkHealth())
				{
					ret = tmp.getName() + " falls to your might!";
					monsters.RemoveAt(i);
					return ret;
				}
			}
			return "";
		}

		public bool checkIfMonster(int x, int y)
		{
			for (int i = 0; i < monsters.Count; i++)
			{
				Monster tmp = (Monster) monsters[i];
				if (tmp.getX() == x && tmp.getY() == y)
				{
					return true;
				}
			}
			return false;
		}

		public bool moveAndCheckAttack(int monNr, int plaX, int plaY)
		{
			
			Monster tmp = (Monster)monsters[monNr];
			if (tmp.moveAI(plaX, plaY, tcodMap))
			{
				return true;
			}
			
			return false;
		}
	}
}
