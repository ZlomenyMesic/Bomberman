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
using Microsoft.Xna.Framework.Media;
using System.Security.Principal;

namespace Bomberman
{
    #region Hidden Items
    public class Item
    {
        public int position = 0;
        public int boardID = 0;
        public bool itemFound = false;
        public bool itemGenerated = false;

        public Item(int newBoardID)
        {
            boardID = newBoardID;
        }

        /// <summary>
        /// List all weak walls, then choose one and hide the item inside.
        /// </summary>
        public void GenerateItem()
        {
            itemFound = false;
            itemGenerated = true;

            List<int> possibleBlocks = new List<int>();

            for (int index = 0; index < 165; index++)
                if ((Game.boardLayout[index] == 2) && (index != Game.treasure.position) && (index != Game.exitPortal.position) && (index != Game.wheelchair.position) && (index != Game.trap.position))
                    possibleBlocks.Add(index);

            position = possibleBlocks[new Random().Next(0, possibleBlocks.Count - 1)];
        }

        /// <summary>
        /// Erase the item and do something depending on the item
        /// </summary>
        public virtual void Collected()
        {
            itemFound = true;
            Game.upgradeSound.Play();
            Game.boardLayout[position] = 0;
            Game.gameBoard[position].ChangeType(BlockType.Air);
        }

        /// <summary>
        /// Check if the player is touching the item, if yes, call Item.Collected()
        /// </summary>
        public virtual void CheckForPlayerCollision()
        {
            Vector2 ericCoordinates = VectorMath.DivideVector(new Vector2(Game.eric.position.X + 25, Game.eric.position.Y + 25));

            for (int index = 0; index < 165; index++)
                if ((Game.boardLayout[index] == boardID) && (index == VectorMath.CalculateBoardRelativePosition(ericCoordinates)) && !itemFound)
                    this.Collected();
        }
    }
    #endregion
}
