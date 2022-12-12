using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bomberman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Bomberman
{
    #region Bomb
    static class Bomb
    {
        private static int bombCountdown = -1;
        private static bool preventBombPlacement = false;

        /// <summary>
        /// Place a bomb on the player
        /// </summary>
        public static void Place()
        {
            Vector2 ericCoords = VectorMath.DivideVector(new Vector2(Game.eric.position.X + 25, Game.eric.position.Y + 25));

            // Check if it's possible to place a bomb at that location

            if (!BlockStates.IsOutOfRange(ericCoords) && BlockStates.IsFree(ericCoords) && !BlockStates.IsBomb(ericCoords) && (bombCountdown == -1) 
                && (!Game.boardLayout.Contains(3)) && (!Game.boardLayout.Contains(4)))
            {
                // Set the bomb countdown to 200 and change the block to a bomb

                bombCountdown = 200;
                Game.boardLayout[VectorMath.CalculateBoardRelativePosition(ericCoords)] = 3;
                Game.gameBoard[VectorMath.CalculateBoardRelativePosition(ericCoords)].ChangeType(BlockType.Bomb);
            }
        }

        /// <summary>
        /// Calculate a radius for the bomb explosion
        /// </summary>
        /// <param name="pos">The block where the bomb is placed at</param>
        /// <returns>A list with the blocks that will explode</returns>
        private static List<int> Radius(int pos)
        {
            List<int> destructableBlocks = new List<int>();
            Vector2 bombPos = VectorMath.CalculateBoardVector(pos);

            // Go from the middle to the sides, break the cycle if it hits a wall

            // 2 blocks left
            for (int index = (int)bombPos.X - 1; index > bombPos.X - 3; index--)
            {
                if (BlockStates.CanExplode(new Vector2(index, bombPos.Y)))
                    destructableBlocks.Add(VectorMath.CalculateBoardRelativePosition(new Vector2(index, bombPos.Y)));
                else break;
            }

            // 2 blocks right
            for (int index = (int)bombPos.X + 1; index < bombPos.X + 3; index++)
            {
                if (BlockStates.CanExplode(new Vector2(index, bombPos.Y)))
                    destructableBlocks.Add(VectorMath.CalculateBoardRelativePosition(new Vector2(index, bombPos.Y)));
                else break;
            }

            // 2 blocks up
            for (int index = (int)bombPos.Y - 1; index > bombPos.Y - 3; index--)
            {
                if (BlockStates.CanExplode(new Vector2(bombPos.X, index)))
                    destructableBlocks.Add(VectorMath.CalculateBoardRelativePosition(new Vector2(bombPos.X, index)));
                else break;
            }

            // 2 blocks down
            for (int index = (int)bombPos.Y + 1; index < bombPos.Y + 3; index++)
            {
                if (BlockStates.CanExplode(new Vector2(bombPos.X, index)))
                    destructableBlocks.Add(VectorMath.CalculateBoardRelativePosition(new Vector2(bombPos.X, index)));
                else break;
            }

            destructableBlocks.Add(VectorMath.CalculateBoardRelativePosition(bombPos));
            return destructableBlocks;
        }

        /// <summary>
        /// Create the smoke effect after an explosion
        /// </summary>
        private static void Explosion()
        {
            bombCountdown = -1;

            // Go through every block, if a block is a bomb, get the explosion radius and create the smoke

            for (int index = 0; index < 165; index++)
            {
                if (Game.boardLayout[index] == 3)
                {
                    foreach (int position in Radius(index))
                    {
                        Game.gameBoard[position].ChangeType(BlockType.Smoke);
                        Game.boardLayout[position] = 4;
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Clear the smoke effect
        /// </summary>
        private static void ClearExplosion()
        {
            // Go through every block, and change every smoke block to air

            for (int index = 0; index < 165; index++)
            {
                if (Game.boardLayout[index] == 4)
                {
                    Game.gameBoard[index].ChangeType(BlockType.Air);
                    Game.boardLayout[index] = 0;
                }
            }
        }

        /// <summary>
        /// Bomb timers, used to calculate time to the explosion and how long will the smoke last
        /// </summary>
        public static void BombCountdown()
        {
            if (Game.boardLayout.Contains(4))
            {
                // If a bomb has exploded, count down from 60 and then clear the smoke
                // bombCountdown is -1 when there isn't any bomb

                if ((bombCountdown <= 0) && !preventBombPlacement)
                {
                    preventBombPlacement = true;
                    bombCountdown = 60;
                }
                else bombCountdown--;

                if (bombCountdown == 0)
                {
                    preventBombPlacement = false;
                    ClearExplosion();
                }
            }
            else
            {
                // If a bomb is placed, count down from 200 and then explode

                if (bombCountdown != -1) 
                    bombCountdown--;

                if (bombCountdown == 0)
                {
                    Game.bombExplosion.Play();
                    Explosion();
                }
            }
        }

        /// <summary>
        /// Check if the game object is touching a smoke block, if yes, kill it
        /// </summary>
        /// <param name="gameObject">Game object reference</param>
        public static void CheckForDeath(ref GameObject gameObject)
        {
            Vector2 gameObjectCoordinates = VectorMath.DivideVector(new Vector2(gameObject.position.X + 25, gameObject.position.Y + 25));
            
            for (int index = 0; index < 165; index++)
                if ((Game.boardLayout[index] == 4) && (index == VectorMath.CalculateBoardRelativePosition(gameObjectCoordinates)))
                    gameObject.Kill();
        }

        /// <summary>
        /// Reset the bomb class variabless, used when restarting the game
        /// </summary>
        public static void ResetCountdowns()
        {
            bombCountdown = -1;
            preventBombPlacement = false;
        }
    }
    #endregion
}
