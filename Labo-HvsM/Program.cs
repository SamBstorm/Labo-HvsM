using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Labo_HvsM.Class;
using Labo_HvsM.Class.Character;
using Labo_HvsM.Class.Loot;
using Labo_HvsM.Ressources;

namespace Labo_HvsM
{
    class Program
    {

        static ConsoleColor[] Colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
        static void Main(string[] args)
        {
            bool Launch = false;
            Console.CursorVisible = false;
            do
            {
                Launch = false;
                int Dungeon_X = 50;
                int Dungeon_Y = 3;
                Console.SetCursorPosition(0, Dungeon_Y);
                Dungeon Donjon = new Dungeon();
                //int TypeHeroes = AskMenu("Choisissez votre classe", new List<string> { "Chevalier", "Nain" });
                int TypeHeroes = ShowMenu("Choisissez votre classe", new List<string> { "Chevalier", "Nain" },20,5);
                if (TypeHeroes != -1) 
                {
                    Launch = true;
                    Donjon.addHeroes(TypeHeroes);
                    Heroes Player = Donjon.Heroes;
                    do
                    {
                        Console.Clear();
                        showDungeon(Donjon, Dungeon_X, Dungeon_Y);
                        Console.SetCursorPosition(0, Dungeon_Y);
                        showPVBar(Player, 20, 85, 3);
                        showXPBar(Player, 20, 85, 5);
                        writeXY(showPlayerStats(Player), 85, 7);
                        ////////TOUJOURS UTILE POUR DEBUG LES MONSTRES///////////////////writeXY(showMonstersStats(Donjon.List_NPC),3,5);
                        int count_NPC = 0;
                        foreach (Monster NPC in Donjon.List_NPC)
                        {
                            if (!NPC.Death && NPC.SomebodyNear(Player))
                            {
                                showPVBar(NPC, 20, 3, 3 + (count_NPC * 12));
                                writeXY(showPlayerStats(NPC), 3, 5 + (count_NPC * 12));
                                count_NPC++;
                            }
                        }
                        showCharacter(Donjon, Dungeon_X, Dungeon_Y);
                        int idTouch = mapping();
                        if (idTouch > (int)Direction.Right && idTouch != (int)MappingTouch.Exit)
                        {
                            int InvTouch = 0;
                            int InvPosition;
                            do
                            {
                                InvPosition = ShowMenu("Inventaire", showInventory(Player), 5, 5);
                                if (InvPosition == -1) { InvTouch = (int)MappingTouch.Exit; }
                                else
                                {
                                    InvPosition--;
                                    if(Player.Inventory[InvPosition] is Equipment) {
                                        Equipment CurrentItem = Player.Inventory[InvPosition] as Equipment;
                                        if (CurrentItem.Set) {
                                            if (Player.UnEquip(InvPosition))
                                            {
                                                CurrentItem.ToggleSet();
                                            }
                                        }
                                        else
                                        {
                                            if (Player.Equip(InvPosition))
                                            {
                                                CurrentItem.ToggleSet();
                                            }
                                        }
                                    }
                                }
                            } while (InvTouch != (int)MappingTouch.Exit);
                        }
                        else
                        {
                            ///////////////////////////////////////////////////////////////////////////TOUR APRES MOUVEMENT//////////////////////////////////////////////////////
                            Random RandomGen = new Random(Guid.NewGuid().GetHashCode());
                            ///////////////Le hero bouge ou frappe
                            PointDungeon HeroesDestination = new PointDungeon(Player.setDestination(idTouch));
                            if (Donjon.HeroesCanMove(idTouch))
                            {
                                Player.Move(HeroesDestination);
                                foreach (Monster NPC in Donjon.List_NPC)
                                {
                                    if (NPC.SamePlace(Player))
                                    {
                                        if (Player.Loot(NPC))
                                        {
                                            writeXY("Chanceux!", 85, 25);
                                            Console.Beep(1024, 100);
                                        }
                                        Thread.Sleep(500);
                                    }
                                }
                            }
                            else
                            {
                                foreach (Monster NPC in Donjon.List_NPC)
                                {
                                    if (NPC.SamePlace(HeroesDestination))
                                    {
                                        int HitValue = Player.Fight(NPC);
                                        if (HitValue > 0)
                                        {
                                            writeXY("Frappe de " + HitValue + "!", 85, 25);
                                            Console.Beep(512, 100);
                                        }
                                        else
                                        {
                                            writeXY("Echec...", 85, 25);
                                        }
                                        Thread.Sleep(500);
                                    }
                                }
                            }
                            foreach (Monster NPC in Donjon.List_NPC)
                            {
                                if (!NPC.Death)
                                {
                                    bool CanHit = NPC.SomebodyNear(Player); ///on vérifie si le héro est sur une des 4 destinations possible : si oui on frappe, si non on bouge       
                                    if (CanHit == true)
                                    {
                                        int HitValue = NPC.Fight(Player);
                                        if (HitValue > 0)
                                        {
                                            writeXY("Frappe de " + HitValue + "!", 85, 25);
                                            Console.Beep(512, 100);
                                        }
                                        else
                                        {
                                            writeXY("Echec...", 85, 25);
                                        }
                                        Thread.Sleep(500);
                                    }
                                    else
                                    {
                                        bool CanMove = true;
                                        int idDirection = RandomGen.Next(0, 4);
                                        PointDungeon NPCDestination = new PointDungeon(NPC.setDestination(idDirection));
                                        foreach (Monster otherNPC in Donjon.List_NPC)
                                        {
                                            if (NPC != otherNPC)
                                            {
                                                if (otherNPC.SamePlace(NPCDestination) && !otherNPC.Death)
                                                {
                                                    CanMove = false;
                                                }
                                            }
                                        }
                                        if (CanMove == true)
                                        {
                                            NPC.Move(NPCDestination);
                                        }
                                    }
                                }
                            }
                        }
                    } while (!(Player.Death || Donjon.Nbr_NPC_alive == 0));
                    if (Player.Death == true) { Console.Clear(); string text = "Vous êtes mort : \n-----------------\n" + showPlayerStats(Player); writeXY(text, Console.WindowWidth / 2, 10); Console.ReadKey(); }
                    else { Console.Clear(); string text = "Vous avez Gagné : \n-----------------\n" + showPlayerStats(Player); writeXY(text, Console.WindowWidth / 2, 10); Console.ReadKey(); }
                }
            } while (Launch);
        }
        #region Functions
        public static string Titled(string TitleMenu)
        {
            string Result = "";
            foreach (char letter in TitleMenu)
            {
                Result+=("-");
            }
            Result += ("----\n");
            Result+=("| " + TitleMenu + " |\n");
            foreach (char letter in TitleMenu)
            {
                Result+=("-");
            }
            Result+=("----\n");
            return Result;
        }
        public static int AskMenu(string TitleMenu, List<string> ChoixMenu)
        {
            int choix_client = 0;
            Console.Clear();
            int countChoix = 0;
            do
            {
                writeXY(Titled(TitleMenu),20,5);
                foreach (string Choix in ChoixMenu)
                {
                    if (Choix.ToCharArray()[0] == '@')
                    {
                        Console.WriteLine("\n--" + Choix.Substring(1) + "--\n");
                    }
                    else
                    {
                        countChoix++;
                        Console.WriteLine(countChoix + " - " + Choix);
                    }
                }
                Console.Write("\nQuel est votre choix: ");
                choix_client = int.Parse(Console.ReadLine());
            } while (choix_client < 1 || choix_client > (countChoix));
            return choix_client;
        }
        public static int ShowMenu(string TitleMenu, List<string> ListMenu, int X, int Y)
        {
            string Menu = "";
            foreach (string Line in ListMenu)
            {
                Menu += Line + "\n";
            }
            Menu = Menu.Substring(0, Menu.Length - 1);
            int MenuPosition = ShowMenu(TitleMenu, Menu,X,Y);
            return MenuPosition;
        }
        public static int ShowMenu(string TitleMenu, string ListMenu, int X, int Y)
        {
            int InvTouch = 0;
            int MenuPosition = 0;            
            do
            {
                writeXY(Titled(TitleMenu),X,Y);
                string InvText = ListMenu;
                int LastLine= writeXY(InvText, MenuPosition, X, Y+3);
                InvTouch = mapping();
                switch (InvTouch)
                {
                    case (int)Direction.Up:
                        MenuPosition -= (MenuPosition == 0) ? 0 : 1;
                        break;
                    case (int)Direction.Down:
                        MenuPosition += (MenuPosition < LastLine-1) ? 1 : 0;
                        break;
                }
            } while (InvTouch != (int)MappingTouch.Validate && InvTouch !=(int)MappingTouch.Exit);
            MenuPosition = (InvTouch == (int)MappingTouch.Validate) ? MenuPosition + 1 :-1;
            return MenuPosition;
        }
        /////////////////////////////////////////////////CODE PERMETTANT LE MAPPAGE TOUCHE FLECHEE
        public static int mapping() {
            int idTouch=0;
            ConsoleKeyInfo cki = Console.ReadKey();
            if (cki.Key == ConsoleKey.UpArrow || cki.Key == ConsoleKey.NumPad8) { idTouch = (int)Direction.Up; }
            if (cki.Key == ConsoleKey.DownArrow || cki.Key == ConsoleKey.NumPad2) { idTouch = (int)Direction.Down; }
            if (cki.Key == ConsoleKey.LeftArrow || cki.Key == ConsoleKey.NumPad4) { idTouch = (int)Direction.Left; }
            if (cki.Key == ConsoleKey.RightArrow || cki.Key == ConsoleKey.NumPad6) { idTouch = (int)Direction.Right; }
            if (cki.Key == ConsoleKey.I || cki.Key == ConsoleKey.OemPlus) { idTouch = (int)MappingTouch.Inventory; }
            if (cki.Key == ConsoleKey.Enter || cki.Key == ConsoleKey.Spacebar) { idTouch = (int)MappingTouch.Validate; }
            if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.Backspace) { idTouch = (int)MappingTouch.Exit; }
            return idTouch;
        }
        ///////////////////////////AFFICHAGE////////////////////
        public static void writeXY(string WhatWrite, int X, int Y)
        {
            string[] Lines = WhatWrite.Split('\n');
            for (int line=0; line < Lines.Length; line++)
            {
                Console.SetCursorPosition(X, Y + line);
                Console.Write(Lines[line]);
            }
        }
        public static int writeXY(string WhatWrite,int selectedLine, int X, int Y)
        {
            string[] Lines = WhatWrite.Split('\n');
            int nbrLines = Lines.Length;
            for (int line = 0; line < Lines.Length; line++)
            {
                if (line == selectedLine)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.SetCursorPosition(X, Y + line);
                Console.Write(Lines[line]);
                Console.ResetColor();
            }
            return nbrLines;
        }
        public static void showDungeon(Dungeon Donjon, int X, int Y)
        {

            Console.BackgroundColor = ConsoleColor.White;
            string TopDownBorder = "";
            for (int X_donjon = -1; X_donjon < Donjon.Width; X_donjon++)
            {
                TopDownBorder += "  ";
            }
            Console.SetCursorPosition(X -1, Y - 1);
            Console.Write(TopDownBorder);
            Console.SetCursorPosition(X -1, Y + Donjon.Height);
            Console.Write(TopDownBorder);
            for (int Y_donjon = -1; Y_donjon <= Donjon.Height; Y_donjon++)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(X - 2, Y + (Y_donjon));
                Console.Write("  ");
                Console.SetCursorPosition(X +(Donjon.Width*2), Y + (Y_donjon));
                Console.Write("  ");
            }
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
        }
        public static void showCharacter(Dungeon Donjon, int X, int Y)
        {
            for (int x_Light = Donjon.Heroes.X - 1; x_Light <= Donjon.Heroes.X+1; x_Light++)
            {
                for (int y_Light = Donjon.Heroes.Y - 1; y_Light <= Donjon.Heroes.Y+1; y_Light ++)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    if (!(x_Light != Donjon.Heroes.X && y_Light != Donjon.Heroes.Y))
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    }
                    Console.SetCursorPosition(X+(x_Light*2),Y+y_Light);
                    if (x_Light >= 0 && x_Light < Donjon.Width && y_Light >= 0 && y_Light < Donjon.Height)
                    {
                        Console.Write("  ");
                    }
                }
            }
            Console.SetCursorPosition(X + (Donjon.Heroes.X * 2), Y + (Donjon.Heroes.Y));
            foreach (Monster NPC in Donjon.List_NPC)
            {
                /////////////////////////////////////////////REPETITION DU CODE NECESSAIRE POUR FAIRE APPARAITRE LES MORTS EN PREMIERS///////////////////
                /////////////////////////////////////////////CELA PERMET AUX MONSTRES VIVANTS OU LE HEROS D'APPARRAITRE PAR DESSUS...///////////////////
                if (NPC.Death)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.SetCursorPosition(X + (NPC.X * 2), Y + (NPC.Y));
                    switch (NPC.GetType().Name.ToString())
                    {
                        case "Wolf":
                            Console.Write("W ");
                            break;
                        case "Orc":
                            Console.Write("O ");
                            break;
                        case "Dragon_Small":
                            Console.Write("DS");
                            break;
                    }
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    if (NPC.SomebodyNear(Donjon.Heroes)) ///////////////////Si le monstre est à proximité du héro, l'afficher
                    {
                        Console.SetCursorPosition(X + (NPC.X * 2), Y + (NPC.Y));
                        switch (NPC.GetType().Name.ToString())
                        {
                            case "Wolf":
                                Console.Write("W ");
                                break;
                            case "Orc":
                                Console.Write("O ");
                                break;
                            case "Dragon_Small":
                                Console.Write("DS");
                                break;
                        }
                    }
                }
            }
            //////Affiche le carré bleu du héro
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(X + (Donjon.Heroes.X * 2), Y + (Donjon.Heroes.Y));
            Console.Write("  ");
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
        }
        public static void showBar(int Current, int Max, int Color, int BarSize, int X, int Y)
        {
            float nbrBlockPV= ((float)Current/ (float)Max)*BarSize;
            string EmptyBar = "[";
            for (int nbrBlock = 0; nbrBlock < BarSize; nbrBlock++)
            {
                EmptyBar += " ";
            }
            EmptyBar += "]";
            Console.SetCursorPosition(X, Y);
            Console.Write(EmptyBar);
            Console.BackgroundColor = Colors[Color];
            string PVBar="";
            for (int nbrBlock = 0; nbrBlock < (int)nbrBlockPV; nbrBlock++)
            {
                PVBar += " ";
            }
            Console.SetCursorPosition(X+1, Y);
            Console.Write(PVBar);
            Console.ResetColor();
        }
        public static void showPVBar(Person Character, int BarSize, int X, int Y)
        {
            float WarningZone = ((float)Character.PV / (float)Character.PvBase);
            if (WarningZone>0.5) {
                showBar(Character.PV, Character.PvBase, (int)ConsoleColor.Green, BarSize, X, Y);
            }
            else
            {
                showBar(Character.PV, Character.PvBase, (int)ConsoleColor.Red, BarSize, X, Y);
            }
        }
        public static void showXPBar(Person Character, int BarSize, int X, int Y)
        {
            int XPnextLVL =(Character.Lvl)*5;
            int XPcurrentLVL =(Character.Lvl-1)*5;
            showBar(Character.Exp-XPcurrentLVL, XPnextLVL-XPcurrentLVL,(int)ConsoleColor.Blue, BarSize, X, Y);
        }
        /////////////////////////DEBUG/////////////////////////
        public static string showPlayerStats(Person Player)
        {
            string result = "Position : " + Player.X + " - " + Player.Y + "\n";
            result += "Niveau : " + Player.Lvl + " - Exp : " + Player.Exp + "\n";
            result += "Endurance : " + Player.EndBase + "+" + Player.EndBonus + "+"+Player.ArmorBonus+"\n";
            result += "Force : " + Player.ForBase + "+" + Player.ForBonus + "+" + Player.WeaponBonus + "\n";
            result += "Points de vie : " + Player.PV + "/" + Player.PvBase + "\n";
            result += "Inventaire : \n----------------\n";
            result += showInventory(Player);
            return result;
        }
        public static string showInventory(Person Player)
        {
            string result = "";
            foreach (Item InInventory in Player.Inventory)
            {
                if (InInventory.Quantity != 0)
                {
                    if (InInventory is Equipment)
                    {
                        bool IsEquiped = false;
                        if (Player.Equiped != null)
                        {
                            foreach (Equipment Equiped in Player.Equiped)
                            {
                                IsEquiped = (InInventory == Equiped) ? true : IsEquiped;
                            }
                        }
                        result += (IsEquiped == true) ? "[E]" : "[ ]";
                        result += " - ";
                    }
                    else
                    {
                        result += InInventory.Quantity + " * ";
                    }
                    result += InInventory.GetType().Name.ToString() + "\n";
                }
            }
            result=(result!="")?result.Substring(0,result.Length-1):"Votre inventaire est vide...";
            return result;
        }
        public static string showMonstersStats(List<Monster> ListMonster)
        {
            string result="";
            foreach (Monster NPC in ListMonster)
            {
                result += NPC.GetType().Name.ToString() + " : " + NPC.X + " " + NPC.Y + " - " + NPC.PV + "/" + NPC.PvBase + " - " + NPC.Inventory[0].GetType().Name.ToString() + " :" + NPC.Inventory[0].Quantity + "\n";
            }
            return result;
        }
        #endregion
    }
}
