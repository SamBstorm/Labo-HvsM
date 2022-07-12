using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_HvsM.Class.Loot
{
    class Armor:Equipment
    {
        #region Fields
        private int _armorBonus;
        #endregion
        #region Properties
        public int ArmorBonus
        {
            get
            {
                return _armorBonus;
            }

            private set
            {
                _armorBonus = value;
            }
        }
        #endregion
        #region Constructors
        public Armor(int Durability) : base(Durability)
        {
            this.ArmorBonus = 0;
        }
        public Armor(int Durability,int ArmorBonus) : base(Durability)
        {
            this.ArmorBonus = ArmorBonus;
        }
        #endregion
        #region Methods

        #endregion
    }
}
