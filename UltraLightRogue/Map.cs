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
		private Transision trans;
		private Monster boss;

		private static Random rand;

		public static String[] monsterModifier = { "Blind", "Crippled", "Stupid", "Mad", "Warrior", "Soldier", "Guardian", "Stalking", "Warlord", "Psycho", "Giant", "Armor Plated", "Infernal", "Titanic", "Otherwordly", "Divine" };

		public static String[] monsterNames = { "Zombie", "Blademaster", "Fiend", "Henchman", "Orc"};

		public static String[] minibossModifier1 = { "Ravenous", "Greater", "Indestructible", "Collosal", "Cosmic"};
		public static String[] minibossModifier2 = { "Destroyer", "Mega", "Avenger", "Titan", "Death", "Doom"};

		public static String[] minibossNames = { "Beast", "Ogre", "Golem", "Demon Lord" };

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
			generateItems();

			this.trans = new Transision(rand.Next(1, 44), rand.Next(1 , 39));
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
			int type = rand.Next(0,3);

			for (int i = 0; i < 10; i++)
			{
				int chance = rand.Next(1 + type, 4 + type);
				switch (chance)
				{
					case 1:
						makeZombie();
						break;
					case 2:
						makeFiend();
						break;
					case 3:
						makeOrc();
						break;
					case 4:
						makeHenchman();
						break;
					case 5:
						makeBlademaster();
						break;
				}
			}
		}

		private void makeZombie()
		{
			int chance = rand.Next(level, level + 3);
			switch (chance){
				case 1:
					monsters.Add(new Monster(AIType.unmoveable, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[0], chance, TCODColor.darkSea, 'Z', false, false, 40, 2, 20, 0, 0, 1));
					break;
				case 2:
					monsters.Add(new Monster(AIType.forgetful, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[0], chance, TCODColor.darkSea, 'Z', false, false, 50, 3, 20, 0, 0, 1));
					break;
				case 3:
					monsters.Add(new Monster(AIType.forgetful, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[0], chance, TCODColor.darkSea, 'Z', false, false, 50, 3, 20, 0, 0, 2));
					break;
				case 4:
					monsters.Add(new Monster(AIType.psycho, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[0], chance, TCODColor.darkSea, 'Z', false, false, 50, 3, 20, 0, 0, 2));
					break;
				case 5:
					monsters.Add(new Monster(AIType.warrior, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[0], chance, TCODColor.darkSea, 'Z', false, false, 60, 3, 25, 3, 0, 2));
					break;
				case 6:
					monsters.Add(new Monster(AIType.warrior, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[0], chance, TCODColor.darkSea, 'Z', false, false, 60, 4, 30, 6, 0, 2));
					break;
			}
		}

		private void makeBlademaster()
		{
			int chance = rand.Next(level, level + 3);
			switch (chance)
			{
				case 1:
					monsters.Add(new Monster(AIType.unmoveable, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[1], chance, TCODColor.darkHan, 'B', false, false, 50, 3, 8, 1, 0, 2));
					break;
				case 2:
					monsters.Add(new Monster(AIType.forgetful, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[1], chance, TCODColor.darkHan, 'B', false, false, 60, 3, 8, 2, 0, 2));
					break;
				case 3:
					monsters.Add(new Monster(AIType.forgetful, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[1], chance, TCODColor.darkHan, 'B', false, false, 70, 3, 8, 2, 10, 3));
					break;
				case 4:
					monsters.Add(new Monster(AIType.psycho, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[1], chance, TCODColor.darkHan, 'B', false, false, 70, 3, 8, 2, 10, 3));
					break;
				case 5:
					monsters.Add(new Monster(AIType.warrior, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[1], chance, TCODColor.darkHan, 'B', false, false, 80, 4, 10, 5, 10, 4));
					break;
				case 6:
					monsters.Add(new Monster(AIType.warrior, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[1], chance, TCODColor.darkHan, 'B', false, false, 80, 4, 12, 8, 15, 4));
					break;
			}
		}

		private void makeFiend()
		{
			int chance = rand.Next(level, level + 3);
			switch (chance)
			{
				case 1:
					monsters.Add(new Monster(AIType.unmoveable, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[2], chance, TCODColor.darkPink, 'F', false, false, 40, 2, 10, 1, 0, 2));
					break;
				case 2:
					monsters.Add(new Monster(AIType.forgetful, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[2], chance, TCODColor.darkPink, 'F', false, false, 50, 3, 10, 1, 0, 2));
					break;
				case 3:
					monsters.Add(new Monster(AIType.forgetful, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[2], chance, TCODColor.darkPink, 'F', false, false, 60, 3, 12, 3, 0, 3));
					break;
				case 4:
					monsters.Add(new Monster(AIType.psycho, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[2], chance, TCODColor.darkPink, 'F', false, false, 60, 3, 12, 3, 0, 3));
					break;
				case 5:
					monsters.Add(new Monster(AIType.warrior, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[2], chance, TCODColor.darkPink, 'F', false, false, 70, 3, 15, 6, 0, 3));
					break;
				case 6:
					monsters.Add(new Monster(AIType.warrior, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[2], chance, TCODColor.darkPink, 'F', false, false, 70, 3, 20, 10, 0, 3));
					break;
			}
		}

		private void makeHenchman()
		{
			int chance = rand.Next(level, level + 3);
			switch (chance)
			{
				case 1:
					monsters.Add(new Monster(AIType.unmoveable, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[3], chance, TCODColor.darkOrange, 'H', false, false, 40, 2, 10, 0, 0, 2));
					break;
				case 2:
					monsters.Add(new Monster(AIType.forgetful, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[3], chance, TCODColor.darkOrange, 'H', false, false, 50, 3, 10, 1, 0, 2));
					break;
				case 3:
					monsters.Add(new Monster(AIType.forgetful, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[3], chance, TCODColor.darkOrange, 'H', false, false, 60, 3, 10, 1, 10, 3));
					break;
				case 4:
					monsters.Add(new Monster(AIType.psycho, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[3], chance, TCODColor.darkOrange, 'H', false, false, 60, 3, 10, 2, 10, 3));
					break;
				case 5:
					monsters.Add(new Monster(AIType.warrior, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[3], chance, TCODColor.darkOrange, 'H', false, false, 70, 3, 12, 5, 15, 4));
					break;
				case 6:
					monsters.Add(new Monster(AIType.warrior, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[3], chance, TCODColor.darkOrange, 'H', false, false, 70, 4, 15, 8, 20, 4));
					break;
			}
		}

		private void makeOrc()
		{
			int chance = rand.Next(level, level + 3);
			switch (chance)
			{
				case 1:
					monsters.Add(new Monster(AIType.unmoveable, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[4], chance, TCODColor.darkGreen, 'G', false, false, 40, 2, 15, 1, 0, 2));
					break;
				case 2:
					monsters.Add(new Monster(AIType.forgetful, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[4], chance, TCODColor.darkGreen, 'G', false, false, 50, 3, 15, 1, 0, 2));
					break;
				case 3:
					monsters.Add(new Monster(AIType.forgetful, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[4], chance, TCODColor.darkGreen, 'G', false, false, 50, 3, 15, 2, 10, 3));
					break;
				case 4:
					monsters.Add(new Monster(AIType.psycho, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[4], chance, TCODColor.darkGreen, 'G', false, false, 50, 3, 15, 2, 10, 3));
					break;
				case 5:
					monsters.Add(new Monster(AIType.warrior, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[4], chance, TCODColor.darkGreen, 'G', false, false, 60, 3, 20, 5, 10, 3));
					break;
				case 6:
					monsters.Add(new Monster(AIType.warrior, rand.Next(1, 43), rand.Next(1, 38), monsterModifier[chance - 1] + " " + monsterNames[4], chance, TCODColor.darkGreen, 'G', false, false, 60, 4, 25, 8, 15, 3));
					break;
			}
		}

		private void generateItems()
		{
			for (int i = 0; i < 10; i++)
			{
				int chance = rand.Next(1, 100);

				if (chance <= 50)
				{
					items[rand.Next(1, 43), rand.Next(1, 38)] = new Item("Gold", rand.Next(1, 10) * (level), false, false, true, false);
				}
				else if (chance <= 70)
				{
					items[rand.Next(1, 43), rand.Next(1, 38)] = new Item("Healing potion", 1, false, false, false, true);
				}
				else if (chance <= 85)
				{
					int mat = rand.Next(level - 1, level + 2);
					items[rand.Next(1, 43), rand.Next(1, 38)] = new Item(Control.materials[mat] + " " + Control.weapons[rand.Next(0, Control.weapons.Length)], mat + 1, true, false, false, false);
				}
				else
				{
					int mat = rand.Next(level - 1, level + 2);
					items[rand.Next(1, 43), rand.Next(1, 38)] = new Item(Control.materials[mat] + " " + Control.armors[rand.Next(0, Control.armors.Length)], mat + 1, false, true, false, false);
				}
			}
		}

		private Item generateItem(int level)
		{
			int chance = rand.Next(1, 100);

			if (chance <= 40)
			{
				return new Item("Gold", rand.Next(1, 10) * (level), false, false, true, false);
			}
			else if (chance <= 50)
			{
				return new Item("Healing potion", 1, false, false, false, true);
			}
			else if (chance <= 60)
			{
				int mat = rand.Next(level, level + 2);
				return new Item(Control.materials[mat] + " " + Control.weapons[rand.Next(0, Control.weapons.Length)], mat + 1, true, false, false, false);
			}
			else if (chance <= 70)
			{
				int mat = rand.Next(level, level + 2);
				return new Item(Control.materials[mat] + " " + Control.armors[rand.Next(0, Control.armors.Length)], mat + 1, false, true, false, false);
			}
			return null;
		}

		public void generateFOV(int playerX, int playerY)
		{
			tcodMap.computeFov(playerX, playerY, 10, true, TCODFOVTypes.ShadowFov);
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

		public Transision getTransision()
		{
			return trans;
		}

		public Monster checkMonsterHealth()
		{
			for (int i = 0; i < monsters.Count; i++)
			{
				Monster tmp = (Monster) monsters[i];
				if (tmp.checkHealth())
				{
					items[tmp.getX(), tmp.getY()] = generateItem(tmp.getLevel());
					monsters.RemoveAt(i);
					return tmp;
				}
			}
			return null;
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
