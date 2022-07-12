using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_HvsM.Class.Loot
{
    class Item
    {
        #region Fields
        private int _quantity;
        private double _weight;
        private int _size;
        #endregion
        #region Properties
        public int Quantity
        {
            get
            {
                return _quantity;
            }

            private set
            {
                _quantity = value;
            }
        }
        public double Weight
        {
            get
            {
                return _weight;
            }

            private set
            {
                _weight = value;
            }
        }
        public int Size
        {
            get
            {
                return _size;
            }

            private set
            {
                _size = value;
            }
        }
        public double TotalWeight
        {
            get { return Weight * Quantity; }
        }
        public int TotalSize
        {
            get { return Size * Quantity; }
        }
        #endregion
        #region Constructors
        private Item()
        {
            this.Quantity = 0;
            this.Weight = 0;
            this.Size = 0;
        }
        public Item(int Quantity) : this()
        {
            this.Quantity = Quantity;
        }
        #endregion
        #region Methods
        public bool AddItem(int Quantity)
        {
            bool added = false;
            this.Quantity += Quantity;
            return added;
        }
        public bool RemoveItem(int Quantity)
        {
            bool removed = false;
            this.Quantity -= Quantity;
            return removed;
        }
        #endregion
    }
}
