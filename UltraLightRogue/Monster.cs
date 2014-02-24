using libtcod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraLightRogue
{
	class Monster
	{
		private int x;
		private int y;
		private int origX;
		private int origY;

		private String name;
		private int level;
		private AIType ai;
		private TCODPath path;

		private TCODColor color;
		private char c;

		private int toHit;
		private int damage;
		private int health;
		private int AC;
		private int dodge;

		private bool miniboss;
		private bool finalboss;

		public Monster(AIType ai, int x, int y, String name, int level, TCODColor color, char c, bool miniboss, bool finalboss)
		{
			this.x = x;
			this.y = y;
			this.origX = x;
			this.origY = y;

			this.name = name;
			this.level = level;
			this.ai = ai;

			this.color = color;
			this.c = c;

			this.toHit = 60;
			this.damage = 3;
			this.health = 5;
			this.AC = 1;
			this.dodge = 10;
			
			this.miniboss = miniboss;
			this.finalboss = finalboss;
		}

		public void setX(int newX)
		{
			x = newX;
		}

		public void setY(int newY)
		{
			y = newY;
		}

		public void reduceHealth(int reduce)
		{
			health -= reduce;
		}

		public int getX()
		{
			return x;
		}

		public int getY()
		{
			return y;
		}

		public String getName()
		{
			return name;
		}

		public int getLevel()
		{
			return level;
		}

		public TCODColor getColor()
		{
			return color;
		}

		public char getC()
		{
			return c;
		}

		public int getToHit()
		{
			return toHit;
		}

		public int getDamage()
		{
			return damage;
		}

		public int getHealth()
		{
			return health;
		}

		public int getAC()
		{
			return AC;
		}

		public int getDodge()
		{
			return dodge;
		}

		public bool getMiniboss()
		{
			return miniboss;
		}

		public bool getFinalboss()
		{
			return finalboss;
		}

		public int getOrigX()
		{
			return origX;
		}

		public int getOrigY()
		{
			return origY;
		}

		public bool checkHealth()
		{
			if (health <= 0)
			{
				return true;
			}
			return false;
		}

		public bool moveAI(int plaX, int plaY, TCODMap map)
		{
			switch (ai)
			{
				case AIType.unmoveable:
					return checkAttack(plaX, plaY);
				case AIType.forgetful:
					path = new TCODPath(map);
					path.compute(x, y, plaX, plaY);
					if (path.size() < 5 && path.size() > 1)
					{
						path.walk(ref x, ref y, false);
						return false;
					}
					else
					{
						return checkAttack(plaX, plaY);
					}
				case AIType.guard:
					TCODPath tmpPath = new TCODPath(map);
					tmpPath.compute(x, y, origX, origY);

					path = new TCODPath(map);
					path.compute(x, y, plaX, plaY);
					if (path.size() < 5 && path.size() > 1 && tmpPath.size() < 3)
					{
						path.walk(ref x, ref y, false);
						return false;
					}
					else if (path.size() == 1)
					{
						return checkAttack(plaX, plaY);
					}
					else
					{
						tmpPath.walk(ref x, ref y, false);
						return false;
					}
				case AIType.warrior:
					path = new TCODPath(map);
					path.compute(x, y, plaX, plaY);
					if (path.size() < 8 && path.size() > 1)
					{
						path.walk(ref x, ref y, false);
						return false;
					}
					else
					{
						return checkAttack(plaX, plaY);
					}
				case AIType.psycho:
					path = new TCODPath(map);
					path.compute(x, y, plaX, plaY);
					if (path.size() > 1)
					{
						path.walk(ref x, ref y, false);
						return false;
					}
					else
					{
						return checkAttack(plaX, plaY);
					}
			}
			return false;
		}

		private bool checkAttack(int plaX, int plaY)
		{
			if ((plaX == x - 1 && plaY == y - 1) || (plaX == x && plaY == y - 1) || (plaX == x + 1 && plaY == y - 1) || (plaX == x - 1 && plaY == y) || (plaX == x + 1 && plaY == y) || (plaX == x - 1 && plaY == y + 1) || (plaX == x && plaY == y + 1) || (plaX == x + 1 && plaY == y + 1))
			{
				return true;
			}
			return false;
		}
	}
}
