using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_HvsM.Class
{
    class Dice
    {
        #region Fields
        private int _min;
        private int _max;
        #endregion
        #region Properties
        public int Min
        {
            get
            {
                return _min;
            }

            private set
            {
                _min = value;
            }
        }
        public int Max
        {
            get
            {
                return _max;
            }

            private set
            {
                _max = value;
            }
        }
        public int Result
        {
            get
            {
                int Number;
                Random RandomGen = new Random(Guid.NewGuid().GetHashCode());
                Number = RandomGen.Next(this.Min, this.Max + 1);
                return Number;
            }
        }
        #endregion
        #region Constructors
        public Dice()
        {
            this.Min = 1;
            this.Max = 6;
        }
        public Dice(int max) : this()
        {
            this.Max = max;
        }
        public Dice(int min, int max) : this(max)
        {
            this.Min = min;
        }
        #endregion
        #region Methods
        public int Roll(int nbrDices, int nbrResult)
        {
            int FinalResult = 0;
            List<int> result_Dices = new List<int>();
            for (int i = 0; i < nbrDices; i++)
            {
                result_Dices.Add(this.Result);
            }
            result_Dices.Sort();
            for (int i = nbrDices-nbrResult; i < nbrDices; i++)
            {
                FinalResult += result_Dices[i];
            }
            return FinalResult;
        }
        #endregion
    }
}
