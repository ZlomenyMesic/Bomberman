using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bomberman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace Bomberman
{
    #region Block State Checking
    internal class BlockStates
    {
        /// <summary>
        /// Check if the block at the given coordinates is empty
        /// </summary>
        /// <param name="vector">XY board-relative coordinates</param>
        /// <param name="board">board layout</param>
        /// <returns>true if the block is empty, otherwise false</returns>
        public static bool IsFree(Vector2 vector, int[] board)
        {
            return board[VectorMath.CalculateBoardRelativePosition(vector)] == 0 ;
        }

        /// <summary>
        /// Check if the block at the given coordinates is outside the window borders
        /// </summary>
        /// <param name="vector">XY board-relative coordinates</param>
        /// <returns>true if the block is out of range, otherwise false</returns>
        public static bool IsOutOfRange(Vector2 vector)
        {
            return (vector.X > 15) || (vector.X < 1) || (vector.Y > 11) || (vector.Y < 1);
        }

        /// <summary>
        /// Check if the block at the given coordinates is a bomb
        /// </summary>
        /// <param name="vector">XY board-relative coordinates</param>
        /// <returns>true if the block is a bomb, otherwise false</returns>
        public static bool IsBomb(Vector2 vector)
        {
            return Game.boardLayout[VectorMath.CalculateBoardRelativePosition(vector)] == 3;
        }

        /// <summary>
        /// Check if the block at the given coordinates is a weak wall
        /// </summary>
        /// <param name="vector">XY board-relative coordinates</param>
        /// <returns>true if the block is a weak wall, otherwise false</returns>
        public static bool IsDestructable(Vector2 vector)
        {
            return Game.boardLayout[VectorMath.CalculateBoardRelativePosition(vector)] == 2;
        }

        /// <summary>
        /// Check if the block at the given coordinates can explode
        /// </summary>
        /// <param name="vector">XY board-relative coordinate</param>
        /// <returns>true if the block can explode, otherwise false</returns>
        public static bool CanExplode(Vector2 vector)
        {
            return !IsOutOfRange(vector) && (IsFree(vector, Game.boardLayout) || IsDestructable(vector));
        }

        /// <summary>
        /// Check if it's possible to walk through the block at the given coordinates
        /// </summary>
        /// <param name="vector">XY board-relative coordinates</param>
        /// <returns>true if it can be walked through, otherwise false</returns>
        public static bool CanBeWalkedThrough(Vector2 vector)
        {
            return new int[]{ 0, 3, 4, 5, 6, 7, 8}.Contains(Game.boardLayout[VectorMath.CalculateBoardRelativePosition(vector)]);
        }

        /// <summary>
        /// Check if the block at the given coordinates is an intersection
        /// </summary>
        /// <param name="vector">XY board-relative coordinates</param>
        /// <param name="board">board layout</param>
        /// <returns>true if the block is an intersection, otherwise false</returns>
        public static bool IsIntersection(Vector2 vector, int[] board)
        {
            bool check = true;
            int addX; int addY;

            for (int index = 0; index < 5; index++)
            {
                addX = 0; addY = 0;

                if (index == 1) addX = 1;
                else if (index == 2) addX = -1;
                else if (index == 3) addY = 1;
                else if (index == 4) addY = -1;

                if (!IsOutOfRange(new Vector2(vector.X + addX, vector.Y + addY)) && !IsFree(new Vector2(vector.X + addX, vector.Y + addY), board))
                    check = false;
            }
            return check;
        }
    }
    #endregion
}
