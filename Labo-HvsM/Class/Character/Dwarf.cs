using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_HvsM.Class.Character
{
    class Dwarf:Heroes
    {
        #region Fields
        private int _endBase;
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
                _endBase = value + 2;
            }
        }
        #endregion
        #region Constructors
        public Dwarf(int Dungeon_Width, int Dungeon_Height, int X, int Y) : base(Dungeon_Width, Dungeon_Height, X,Y)
        {

        }
        #endregion
        #region Methods
        #endregion
    }
}
