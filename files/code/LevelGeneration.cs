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
        public static int level = 1;
        public static string levelText = "CURRENT STAGE: 1";

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

        /// <summary>
        /// Move the player and the floaters to their new random starting positions
        /// </summary>
        /// <param name="newLevel">The new board</param>
        public static void LoadNewStartPositions()
        {
            List<int> possibleSpawnPoints = new List<int>();

            for (int index = 0; index < 165; index++)
                if (BlockStates.IsIntersection(VectorMath.CalculateBoardVector(index), Game.boardLayout))
                    possibleSpawnPoints.Add(index);

            int ericPosition = possibleSpawnPoints[new Random().Next(0, possibleSpawnPoints.Count - 1)];

            // Prevent spawning the floaters too close to the player

            List<int> possibleFloaterSpawnPoints = possibleSpawnPoints.Except(new List<int> { ericPosition, ericPosition + 2, ericPosition + 4, ericPosition - 2, ericPosition - 4, ericPosition + 30, ericPosition + 60, ericPosition - 30, ericPosition - 60, ericPosition + 32, ericPosition + 28, ericPosition - 32, ericPosition - 28 }).ToList();

            Game.eric = new GameObject(VectorMath.CalculateActualVector(ericPosition), true);
            Game.floater1 = new GameObject(VectorMath.CalculateActualVector(possibleFloaterSpawnPoints[new Random().Next(0, possibleFloaterSpawnPoints.Count)]), false);
            Game.floater2 = new GameObject(VectorMath.CalculateActualVector(possibleFloaterSpawnPoints[new Random().Next(0, possibleFloaterSpawnPoints.Count)]), false);
        }
    }
    #endregion
}
