using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_HvsM.Class.Character
{
    class Knight:Heroes
    {
        #region Fields
        private int _endBase;
        private int _forBase;
        #endregion
        #region Properties
        public override int EndBase
        {
            get
            {
                return _endBase;
            }
            protected set
            {
                _endBase =value + 1;
            }
        }
        public override int ForBase
        {
            get
            {
                return _forBase;
            }
            protected set
            {
                _forBase = value + 1;
            }
        }
        #endregion
        #region Constructors
        public Knight(int Dungeon_Width, int Dungeon_Height, int X, int Y) : base(Dungeon_Width, Dungeon_Height, X,Y)
        {
        }
        #endregion
        #region Methods
        #endregion
    }
}
