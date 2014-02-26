using libtcod;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraLightRogue
{
    class Control
    {
		private bool gameEnd;

		private Map currentMap;
		private Player player;
		private ArrayList textBox;

		private static Random rand;

		public static String[] materials = {"Glass", "Plywood", "Oak", "Stone", "Copper", "Rusty", "Bronze", "Iron", "Steel", "Wootz Steel",
			"Titanium", "Mithril", "Adamantium", "Meteorite", "Starmetal", "Unobtainium", "Infinium"};

		public static String[] weapons = {"Broadsword", "Longsword", "Greatsword", "Scimitar", "Shortsword", "Halberd", "Pike", "Spear", "Voulge",
			"Club", "Mace", "Warhammer", "Flail", "Hatchet", "Waraxe", "Battleaxe", "Warpick"};

		public static String[] armors = { "Chain Shirt", "Chainmail", "Chestguard", "Breastplate", "Halfplate", "Fullplate", "Brigandine", "Scale Mail", "Fieldplate" };

		public static String[] realmStarts = { "Ug", "Xyl", "Nas", "Qve", "Pra", "Vus", "Znul", "Cri", "Svart", "Nif", "Cryk", "Fir", "Snim", "Rit", "Haf", "Tul", "Kris", "Xzir" };

		public static String[] realmNames = { "nam", "los", "hur", "nam", "tok", "fos", "stryk", "nir", "mon", "sae", "vars", "znek", "nok", "corm", "heim", "dal", "wuld", "strat" };

		public static String[] realmTypes = { "Land", "Realm", "Valley", "Kingdom", "Waste", "Plane", "Abyss", "Pit"};

		public static String[] realmTitles = { "Emptiness", "Despair", "Savagery", "Madness", "Eternal War", "Slaughter", "The Dead", "Deceit", 
												 "Torture", "Suffering", "Giants", "The War Machine", "Doom", "The Gods" , ""};

        public Control()
        {
			gameEnd = false;

			rand = new Random();

			currentMap = new Map(realmStarts[rand.Next(0, realmStarts.Length)] + realmNames[rand.Next(0, realmNames.Length)] + realmNames[rand.Next(0, realmNames.Length)] + ", The " + realmTypes[rand.Next(0, realmTypes.Length)] + " of " + realmTitles[0],  1); ;

			player = new Player(new Item(materials[0] + " " + weapons[rand.Next(0, weapons.Length)], 1, true, false, false, false), new Item(materials[0] + " " + armors[rand.Next(0, armors.Length)], 1, false, true, false, false), Classes.Armsman);

			textBox = new ArrayList();
        }

        public void run()
        {
            while (!TCODConsole.isWindowClosed() && !gameEnd)
            {
				draw();
				input();
				process();
            }
			end();
        }

        private void draw()
        {
			TCODConsole.root.clear();
			currentMap.generateFOV(player.getPlayerX(), player.getPlayerY());
			drawMap();
			drawItems();
			drawActors();
			drawText();
			drawTextBox();
			TCODConsole.flush();
        }

		private void drawMap()
		{
			for (int y = 0; y < 40; y++)
			{
				for (int x = 0; x < 45; x++)
				{
					if (currentMap.getTile(x, y) == 0 && currentMap.getInFOV(x, y))
					{
						TCODConsole.root.setForegroundColor(TCODColor.green);
						TCODConsole.root.print(x, y, ".");
					}
					else if (currentMap.getTile(x, y) == 1 && currentMap.getInFOV(x, y))
					{
						TCODConsole.root.setForegroundColor(TCODColor.lightGrey);
						TCODConsole.root.print(x, y, "#");
					}
				}
			}
		}

		private void drawItems()
		{
			for (int y = 0; y < 40; y++)
			{
				for (int x = 0; x < 45; x++)
				{
					if (currentMap.getItem(x, y) != null && currentMap.getInFOV(x, y))
					{			
						if (currentMap.getItem(x, y).getIsGold())
						{
							TCODConsole.root.setForegroundColor(TCODColor.yellow);
							TCODConsole.root.print(x, y, "o");
						}
						else if (currentMap.getItem(x, y).getIsWeapon())
						{
							TCODConsole.root.setForegroundColor(TCODColor.amber);
							TCODConsole.root.print(x, y, "w");
						}
						else if (currentMap.getItem(x, y).getIsArmor())
						{
							TCODConsole.root.setForegroundColor(TCODColor.azure);
							TCODConsole.root.print(x, y, "a");
						}
						else if (currentMap.getItem(x, y).getIsPotion())
						{
							TCODConsole.root.setForegroundColor(TCODColor.sky);
							TCODConsole.root.print(x, y, "p");
						}
					}
				}
			}

			if (currentMap.getInFOV(currentMap.getTransision().getX(), currentMap.getTransision().getY()))
			{
				TCODConsole.root.setForegroundColor(currentMap.getTransision().getColor());
				TCODConsole.root.print(currentMap.getTransision().getX(), currentMap.getTransision().getY(), currentMap.getTransision().getC() + "");
			}
		}

		private void drawActors()
		{
			TCODConsole.root.setForegroundColor(player.getPlayerColor());
			TCODConsole.root.print(player.getPlayerX(), player.getPlayerY(), player.getPlayerChar() + "");

			Object[] temp = currentMap.getMonsters();
			for (int i = 0; i < temp.Length; i++ )
			{
				Monster tempMon = (Monster) temp[i];
				if (currentMap.getInFOV(tempMon.getX(), tempMon.getY()))
				{
					TCODConsole.root.setForegroundColor(tempMon.getColor());
					TCODConsole.root.print(tempMon.getX(), tempMon.getY(), tempMon.getC() + "");
				}
			}
		}

		private void drawText()
		{
			TCODConsole.root.setForegroundColor(TCODColor.white);
			TCODConsole.root.print(0, 41, currentMap.getName());

			TCODConsole.root.setForegroundColor(TCODColor.sepia);
			for (int i = 0; i < 39; i++)
			{
				TCODConsole.root.print(46, 1 + i,"||");
			}

			TCODConsole.root.setForegroundColor(TCODColor.white);

			TCODConsole.root.print(49, 2, "Name:     " + player.getName());
			TCODConsole.root.print(49, 3, "Class:    " + player.getPCClass());
			TCODConsole.root.print(49, 4, "Level:    " + player.getLevel());
			TCODConsole.root.print(49, 5, "XP:       " + player.getXP() + "/" + (player.getLevel() * 10));
			TCODConsole.root.print(49, 6, "Health:   " + player.getNowHP() + "/" + player.getMaxHP());
			TCODConsole.root.print(49, 7, "Loot:     " + player.getGold() + " gold");

			TCODConsole.root.print(49, 9, "To Hit:   " + player.getToHit());
			TCODConsole.root.print(49, 10, "Dodge:    " + player.getDodge());

			TCODConsole.root.print(49, 12, "Weapon:(" + player.getWeapon().getQuality() + ") " + player.getWeapon().getName());

			TCODConsole.root.print(49, 14, "Armor: (" + player.getArmor().getQuality() + ") " + player.getArmor().getName());

			TCODConsole.root.print(49, 18, "Ground:   " + checkGround());

			TCODConsole.root.setForegroundColor(checkLoot());
			TCODConsole.root.print(49, 20, "q - Loot item");

			TCODConsole.root.setForegroundColor(checkGet());
			TCODConsole.root.print(49, 21, "w - Equip item");

			TCODConsole.root.setForegroundColor(checkUse());
			TCODConsole.root.print(49, 22, "e - Use item");
		}

		private TCODColor checkLoot()
		{
			if (currentMap.getItem(player.getPlayerX(), player.getPlayerY()) != null)
			{
				if (currentMap.getItem(player.getPlayerX(), player.getPlayerY()) != null)
				{
					return TCODColor.white;
				}
			}
			return TCODColor.grey;
		}

		private TCODColor checkGet()
		{
			if (currentMap.getItem(player.getPlayerX(), player.getPlayerY()) != null)
			{
				if (currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getIsArmor() || currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getIsWeapon())
				{
					return TCODColor.white;
				}
			}
			return TCODColor.grey;
		}

		private TCODColor checkUse()
		{
			if (currentMap.getItem(player.getPlayerX(), player.getPlayerY()) != null)
			{
				if (currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getIsPotion())
				{
					return TCODColor.white;
				}
			}
			else if (currentMap.getTransision().getX() == player.getPlayerX() && currentMap.getTransision().getY() == player.getPlayerY())
			{
				return TCODColor.white;
			}

			return TCODColor.grey;
		}

		private void drawTextBox()
		{
			TCODConsole.root.setForegroundColor(TCODColor.sepia);
			TCODConsole.root.print(0, 42, " ==============================================================================");
			TCODConsole.root.print(0, 58, " ==============================================================================");

			TCODConsole.root.setForegroundColor(TCODColor.lightestGrey);
			for (int i = 0; i < 15; i++ )
			{
				if (textBox.Count > i)
				{
					TCODConsole.root.print(2, 57 - i, (String) textBox[textBox.Count - 1 - i]);
				}
			}
		}

		private void input()
        {
			bool moveon = false;
			while (!moveon && !TCODConsole.isWindowClosed())
			{
				TCODKey key = TCODConsole.waitForKeypress(true);

				switch (key.KeyCode)
				{
					case TCODKeyCode.Up:
					case TCODKeyCode.KeypadEight:
						if (player.getPlayerY() > 0 && currentMap.getPassable(player.getPlayerX(), player.getPlayerY() - 1))
						{
							if (currentMap.checkIfMonster(player.getPlayerX(), player.getPlayerY() - 1))
							{
								Object[] temp = currentMap.getMonsters();
								for (int i = 0; i < temp.Length; i++)
								{
									Monster tempMon = (Monster)temp[i];
									if (tempMon.getX() == player.getPlayerX() && tempMon.getY() == player.getPlayerY() - 1)
									{
										tempMon.reduceHealth(executePlayerAttack(tempMon));
									}
								}
								moveon = true;
								break;
							}
							player.setPlayerY(player.getPlayerY() - 1);
							moveon = true;
						}
						break;
					case TCODKeyCode.Down:
					case TCODKeyCode.KeypadTwo:
						if (player.getPlayerY() < 39 && currentMap.getPassable(player.getPlayerX(), player.getPlayerY() + 1))
						{
							if (currentMap.checkIfMonster(player.getPlayerX(), player.getPlayerY() + 1))
							{
								Object[] temp = currentMap.getMonsters();
								for (int i = 0; i < temp.Length; i++)
								{
									Monster tempMon = (Monster)temp[i];
									if (tempMon.getX() == player.getPlayerX() && tempMon.getY() == player.getPlayerY() + 1)
									{
										tempMon.reduceHealth(executePlayerAttack(tempMon));
									}
								}
								moveon = true;
								break;
							}
							player.setPlayerY(player.getPlayerY() + 1);
							moveon = true;
						}
						break;
					case TCODKeyCode.Left:
					case TCODKeyCode.KeypadFour:
						if (player.getPlayerX() > 0 && currentMap.getPassable(player.getPlayerX() - 1, player.getPlayerY()))
						{
							if (currentMap.checkIfMonster(player.getPlayerX() - 1, player.getPlayerY()))
							{
								Object[] temp = currentMap.getMonsters();
								for (int i = 0; i < temp.Length; i++)
								{
									Monster tempMon = (Monster)temp[i];
									if (tempMon.getX() == player.getPlayerX() - 1 && tempMon.getY() == player.getPlayerY())
									{
										tempMon.reduceHealth(executePlayerAttack(tempMon));
									}
								}
								moveon = true;
								break;
							}
							player.setPlayerX(player.getPlayerX() - 1);
							moveon = true;
						}
						break;
					case TCODKeyCode.Right:
					case TCODKeyCode.KeypadSix:
						if (player.getPlayerX() < 44 && currentMap.getPassable(player.getPlayerX() + 1, player.getPlayerY()))
						{
							if (currentMap.checkIfMonster(player.getPlayerX() + 1, player.getPlayerY()))
							{
								Object[] temp = currentMap.getMonsters();
								for (int i = 0; i < temp.Length; i++)
								{
									Monster tempMon = (Monster)temp[i];
									if (tempMon.getX() == player.getPlayerX() + 1 && tempMon.getY() == player.getPlayerY())
									{
										tempMon.reduceHealth(executePlayerAttack(tempMon));
									}
								}
								moveon = true;
								break;
							}
							player.setPlayerX(player.getPlayerX() + 1);
							moveon = true;
						}
						break;
					case TCODKeyCode.KeypadSeven:
						if (player.getPlayerX() < 44 && currentMap.getPassable(player.getPlayerX() - 1, player.getPlayerY() - 1))
						{
							if (currentMap.checkIfMonster(player.getPlayerX() - 1, player.getPlayerY() - 1))
							{
								Object[] temp = currentMap.getMonsters();
								for (int i = 0; i < temp.Length; i++)
								{
									Monster tempMon = (Monster)temp[i];
									if (tempMon.getX() == player.getPlayerX() - 1 && tempMon.getY() == player.getPlayerY() - 1)
									{
										tempMon.reduceHealth(executePlayerAttack(tempMon));
									}
								}
								moveon = true;
								break;
							}
							player.setPlayerX(player.getPlayerX() - 1);
							player.setPlayerY(player.getPlayerY() - 1);
							moveon = true;
						}
						break;
					case TCODKeyCode.KeypadNine:
						if (player.getPlayerX() < 44 && currentMap.getPassable(player.getPlayerX() + 1, player.getPlayerY() - 1))
						{
							if (currentMap.checkIfMonster(player.getPlayerX() + 1, player.getPlayerY() - 1))
							{
								Object[] temp = currentMap.getMonsters();
								for (int i = 0; i < temp.Length; i++)
								{
									Monster tempMon = (Monster)temp[i];
									if (tempMon.getX() == player.getPlayerX() + 1 && tempMon.getY() == player.getPlayerY() - 1)
									{
										tempMon.reduceHealth(executePlayerAttack(tempMon));
									}
								}
								moveon = true;
								break;
							}
							player.setPlayerX(player.getPlayerX() + 1);
							player.setPlayerY(player.getPlayerY() - 1);
							moveon = true;
						}
						break;
					case TCODKeyCode.KeypadOne:
						if (player.getPlayerX() < 44 && currentMap.getPassable(player.getPlayerX() - 1, player.getPlayerY() + 1))
						{
							if (currentMap.checkIfMonster(player.getPlayerX() - 1, player.getPlayerY() + 1))
							{
								Object[] temp = currentMap.getMonsters();
								for (int i = 0; i < temp.Length; i++)
								{
									Monster tempMon = (Monster)temp[i];
									if (tempMon.getX() == player.getPlayerX() - 1 && tempMon.getY() == player.getPlayerY() + 1)
									{
										tempMon.reduceHealth(executePlayerAttack(tempMon));
									}
								}
								moveon = true;
								break;
							}
							player.setPlayerX(player.getPlayerX() - 1);
							player.setPlayerY(player.getPlayerY() + 1);
							moveon = true;
						}
						break;
					case TCODKeyCode.KeypadThree:
						if (player.getPlayerX() < 44 && currentMap.getPassable(player.getPlayerX() + 1, player.getPlayerY() + 1))
						{
							if (currentMap.checkIfMonster(player.getPlayerX() + 1, player.getPlayerY() + 1))
							{
								Object[] temp = currentMap.getMonsters();
								for (int i = 0; i < temp.Length; i++)
								{
									Monster tempMon = (Monster)temp[i];
									if (tempMon.getX() == player.getPlayerX() + 1 && tempMon.getY() == player.getPlayerY() + 1)
									{
										tempMon.reduceHealth(executePlayerAttack(tempMon));
									}
								}
								moveon = true;
								break;
							}
							player.setPlayerX(player.getPlayerX() + 1);
							player.setPlayerY(player.getPlayerY() + 1);
							moveon = true;
						}
						break;
					case TCODKeyCode.Char:
						switch (key.Character)
						{
							case 'q':
								if (currentMap.getItem(player.getPlayerX(), player.getPlayerY()) != null)
								{
									if (currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getIsWeapon())
									{
										textBox.Add("You looted " + currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getName() + " for " + currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getQuality() + " gold");
										player.addGold(currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getQuality());
										currentMap.setItem(player.getPlayerX(), player.getPlayerY(), null);
										moveon = true;
									}
									else if (currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getIsArmor())
									{
										textBox.Add("You looted " + currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getName() + " for " + currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getQuality() + " gold");
										player.addGold(currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getQuality());
										currentMap.setItem(player.getPlayerX(), player.getPlayerY(), null);
										moveon = true;
									}
									else if (currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getIsGold())
									{
										textBox.Add("You picked up " + currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getQuality() + " gold");
										player.addGold(currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getQuality());
										currentMap.setItem(player.getPlayerX(), player.getPlayerY(), null);
										moveon = true;
									}
									else if (currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getIsPotion())
									{
										textBox.Add("You looted " + currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getName() + " for 10 gold");
										player.addGold(10);
										currentMap.setItem(player.getPlayerX(), player.getPlayerY(), null);
										moveon = true;
									}
								}
								break;
							case 'w':
								if (currentMap.getItem(player.getPlayerX(), player.getPlayerY()) != null)
								{
									if (currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getIsWeapon())
									{
										textBox.Add("You looted " + player.getWeapon().getName() + " for " + player.getWeapon().getQuality() + " gold");
										player.addGold(player.getWeapon().getQuality());
										player.setWeapon(currentMap.getItem(player.getPlayerX(), player.getPlayerY()));
										textBox.Add("You equip " + player.getWeapon().getName());
										currentMap.setItem(player.getPlayerX(), player.getPlayerY(), null);
										moveon = true;
									}
									else if (currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getIsArmor())
									{
										textBox.Add("You looted " + player.getArmor().getName() + " for " + player.getArmor().getQuality() + " gold");
										player.addGold(player.getArmor().getQuality());
										player.setArmor(currentMap.getItem(player.getPlayerX(), player.getPlayerY()));
										textBox.Add("You equip " + player.getArmor().getName());
										currentMap.setItem(player.getPlayerX(), player.getPlayerY(), null);
										moveon = true;
									}
								}
								break;
							case 'e':
								if (currentMap.getItem(player.getPlayerX(), player.getPlayerY()) != null)
								{
									if (currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getIsPotion())
									{
										player.setNowHP(player.getNowHP() + (player.getMaxHP() / 2));
										textBox.Add("You drink the healing potion, and recover some lost health.");
										currentMap.setItem(player.getPlayerX(), player.getPlayerY(), null);
										moveon = true;
									}
								}
								else if (currentMap.getTransision().getX() == player.getPlayerX() && currentMap.getTransision().getY() == player.getPlayerY())
								{
									textBox.Add("You transport yourself into another realm");

									Map tmp = new Map(realmStarts[rand.Next(0, realmStarts.Length)] + realmNames[rand.Next(0, realmNames.Length)] + realmNames[rand.Next(0, realmNames.Length)] + ", The " + realmTypes[rand.Next(0, realmTypes.Length)] + " of " + realmTitles[currentMap.getLevel()], currentMap.getLevel() + 1);
									
									currentMap = tmp;
									player.setPlayerX(rand.Next(1, 44));
									player.setPlayerY(rand.Next(1, 39));
									moveon = true;
								}
								break;
						}
						break;
				}
			}
        }

        private void process()
        {
			Monster tmp = currentMap.checkMonsterHealth();
			if (tmp != null)
			{
				player.addXP(tmp.getLevel());
				textBox.Add(tmp.getName() + " falls to your might!");
			}

			for (int i = 0; i < currentMap.getMonsters().Length; i++ )
			{
				if (currentMap.moveAndCheckAttack(i, player.getPlayerX(), player.getPlayerY()))
				{
					Object[] temp = currentMap.getMonsters();
					Monster monTemp = (Monster) temp[i];
					player.setNowHP(player.getNowHP() - executeEnemyAttack(monTemp));
				}
			}
			
			if (player.endRoundCheck())
			{
				textBox.Add("You have gained a level, " + player.getName() + "'s stats have been increased");
			}

			if (player.getNowHP() <= 0)
			{
				gameEnd = true;
			}
			
        }

		private Item generateItem(int monsterLevel)
		{
			int chance = rand.Next(1, 100);
			
			if (chance <= 30){
				return new Item("Gold", rand.Next(1, 10) * (monsterLevel + 1), false, false, true, false);
			}
			if (chance <= 45)
			{
				return new Item("Healing potion", 1, false, false, false, true);
			}
			if (chance <= 60)
			{
				int mat = rand.Next(monsterLevel, monsterLevel + 3);
				return new Item(materials[mat] + " " + weapons[rand.Next(0, weapons.Length)], mat + 1, true, false, false, false);
			}
			if (chance <= 75)
			{
				int mat = rand.Next(monsterLevel, monsterLevel + 3);
				return new Item(materials[mat] + " " + armors[rand.Next(0, armors.Length)], mat + 1, false, true, false, false);
			}
			return null;
		}

		private String checkGround()
		{
			if (currentMap.getItem(player.getPlayerX(), player.getPlayerY()) != null)
			{
				if (currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getIsGold())
				{
					return currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getQuality() + " " + currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getName();
				}
				return currentMap.getItem(player.getPlayerX(), player.getPlayerY()).getName();
			}
			else if (currentMap.getTransision().getX() == player.getPlayerX() && currentMap.getTransision().getY() == player.getPlayerY())
			{
				return currentMap.getTransision().getName();
			}
			return "-";
		}

		private int executePlayerAttack(Monster monster)
		{
			int chance = player.getToHit() - monster.getDodge();
			int result = rand.Next(1, 100);
			if (result < chance)
			{
				int variation = rand.Next(1, 4);
				int damage = (1 + (player.getWeapon().getQuality()) * variation);
				damage -= monster.getAC();
				if (damage > 0)
				{
					textBox.Add(player.getName() + " hits " + monster.getName() + " for " + damage + " damage");
					return damage;
				}
				else
				{
					textBox.Add(monster.getName() + "'s armor completely absorbs the attack");
					return 0;
				}
			}
			else
			{
				textBox.Add(monster.getName() + " dodges " + player.getName() + "'s attack");
				return 0;
			}
		}

		private int executeEnemyAttack(Monster monster)
		{
			int chance = monster.getToHit() - player.getDodge();
			int result = rand.Next(1, 100);
			if (result < chance)
			{
				int variation = rand.Next(1, 3);
				int damage = monster.getDamage() * variation;
				damage -= player.getArmor().getQuality() + 1;
				if (damage > 0)
				{
					textBox.Add(monster.getName() + " hits " + player.getName() + " for " + damage + " damage");
					return damage;
				}
				else
				{
					textBox.Add(player.getName() + "'s armor completely absorbs the attack");
					return 0;
				}
			}
			else
			{
				textBox.Add(player.getName() + " dodges " + monster.getName() + "'s attack");
				return 0;
			}
		}

		private void end(){
			TCODConsole.root.clear();
			TCODConsole.root.setForegroundColor(TCODColor.red);
			TCODConsole.root.print(35, 25, "You died");
			TCODConsole.flush();
			TCODConsole.waitForKeypress(true);
		}
    }
}
