using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_HvsM.Class.Character
{
    class Point2D
    {
        #region Fields
        private int _x;
        private int _y;
        #endregion
        #region Properties
        public virtual int X
        {
            get
            {
                return _x;
            }

            set
            {
                _x = value;
            }
        }
        public virtual int Y
        {
            get
            {
                return _y;
            }

            set
            {
                _y = value;
            }
        }
        #endregion
        #region Constructors
        public Point2D()
        {
            this.X=0;
            this.Y=0;
        }
        public Point2D(int newX, int newY) : this()
        {
            this.X = newX;
            this.Y = newY;
        }
        #endregion
        #region Methods
        public bool SamePlace(Point2D CoordPlace)
        {
            bool ItIs = (this.X == CoordPlace.X && this.Y == CoordPlace.Y) ? true : false;
            return ItIs;
        }
        #endregion


    }
}
