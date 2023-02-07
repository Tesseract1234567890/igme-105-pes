using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE2GameFramework
{
    internal class Piece
    {
        private string owner;
        private string color;
        private string currentEffectName;
        private int currentEffectDuration;

        public Piece(string owner, string color, string currentEffectName, int currentEffectDuration)
        {
            this.owner = owner;
            this.color = color;
            this.currentEffectName = currentEffectName;
            this.currentEffectDuration = currentEffectDuration;
        }
    }
}
