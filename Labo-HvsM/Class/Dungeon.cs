using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labo_HvsM.Class.Character;
using Labo_HvsM.Class.Loot;
using Labo_HvsM.Ressources;

namespace Labo_HvsM.Class
{
    class Dungeon
    {
        #region Fields
        private int _width;
        private int _height;
        private int _nbr_NPC;
        private List<Monster> _list_NPC;
        private Heroes _heroes;
        #endregion
        #region Properties
        public int Width
        {
            get
            {
                return _width;
            }

            set
            {
                _width = value;
            }
        }
        public int Height
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value;
            }
        }
        public int Nbr_NPC
        {
            get
            {
                return _nbr_NPC;
            }

            set
            {
                _nbr_NPC = value;
            }
        }
        public Heroes Heroes
        {
            get
            {
                return _heroes;
            }

            private set
            {
                _heroes = value;
            }
        }
        internal List<Monster> List_NPC
        {
            get
            {
                return _list_NPC;
            }

            private set
            {
                _list_NPC = value;
            }
        }
        public int Nbr_NPC_alive
        {
            get {
                int Number = 0;
                foreach (Monster NPC in this.List_NPC)
                {
                    if (!NPC.Death)
                    {
                        Number++;
                    }
                }
                return Number; }
        }
        #endregion
        #region Constructors
        public Dungeon()
        {
            Random RandomGen = new Random(Guid.NewGuid().GetHashCode());
            this.Width = 15;
            this.Height = 15;
            this.Nbr_NPC = 10;
            for (int countMonster = 0; countMonster < Nbr_NPC; countMonster++)
            {
                bool canPlace = true;
                int X = RandomGen.Next(0, this.Width);
                int Y = RandomGen.Next(0, this.Height);
                if (this.List_NPC !=null)
                {
                    foreach (Monster NPC in List_NPC)
                    {
                        if((X>=NPC.X-1 && X<=NPC.X+1) && (Y >= NPC.Y-1 && Y<=NPC.Y+1)) { canPlace = false; }
                    }
                }
                else { this.List_NPC = new List<Monster>(); }
                /*Si le Heros est placer avant, mais c'est pas le cas...if (X == this.Heroes.X && Y == this.Heroes.Y) { canPlace = false; }*/
                if (canPlace == false) { countMonster--; }
                else {
                    /*choix aléatoire du type de monstre*/
                    int MaxNPCType = Enum.GetNames(typeof(eMonster)).Length;
                    int NPCType = RandomGen.Next(0, MaxNPCType);
                    Monster newMonster = null;
                    switch (NPCType)
                    {
                        case (int)eMonster.Wolf:
                            newMonster = new Wolf(this.Width, this.Height,X, Y);
                            break;
                        case (int)eMonster.Orc:
                            newMonster = new Orc(this.Width, this.Height, X, Y);
                            break;
                        case (int)eMonster.Dragon_Small:
                            newMonster = new Dragon_Small(this.Width, this.Height, X, Y);
                            break;
                    }
                    this.List_NPC.Add(newMonster);
                }
            }
        }
        public Dungeon(int Width, int Height):this() {
            this.Width = Width;
            this.Height = Height;
            this.Nbr_NPC = 10;
        }
        public Dungeon(int Width, int Height, int Nbr_NPC):this(Width, Height)
        {
            this.Nbr_NPC = Nbr_NPC;
        }
        #endregion
        #region Methods
        public bool addHeroes(int TypeHeroes)
        {
            Heroes Player = null;
            Random RandomGen = new Random(Guid.NewGuid().GetHashCode());
            bool CanPlace;
            int X, Y;
            do
            {
                CanPlace = true;
                X = RandomGen.Next(0, this.Width);
                Y = RandomGen.Next(0, this.Height);
                Console.WriteLine(X+"-"+Y);/////////////////////////////DEBUG
                foreach (Monster NPC in this.List_NPC)
                {
                    if ((X >= NPC.X - 1 && X <= NPC.X + 1) && (Y >= NPC.Y - 1 && Y <= NPC.Y + 1))
                    {
                        CanPlace = false;
                    }
                }
            } while (!CanPlace);
            switch (TypeHeroes)
            {
                case 1:
                    Player = new Knight(this.Width, this.Height, X, Y);
                    break;
                case 2:
                    Player = new Dwarf(this.Width, this.Height, X, Y);
                    break;
            }
            this.Heroes = Player;
            return true;
        }
        public bool HeroesCanMove(int idTouch)
        {
            int DestX=this.Heroes.X;
            int DestY=this.Heroes.Y;
            bool CanMove=true;
            switch (idTouch)
            {
                case 0:
                    DestY--;
                    break;
                case 1:
                    DestY++;
                    break;
                case 2:
                    DestX--;
                    break;
                case 3:
                    DestX++;
                    break;
            }
            foreach (Monster NPC in this.List_NPC)
            {
                if (DestX==NPC.X && DestY == NPC.Y && !NPC.Death)
                {
                    CanMove = false;
                }
            }
            return CanMove;
        }
        /*public List<bool> NPCCanMove()
        {
            List<bool> CanMove = new List<bool>();
            foreach (Monster NPC in this.List_NPC)
            {
                bool NPC_canMove = true;
                for (int DestX = NPC.X -1; DestX <= NPC.X+1; DestX++)
                {
                    for (int DestY = NPC.Y-1; DestY <= NPC.Y+1; DestY++)
                    {
                        foreach (Monster otherNPC in this.List_NPC)
                        {
                            if (NPC != otherNPC)
                            {
                                if (DestX == otherNPC.X && DestY == otherNPC.Y) { NPC_canMove = false; }
                            }
                        }
                    }
                }
                CanMove.Add(NPC_canMove);
                /*if (DestX == NPC.X && DestY == NPC.Y)
                {
                    CanMove = false;
                }
            }
            return CanMove;
        }*/
        public bool Action(int idTouch)
        {
            
            //////////////Tout les monstres bougent un à un sauf si il y a un héro ou un autre NPC...
            
            return true;
        }        
        #endregion
    }
}
