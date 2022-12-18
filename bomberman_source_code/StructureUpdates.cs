using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bomberman;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    #region Structure Updates
    internal static class StructureUpdates
    {
        /// <summary>
        /// Update the treasure and exit portal textures
        /// </summary>
        public static void UpdateTextures()
        {
            if ((Game.boardLayout[Game.treasure.position] == 0) && !Game.treasure.itemFound)
            {
                // Load the treasure texture

                Game.gameBoard[Game.treasure.position].ChangeType(BlockType.Treasure);
                Game.boardLayout[Game.treasure.position] = 5;
            }

            if ((Game.boardLayout[Game.exitPortal.position] == 0) && !Game.exitPortal.itemFound)
            {
                // Load the exit portal texture

                Game.gameBoard[Game.exitPortal.position].ChangeType(BlockType.ExitPortal);
                Game.boardLayout[Game.exitPortal.position] = 6;
            }

            if ((Game.boardLayout[Game.wheelchair.position] == 0) && !Game.wheelchair.itemFound && Game.wheelchair.itemGenerated)
            {
                // Load the wheelchair texture

                Game.gameBoard[Game.wheelchair.position].ChangeType(BlockType.Wheelchair);
                Game.boardLayout[Game.wheelchair.position] = 7;
            }
        }

        /// <summary>
        /// Check for collisions between the player and game items
        /// </summary>
        public static void CheckForCollision()
        {
            Game.treasure.CheckForPlayerCollision();
            Game.exitPortal.CheckForPlayerCollision();
            Game.wheelchair.CheckForPlayerCollision();
        }
    }
    #endregion
}
