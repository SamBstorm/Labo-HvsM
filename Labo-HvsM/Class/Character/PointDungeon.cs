using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_HvsM.Class.Character
{
    class PointDungeon:Point2D
    {
        #region Fields
        private int _x;
        private int _y;
        private int _limitX;
        private int _limitY;
        #endregion
        #region Properties
        public override int X
        {
            get
            {
                return _x;
            }

            set
            {
                if (value >= 0 && value < _limitX)
                {
                    _x = value;
                }
            }
        }
        public override int Y
        {
            get
            {
                return _y;
            }

            set
            {
                if (value >= 0 && value < _limitY)
                {
                    _y = value;
                }
            }
        }
        #endregion
        #region Constructors
        public PointDungeon(int X, int Y, int LimitX, int LimitY) : base()
        {
            this._limitX = LimitX;
            this._limitY = LimitY;
            this.X = X;
            this.Y = Y;
        }
        public PointDungeon(PointDungeon Coord) : base()
        {
            this._limitX = Coord._limitX;
            this._limitY = Coord._limitY;
            this.X = Coord.X;
            this.Y = Coord.Y;
        }
        #endregion


    }
}
