using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labo_HvsM.Class;
using Labo_HvsM.Ressources;
using Labo_HvsM.Class.Loot;

namespace Labo_HvsM.Class.Character
{
    abstract class Person
    {
        #region Fields
        private PointDungeon _coord;
        private int _endBase;
        private int _forBase;
        private int _pv;
        private int _exp;
        private int _lvl;
        private List<Item> _inventory;
        private List<Equipment> _equiped;
        #endregion
        #region Properties
        public int X
        {
            get
            {
                return Coord.X;
            }
        }
        public int Y
        {
            get
            {
                return Coord.Y;
            }
        }
        public virtual int EndBase
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
        public virtual int ForBase
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
                bonus += this.Lvl / 5;
                bonus += ArmorBonus;
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
                bonus += this.Lvl / 5;
                return bonus;
            }
        }
        public int ArmorBonus
        {
            get
            {
                int bonus = 0;
                if (Equiped != null)
                {
                    for (int i=0; i<this.Equiped.Count();i++)
                    {
                        Equipment ItemCompared = this.Equiped[i] as Equipment;
                        if (ItemCompared is Armor)
                        {
                            Armor Armor_Found = ItemCompared as Armor;
                            bonus += Armor_Found.ArmorBonus;
                        }
                    }
                }
                return bonus;
            }
        }
        public int WeaponBonus
        {
            get
            {
                int bonus = 0;
                if (Equiped != null)
                {
                    for (int i = 0; i < this.Equiped.Count(); i++)
                    {
                        Equipment ItemCompared = this.Equiped[i] as Equipment;
                        if (ItemCompared is Weapon)
                        {
                            Weapon Weapon_Found = ItemCompared as Weapon;
                            bonus += Weapon_Found.WeaponBonus;
                        }
                    }
                }
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
        public int PV
        {
            get { return _pv; }
            set { _pv = (value<0)?0:(value>PvBase)?PvBase:value; }
        }
        public bool Death
        {
            get
            {
                return (this.PV <= 0) ? true : false;
            }
        }
        internal PointDungeon Coord
        {
            get
            {
                return _coord;
            }

            private set
            {
                _coord = value;
            }
        }
        internal List<Item> Inventory
        {
            get
            {
                return _inventory;
            }

            private set
            {
                _inventory = value;
            }
        }
        public int Exp
        {
            get
            {
                return _exp;
            }

            private set
            {
                _exp = value;
            }
        }
        public int Lvl
        {
            get
            {
                int level = (Exp / 5)+1;
                return level;
            }
        }
        internal List<Equipment> Equiped
        {
            get
            {
                return _equiped;
            }

            set
            {
                _equiped = value;
            }
        }
        #endregion
        #region Constructors
        private Person()
        {
            Dice sixFace = new Dice(6);
            int result = sixFace.Roll(4, 3);
            this.EndBase = result;
            result = sixFace.Roll(4, 3);
            this.ForBase = result;
            this.PV = PvBase;
            this.Coord = new PointDungeon(0,0,10,10);
            this.Inventory = new List<Item>();
            this.Equiped = new List<Equipment>();

        }
        public Person(int Dungeon_Width, int Dungeon_Height,int X, int Y) : this()
        {
            this.Exp = 0;
            this.Coord=new PointDungeon(X, Y, Dungeon_Width, Dungeon_Height);
        }
        #endregion
        #region Methods
        public PointDungeon setDestination(int idDirection)
        {
            PointDungeon Destination = new PointDungeon(this.Coord);
            switch (idDirection)
            {
                case (int)Direction.Up:
                    Destination.Y--;
                    break;
                case (int)Direction.Down:
                    Destination.Y++;
                    break;
                case (int)Direction.Left:
                    Destination.X--;
                    break;
                case (int)Direction.Right:
                    Destination.X++;
                    break;
            }
            return Destination;
        }
        public bool Move(int idDirection)
        {
            bool Moved = false;
            if (!this.Death)
            {
                switch (idDirection)
                {
                    case (int)Direction.Up:
                        this.Coord.Y--;
                        break;
                    case (int)Direction.Down:
                        this.Coord.Y++;
                        break;
                    case (int)Direction.Left:
                        this.Coord.X--;
                        break;
                    case (int)Direction.Right:
                        this.Coord.X++;
                        break;
                }
                Moved = true;
            }
            return Moved;
        }
        public bool Move(PointDungeon Destination)
        {
            bool Moved = false;
            if (!this.Death)
            {
                this.Coord = Destination;
                Moved = true;
            }
            return Moved;
        }
        public int Hit() {
            Dice fourFaces = new Dice(4);
            int HitValue = fourFaces.Result + this.ForBonus+this.WeaponBonus;
            return HitValue;
        }
        public bool beHit(int HitValue)
        {
            this.PV -= (HitValue-this.ArmorBonus);
            return true;
        }
        public bool Heal()
        {
            this.PV = this.PvBase;
            return true;
        }
        public bool Heal(int nbrPV)
        {
            this.PV += nbrPV;
            return true;
        }
        public bool SamePlace(Point2D CoordPlace) {
            bool ItIs = (this.X==CoordPlace.X && this.Y==CoordPlace.Y)? true :false;            
            return ItIs;
        }
        public bool SamePlace(Person CoordPerson)
        {
            bool ItIs = (this.X == CoordPerson.X && this.Y == CoordPerson.Y) ? true : false;
            return ItIs;
        }
        public int Fight(Person Opponent)
        {
            int HitValue = this.Hit();
            if (HitValue > 0)
            {
                Opponent.beHit(HitValue);
                if (Opponent.Death) { this.Exp += (Opponent.EndBonus + Opponent.ForBonus)*3; this.Heal(); }
            }
            return HitValue;
        }
        public bool Equip(Equipment NewItem)
        {
            bool IsEquiped = false;
            foreach (Equipment OldItem in this.Equiped)
            {
                if (NewItem.GetType() != OldItem.GetType()) {
                    this.Equiped.Add(NewItem);
                    IsEquiped = true;
                }
            }
            return IsEquiped;
        }
        public bool Equip(int Id_Inv_Item)
        {
            bool IsEquiped = false;
            Equipment NewItem = this.Inventory[Id_Inv_Item] as Equipment;
            if (this.Equiped.Count()!=0)
            {
                for (int i=0;i< this.Equiped.Count();i++)
                {
                    Equipment OldItem = this.Equiped[i];
                    if (NewItem.GetType() != OldItem.GetType())
                    {
                        this.Equiped.Add(NewItem);
                        IsEquiped = true;
                    }
                }
            }
            else {
                this.Equiped.Add(NewItem);
                IsEquiped = true;
            }
            return IsEquiped;
        }
        public bool UnEquip(int Id_Equipment)
        {
            bool IsUnEquiped = false;
            //this.Inventory.Add(this.Equiped[Id_Equipment]);
            for (int i = 0; i < this.Equiped.Count(); i++)
            {
                if (this.Inventory[Id_Equipment] == this.Equiped[i] && IsUnEquiped == false)
                {
                    this.Equiped.Remove(this.Equiped[i]);
                    IsUnEquiped = true;
                }
            }
            return IsUnEquiped;
        }
        #endregion
    }
}
