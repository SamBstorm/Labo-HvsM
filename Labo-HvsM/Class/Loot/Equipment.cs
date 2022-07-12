using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_HvsM.Class.Loot
{
    class Equipment:Item
    {
        #region Fields
        private int _durability;
        private bool _set;
        #endregion
        #region Properties
        public int Durability
        {
            get
            {
                return _durability;
            }

            private set
            {
                _durability = value;
            }
        }
        public bool Set
        {
            get
            {
                return _set;
            }

            private set
            {
                _set = value;
            }
        }
        #endregion
        #region Constructors
        private Equipment() : base(1) {
            this.Durability = 10;
            this.Set = false;
        }
        public Equipment(int Durability) : this()
        {
            this.Durability = Durability;
        }
        #endregion
        #region Methods
        public bool ToggleSet()
        {
            bool isSet = false;
            this.Set = !this.Set;
            return isSet;
        }
        #endregion
    }
}
