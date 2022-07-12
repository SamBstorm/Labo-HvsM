using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_HvsM.Class.Loot
{
    class Weapon:Equipment
    {
        #region Fields
        private int _weaponBonus;
        #endregion
        #region Properties
        public int WeaponBonus
        {
            get
            {
                return _weaponBonus;
            }

            private set
            {
                _weaponBonus = value;
            }
        }
        #endregion
        #region Constructors
        public Weapon(int Durability) : base(Durability)
        {
            this.WeaponBonus = 0;
        }
        public Weapon(int Durability, int WeaponBonus) : base(Durability)
        {
            this.WeaponBonus = WeaponBonus;
        }
        #endregion
        #region Methods

        #endregion
    }
}
