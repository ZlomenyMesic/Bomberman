using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bomberman;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Bomberman
{
    #region Game Level Manager
    internal static class LevelManager
    {
        public static int level = 1;
        public static string levelText = "CURRENT STAGE: 1";

        /// <summary>
        /// Actions after a game object died.
        /// Add score if a floater died, restart the game if the player died
        /// </summary>
        /// <param name="playerDied">true if the dead game object is a player</param>
        public static void Death(bool playerDied)
        {
            Score.Set(playerDied ? 0 : Score.score + (new Random().Next(1, 20)) * 10);

            if (playerDied)
                Game.Restart(newLevel: false);
        }

        /// <summary>
        /// Move the player and the floaters to their starting position depending on the new board
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

            List<int> tooClose = new List<int> { ericPosition, ericPosition + 2, ericPosition + 4, ericPosition - 2, ericPosition - 4, ericPosition + 30, ericPosition + 60, ericPosition - 30, ericPosition - 60, ericPosition + 32, ericPosition + 28, ericPosition - 32, ericPosition - 28};
            possibleSpawnPoints = possibleSpawnPoints.Except(tooClose).ToList();

            int floaterPosition1 = possibleSpawnPoints[new Random().Next(0, possibleSpawnPoints.Count - 1)];
            possibleSpawnPoints.Remove(floaterPosition1);
            int floaterPosition2 = possibleSpawnPoints[new Random().Next(0, possibleSpawnPoints.Count - 1)];

            Game.eric = new GameObject(VectorMath.CalculateActualVector(ericPosition), true);
            Game.floater1 = new GameObject(VectorMath.CalculateActualVector(floaterPosition1), false);
            Game.floater2 = new GameObject(VectorMath.CalculateActualVector(floaterPosition2), false);
        }
    }
    #endregion
}
