using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE2GameFramework
{
    internal class Player
    {
        private string name;
        private string color;
        private int turnPosition;
        private int pieceCount;
        private int startingPosition;
        private int currentPosition;
        private bool isHuman;

        private Piece[] pieces;

        public void setName(string name)
        {
            this.name = name;
        }

        public Player(string playerName, string playerColor, int pieceCount, bool isHuman, int startingPosition)
        {
            this.name = playerName;
            this.color = playerColor;
            this.turnPosition = 0;
            this.pieceCount = pieceCount;
            this.isHuman = isHuman;
            this.startingPosition = startingPosition;
        }

        public string getName()
        {
            return this.name;
        }

        public bool getIsHuman()
        {
            return this.isHuman;
        }

        public string getColor()
        {
            return this.color;
        }

        public int getStartingPosition()
        {
            return this.startingPosition;
        }

        private int _calculateDistanceFromStart()
        {
            if (currentPosition >= startingPosition)
            {
                return currentPosition - startingPosition;
            }
            else
            {
                // return currentPosition + boardSize - startingPosition
                return 0;
            }

        }

        public void setPosition(int position)
        {
            currentPosition = position;
        }
    }
}
