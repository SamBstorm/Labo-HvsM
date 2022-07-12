using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labo_HvsM.Class.Loot;
using Labo_HvsM.Ressources;

namespace Labo_HvsM.Class.Character
{
    class Monster:Person
    {
        #region Fields
        #endregion
        #region Properties
        #endregion
        #region Constructors
        public Monster(int Dungeon_Width, int Dungeon_Height, int X, int Y) : base(Dungeon_Width, Dungeon_Height, X, Y) {
            Dice fourFaces = new Dice(4);
            Dice sixFaces = new Dice(6);
            if (this is IGold)
            {
                Inventory.Add(new Gold(sixFaces.Result));
            }
            if (this is ILeather)
            {
                Inventory.Add(new Leather(fourFaces.Result));
            }            
        }
        #endregion
        #region Methods
        public bool SomebodyNear(Person Somebody)
        {
            bool IsNear = false;
            for (int idDirection = 0; idDirection < 4; idDirection++)
            {
                if (Somebody.SamePlace(this.setDestination(idDirection))) { IsNear = true; }
            }
            return IsNear;
        }
        #endregion
    }
}
