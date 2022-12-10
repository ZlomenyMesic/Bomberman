using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bomberman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Bomberman
{
    #region Treasure
    public static class Treasure
    {
        public static int treasurePosition;
        public static bool treasureFound = false;
        public static bool treasureLoadCheck = false;

        /// <summary>
        /// List all weak walls, then choose one and hide the treasure inside
        /// </summary>
        public static void GenerateTreasure()
        {
            treasureFound = false;
            treasureLoadCheck = false;

            List<int> possibleBlocks = new List<int>();

            // List all weak walls

            for (int index = 0; index < 165; index++)
                if (Game.boardLayout[index] == 2)
                    possibleBlocks.Add(index);

            // Choose a random one

            treasurePosition = possibleBlocks[new Random().Next(0, possibleBlocks.Count - 1)];
        }

        /// <summary>
        /// Erase the treasure and add score 500 to the player
        /// </summary>
        public static void Collected()
        {
            if (!treasureFound)
            {
                treasureFound = true;
                Game.boardLayout[treasurePosition] = 0;
                Game.gameBoard[treasurePosition].ChangeType(BlockType.Air);
                Score.Add(500);
            }
        }

        /// <summary>
        /// Check if the player is touching the treasure
        /// </summary>
        public static void CheckForPlayerCollision()
        {
            Vector2 ericCoordinates = VectorMath.DivideVector(new Vector2(Game.eric.position.X + 25, Game.eric.position.Y + 25));

            // If the player is standing at the same block as the treasure, call Treasure.Collected()

            for (int index = 0; index < 165; index++)
                if ((Game.boardLayout[index] == 5) && (index == VectorMath.CalculateBoardRelativePosition(ericCoordinates)))
                    Treasure.Collected();
        }
    }
    #endregion
}
