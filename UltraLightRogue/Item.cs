using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraLightRogue
{
	class Item
	{
		private String name;
		private int quality;

		private bool isWeapon;
		private bool isArmor;
		private bool isGold;
		private bool isPotion;

		public Item(String name, int quality, bool isWeapon, bool isArmor, bool isGold, bool isPotion)
		{
			this.name = name;
			this.quality = quality;
			this.isWeapon = isWeapon;
			this.isArmor = isArmor;
			this.isGold = isGold;
			this.isPotion = isPotion;
		}

		public String getName()
		{
			return name;
		}

		public int getQuality()
		{
			return quality;
		}

		public bool getIsWeapon()
		{
			return isWeapon;
		}

		public bool getIsArmor()
		{
			return isArmor;
		}

		public bool getIsGold()
		{
			return isGold;
		}

		public bool getIsPotion()
		{
			return isPotion;
		}
	}
}
