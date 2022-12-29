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
        /// Update all item textures
        /// </summary>
        public static void UpdateTextures()
        {
            Item[] items = new Item[] { Game.treasure, Game.exitPortal, Game.wheelchair, Game.trap };

            foreach (Item item in items)
            {
                if ((Game.boardLayout[item.position] == 0) && item.itemGenerated && !item.itemFound)
                {
                    Game.gameBoard[item.position].ChangeType(BlockUtilities.ConvertToBlockType(item.boardID));
                    Game.boardLayout[item.position] = item.boardID;
                }
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
