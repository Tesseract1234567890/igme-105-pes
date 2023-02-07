using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE2GameFramework
{
    internal class Dice
    {
        private int totalDice;
        private int diceSides;

        public Dice(int totalDice, int diceSides)
        {
            this.totalDice = totalDice;
            this.diceSides = diceSides;
        }

        public void setDiceAmount(int amount)
        {
            this.totalDice = amount;
        }

        public void setDiceSides(int sides)
        {
            this.diceSides = sides;
        }
    }
}
