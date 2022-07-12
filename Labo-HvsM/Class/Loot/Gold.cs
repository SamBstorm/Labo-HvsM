using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_HvsM.Class.Loot
{
    /*interface IGold
    {
    }*/
    class Gold:Item, IGold
    {
        #region Fields

        #endregion
        #region Properties

        #endregion
        #region Constructors
        public Gold() : base(0) { }
        public Gold(int Quantity) : base(Quantity) { }
        #endregion
        #region Methods

        #endregion
    }
}
