using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    #region Level Generating
    internal class LevelGeneration
    {
        /// <summary>
        /// Generate a new random board layout
        /// </summary>
        /// <returns>the new board layout</returns>
        public static int[] GenerateNewBoardLayout()
        {
            int[] temporaryLayout = new int[165];
            int[] newLayout = new int[165];

            // Strong Walls
            for (int index = 16; index < 150; index += 2)
            {
                if ((index % 15) == 0)
                    index += 16;

                newLayout[index] = 1;
                temporaryLayout[index] = 1;
            }

            // Weak Walls (30 % chance of creating a weak wall at each possible location)
            for (int index = 0; index < 165; index++)
                if ((temporaryLayout[index] != 1) && !BlockStates.IsIntersection(VectorMath.CalculateBoardVector(index), temporaryLayout) && (new Random().Next(0, 3) == 1))
                    newLayout[index] = 2;

            if (CheckNewBoardLayout(newLayout))
                return newLayout;
            else return new int[165];
        }

        /// <summary>
        /// Check if the new board layout contains an adequate amount of weak walls
        /// </summary>
        /// <param name="board">board layout</param>
        /// <returns>true if it does, otherwise false</returns>
        public static bool CheckNewBoardLayout(int[] board)
        {
            int totalWeakWalls = 0;

            foreach (int index in board)
                if (index == 2)
                    totalWeakWalls++;

            return (totalWeakWalls > 27) && (totalWeakWalls < 35);
        }
    }
    #endregion
}
