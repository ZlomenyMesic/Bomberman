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
        public static int level = 0;
        public static bool preventMultipleRestarts = true;
        public static List<int[]> levels = new List<int[]>
        {
            new int[] { 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0,
 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,
 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0,
 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2,
 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0,
 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,
 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0,
 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2,
 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0,
 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,
 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0 },
            new int[] { 0, 0, 0, 0, 0, 2, 0, 2, 0, 2, 0, 0, 0, 0, 0,
 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2,
 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0,
 2, 1, 2, 1, 0, 1, 2, 1, 2, 1, 0, 1, 2, 1, 2,
 0, 0, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 0, 0,
 2, 1, 2, 1, 0, 1, 2, 1, 2, 1, 0, 1, 2, 1, 2,
 0, 0, 0, 2, 0, 2, 0, 0, 0, 2, 0, 2, 0, 0, 0,
 2, 1, 2, 1, 0, 1, 2, 1, 2, 1, 0, 1, 2, 1, 2,
 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0,
 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2,
 0, 0, 0, 0, 0, 2, 0, 2, 0, 2, 0, 0, 0, 0, 0 },
            new int[] { 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2,
 2, 1, 0, 1, 2, 1, 2, 1, 2, 1, 2, 1, 0, 1, 2,
 2, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 2, 0, 2, 2,
 2, 1, 0, 1, 0, 1, 2, 1, 2, 1, 0, 1, 0, 1, 2,
 2, 2, 0, 2, 0, 2, 0, 0, 0, 2, 0, 2, 0, 2, 2,
 2, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 2,
 2, 2, 0, 2, 0, 2, 0, 0, 0, 2, 0, 2, 0, 2, 2,
 2, 1, 0, 1, 0, 1, 2, 1, 2, 1, 0, 1, 0, 1, 2,
 2, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 2, 0, 2, 2,
 2, 1, 0, 1, 2, 1, 2, 1, 2, 1, 2, 1, 0, 1, 2,
 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2 },
            new int[] { 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2,
 2, 1, 2, 1, 2, 1, 0, 1, 0, 1, 2, 1, 2, 1, 2,
 2, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 2,
 2, 1, 2, 1, 2, 1, 0, 1, 0, 1, 2, 1, 2, 1, 2,
 2, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 2,
 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1,
 2, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 2,
 2, 1, 2, 1, 2, 1, 0, 1, 0, 1, 2, 1, 2, 1, 2,
 2, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 2,
 2, 1, 2, 1, 2, 1, 0, 1, 0, 1, 2, 1, 2, 1, 2,
 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2 },
            new int[] { 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2,
 2, 1, 0, 1, 2, 1, 0, 1, 0, 1, 2, 1, 0, 1, 2,
 2, 2, 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0, 2, 2,
 2, 1, 0, 1, 0, 1, 2, 1, 2, 1, 0, 1, 0, 1, 2,
 2, 2, 0, 0, 0, 2, 2, 2, 2, 2, 0, 0, 0, 2, 2,
 2, 1, 0, 1, 2, 1, 2, 1, 2, 1, 2, 1, 0, 1, 2,
 2, 2, 0, 0, 0, 2, 2, 2, 2, 2, 0, 0, 0, 2, 2,
 2, 1, 0, 1, 0, 1, 2, 1, 2, 1, 0, 1, 0, 1, 2,
 2, 2, 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0, 2, 2,
 2, 1, 0, 1, 2, 1, 0, 1, 0, 1, 2, 1, 0, 1, 2,
 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2 }
        };

        /// <summary>
        /// Actions after a game object died
        /// Add score if a floater died, restart the game if the player died
        /// </summary>
        /// <param name="playerDied">true if the dead game object is a player</param>
        public static void Death(bool playerDied)
        {
            Score.Set(playerDied ? 0 : Score.score + 250);

            if (playerDied ) 
                Game.Restart(!playerDied);
        }

        /// <summary>
        /// Move the player and the floaters to their starting position depending on the next level
        /// </summary>
        /// <param name="newLevel">The next level</param>
        public static void LoadNewStartPositions(int newLevel)
        {
            if (newLevel == 0)
            {
                Game.eric = new(new Vector2(600, 530), true);
                Game.floater1 = new(new Vector2(150, 230), false);
                Game.floater2 = new(new Vector2(550, 230), false);
            }
            else if (newLevel == 1)
            {
                Game.eric = new(new Vector2(600, 530), true);
                Game.floater1 = new(new Vector2(200, 280), false);
                Game.floater2 = new(new Vector2(500, 280), false);
            }
            else if (newLevel == 2)
            {
                Game.eric = new(new Vector2(600, 530), true);
                Game.floater1 = new(new Vector2(350, 30), false);
                Game.floater2 = new(new Vector2(350, 430), false);
            }
            else if (newLevel == 3)
            {
                Game.eric = new(new Vector2(100, 530), true);
                Game.floater1 = new(new Vector2(350, 30), false);
                Game.floater2 = new(new Vector2(350, 130), false);
            }
            else if (newLevel == 4)
            {
                Game.eric = new(new Vector2(100, 530), true);
                Game.floater1 = new(new Vector2(100, 280), false);
                Game.floater2 = new(new Vector2(600, 280), false);
            }
        }
    }
    #endregion
}
