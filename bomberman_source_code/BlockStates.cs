using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bomberman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int num = Game.boardLayout[VectorMath.CalculateBoardRelativePosition(vector)];
            return (num == 0) || (num == 3) || (num == 4) || (num == 5) || (num == 6) || (num == 7);
        }

        /// <summary>
        /// Check if the block at the given coordinates is an intersection
        /// </summary>
        /// <param name="vector">XY board-relative coordinates</param>
        /// <param name="board">board layout</param>
        /// <returns>true if the block is an intersection, otherwise false</returns>
        public static bool IsIntersection(Vector2 vector, int[] board)
        {
            return (IsOutOfRange(new Vector2(vector.X + 1, vector.Y)) || IsFree(new Vector2(vector.X + 1, vector.Y), board))
                && (IsOutOfRange(new Vector2(vector.X - 1, vector.Y)) || IsFree(new Vector2(vector.X - 1, vector.Y), board))
                && (IsOutOfRange(new Vector2(vector.X, vector.Y + 1)) || IsFree(new Vector2(vector.X, vector.Y + 1), board))
                && (IsOutOfRange(new Vector2(vector.X, vector.Y - 1)) || IsFree(new Vector2(vector.X, vector.Y - 1), board))
                && !IsOutOfRange(vector) && IsFree(vector, board);
        }
    }
    #endregion
}
