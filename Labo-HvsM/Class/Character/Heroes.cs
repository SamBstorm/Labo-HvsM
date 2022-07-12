using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labo_HvsM.Class.Loot;
using Labo_HvsM.Ressources;

namespace Labo_HvsM.Class.Character
{
    abstract class Heroes:Person
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
                _endBase = value;
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
                _forBase = value;
            }
        }
        #endregion
        #region Constructors
        public Heroes(int Dungeon_Width, int Dungeon_Height, int X, int Y) : base(Dungeon_Width,Dungeon_Height,X,Y)
        {
            this.Inventory.Add(new Sword());
            this.Inventory.Add(new Helmet());
        }
        #endregion
        #region Methods
        public bool Loot (Monster Monster)
        {
            bool Looted = false;

            foreach (Item ItemLoot in Monster.Inventory)
            {
                Item SelectedItem = null;
                foreach (Item currentItem in this.Inventory)
                {
                    if (ItemLoot.GetType().Name == currentItem.GetType().Name)
                    {
                        SelectedItem = currentItem;
                    }
                }

                if(SelectedItem == null)
                {
                    SelectedItem = Activator.CreateInstance(ItemLoot.GetType()) as Item;
                    this.Inventory.Add(SelectedItem);
                }
                
                if (SelectedItem != null)
                {
                    SelectedItem.AddItem(ItemLoot.Quantity);
                    ItemLoot.RemoveItem(ItemLoot.Quantity);

                    Looted = true;
                }
            }
            return Looted;
        }
        #endregion
    }
}
