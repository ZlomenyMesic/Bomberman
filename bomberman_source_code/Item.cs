﻿using Microsoft.Xna.Framework;
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
    #region Treasure
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

            // List all weak walls

            for (int index = 0; index < 165; index++)
                if ((Game.boardLayout[index] == 2) && (index != Game.treasure.position) && (index != Game.exitPortal.position) && (index != Game.wheelchair.position))
                    possibleBlocks.Add(index);

            // Choose a random one

            position = possibleBlocks[new Random().Next(0, possibleBlocks.Count - 1)];
        }

        /// <summary>
        /// TODO: write summary
        /// </summary>
        public void Collected()
        {
            if (!itemFound)
            {
                itemFound = true;
                Game.upgradeSound.Play();
                Game.boardLayout[position] = 0;
                Game.gameBoard[position].ChangeType(BlockType.Air);

                if (this.boardID == 5)
                    Score.Add((new Random().Next(16, 126)) * 10);
                else if (this.boardID == 7)
                    Score.Add((new Random().Next(16, 126)) * -10);
                else if (this.boardID == 6)
                {
                    LevelManager.levelText = $"CURRENT STAGE: {++LevelManager.level}";
                    Game.Restart(newLevel: true);
                }
            }
        }

        public void CheckForPlayerCollision()
        {
            Vector2 ericCoordinates = VectorMath.DivideVector(new Vector2(Game.eric.position.X + 25, Game.eric.position.Y + 25));

            // If the player is standing at the same block as the wheelchair, call Wheelchair.Collected()

            for (int index = 0; index < 165; index++)
                if ((Game.boardLayout[index] == boardID) && (index == VectorMath.CalculateBoardRelativePosition(ericCoordinates)))
                    this.Collected();
        }
    }
    #endregion
}