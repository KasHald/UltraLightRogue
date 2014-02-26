using libtcod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraLightRogue
{
	class Player
	{
		private String name;

		private int playerX;
		private int playerY;

		private TCODColor playerColor;
		private char playerChar;

		private Item weapon;
		private Item armor;
		private int gold;
		
		private int maxHP;
		private int nowHP;

		private Classes pcClass;
		private int level;
		private int xp;
		private int toHit;
		private int dodge;

		public Player(Item weapon, Item armor, Classes pcClass)
		{
			this.name = "Tim";
			this.pcClass = pcClass;
			
			this.playerX = 1;
			this.playerY = 1;

			this.playerColor = TCODColor.magenta;
			this.playerChar = '@';

			this.weapon = weapon;
			this.armor = armor;
			this.gold = 0;

			this.maxHP = healthSet();
			this.nowHP = maxHP;

			this.level = 1;
			this.xp = 0;
			this.toHit = toHitSet();
			this.dodge = dodgeSet();
		}

		private int healthSet(){
			switch (pcClass)
			{
				case Classes.Throatstabber:
					return 10;
				case Classes.Acrobat:
					return 20;
				case Classes.Fencer:
					return 25;
				case Classes.Armsman:
					return 30;
				case Classes.Berserker:
					return 35;
				case Classes.Juggernaut:
					return 40;
				case Classes.Meatshield:
					return 60;
			}
			return 30;
		}

		private int toHitSet()
		{
			switch (pcClass)
			{
				case Classes.Meatshield:
					return 40;
				case Classes.Acrobat:
				case Classes.Juggernaut:
					return 50;
				case Classes.Armsman:
					return 60;
				case Classes.Berserker:
					return 65;
				case Classes.Fencer:
					return 75;
				case Classes.Throatstabber:
					return 100;
			}
			return 60;
		}

		private int dodgeSet()
		{
			switch (pcClass)
			{
				case Classes.Meatshield:
					return 0;
				case Classes.Berserker:
				case Classes.Fencer:
				case Classes.Juggernaut:
					return 5;
				case Classes.Armsman:
				case Classes.Throatstabber:
					return 10;
				case Classes.Acrobat:
					return 30;
			}
			return 10;
		}

		public bool endRoundCheck()
		{
			if (xp >= level * 10)
			{
				xp -= level * 10;
				levelUp();
				return true;
			}
			return false;
		}

		private void levelUp()
		{
			level++;
			switch (pcClass)
			{
				case Classes.Acrobat:
					maxHP += 3;
					nowHP = maxHP;
					toHit += 5;
					dodge += 2;
					break;
				case Classes.Armsman:
					maxHP += 5;
					nowHP = maxHP;
					toHit += 5;
					dodge += 1;
					break;
				case Classes.Berserker:
					maxHP += 5;
					nowHP = maxHP;
					toHit += 5;
					dodge += 1;
					break;
				case Classes.Fencer:
					maxHP += 3;
					nowHP = maxHP;
					toHit += 8;
					dodge += 1;
					break;
				case Classes.Juggernaut:
					maxHP += 8;
					nowHP = maxHP;
					toHit += 3;
					dodge += 1;
					break;
				case Classes.Meatshield:
					maxHP += 15;
					nowHP = maxHP;
					toHit += 2;
					break;
				case Classes.Throatstabber:
					maxHP += 1;
					nowHP = maxHP;
					toHit += 5;
					dodge += 3;
					break;
			}
		}

		public void setPlayerX(int x)
		{
			playerX = x;
		}

		public void setPlayerY(int y)
		{
			playerY = y;
		}

		public void setWeapon(Item newWeapon)
		{
			weapon = newWeapon;
		}

		public void setArmor(Item newArmor)
		{
			armor = newArmor;
		}

		public void setMaxHP(int newMaxHP)
		{
			maxHP = newMaxHP;
		}

		public void setNowHP(int newNowHP)
		{
			nowHP = newNowHP;
			if (nowHP > maxHP)
			{
				nowHP = maxHP;
			}
		}

		public void setLevel(int newLevel)
		{
			level = newLevel;
		}

		public void addGold(int add)
		{
			gold += add;
		}

		public void addXP(int add)
		{
			xp += add;
		}

		public String getName()
		{
			return name;
		}

		public int getPlayerX()
		{
			return playerX;
		}

		public int getPlayerY()
		{
			return playerY;
		}

		public TCODColor getPlayerColor()
		{
			return playerColor;
		}

		public char getPlayerChar()
		{
			return playerChar;
		}

		public Item getWeapon()
		{
			return weapon;
		}

		public Item getArmor()
		{
			return armor;
		}

		public int getGold()
		{
			return gold;
		}

		public int getMaxHP()
		{
			return maxHP;
		}

		public int getNowHP()
		{
			return nowHP;
		}

		public int getLevel()
		{
			return level;
		}

		public int getXP()
		{
			return xp;
		}

		public int getToHit()
		{
			return toHit;
		}

		public int getDodge()
		{
			return dodge;
		}

		public Classes getPCClass()
		{
			return pcClass;
		}
	}
}
