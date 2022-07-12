using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labo_HvsM.Class;

namespace Labo_HvsM.Class.Character
{
    abstract class Character
    {
        #region Fields
        private int _x;
        private int _y;
        private int _endBase;
        private int _forBase;
        private int _pv;
        private int _gold;
        private int _leather;
        #endregion
        #region Properties
        public int X
        {
            get
            {
                return _x;
            }

            private set
            {
                _x = value;
            }
        }
        public int Y
        {
            get
            {
                return _y;
            }

            private set
            {
                _y = value;
            }
        }
        public int EndBase
        {
            get
            {
                return _endBase;
            }

            private set
            {
                _endBase = value;
            }
        }
        public int ForBase
        {
            get
            {
                return _forBase;
            }

            private set
            {
                _forBase = value;
            }
        }
        public int EndBonus
        {
            get
            {
                int bonus = 0;
                if (EndBase < 5)
                {
                    bonus = -1;
                }
                else if (EndBase < 10)
                {
                    bonus = 0;
                }
                else if (EndBase < 15)
                {
                    bonus = 1;
                }
                else { bonus = 2; }
                return bonus;
            }
        }
        public int ForBonus
        {
            get
            {
                int bonus = 0;
                if (ForBase < 5)
                {
                    bonus = -1;
                }
                else if (ForBase < 10)
                {
                    bonus = 0;
                }
                else if (ForBase < 15)
                {
                    bonus = 1;
                }
                else { bonus = 2; }
                return bonus;
            }
        }
        public int PvBase
        {
            get
            {
                return EndBase+EndBonus;
            }
        }
        public virtual int End
        {
            get
            {
                return EndBase+EndBonus;
            }
        }
        public virtual int For
        {
            get
            {
                return ForBase+ForBonus;
            }
        }
        public int PV
        {
            get { return _pv; }
            set { _pv=value; }
        }
        public int Gold
        {
            get
            {
                return _gold;
            }

            private set
            {
                _gold = value;
            }
        }
        public int Leather
        {
            get
            {
                return _leather;
            }

            private set
            {
                _leather = value;
            }
        }
        public bool Death
        {
            get
            {
                return (this.PV <= 0) ? true : false;
            }
        }
        #endregion
        #region Constructors
        private Character()
        {
            Dice sixFace = new Dice(6);
            this.X = 0;
            this.Y = 0;
            int result = sixFace.Roll(4, 3);
            this.EndBase = result;
            result = sixFace.Roll(4, 3);
            this.ForBase = result;
            this.PV = PvBase;
            this.Gold = 0;
            this.Leather = 0;
        }
        public Character(int X, int Y) : this()
        {
            this.X = X;
            this.Y = Y;
        }
        #endregion
        #region Methods

        #endregion
    }
}
