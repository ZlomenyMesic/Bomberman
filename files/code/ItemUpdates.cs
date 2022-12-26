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
    #region Item Updates
    internal static class ItemUpdates
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

            if ((Game.boardLayout[Game.trap.position] == 0) && !Game.trap.itemFound && Game.trap.itemGenerated)
            {
                // Load the trap texture

                Game.gameBoard[Game.trap.position].ChangeType(BlockType.Trap);
                Game.boardLayout[Game.trap.position] = 8;
            }
        }

        /// <summary>
        /// Check for collisions between the game objects and game items
        /// </summary>
        public static void CheckForCollision()
        {
            Game.treasure.CheckForPlayerCollision();
            Game.exitPortal.CheckForPlayerCollision();
            Game.wheelchair.CheckForPlayerCollision();

            Game.trap.CheckForPlayerCollision(ref Game.eric);
            Game.trap.CheckForPlayerCollision(ref Game.floater1);
            Game.trap.CheckForPlayerCollision(ref Game.floater2);
        }
    }
    #endregion
}
