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
    #region Vector Math
    internal class VectorMath : Microsoft.Xna.Framework.Game
    {
        /// <summary>
        /// Convert XY window relative coordinates to a single number
        /// </summary>
        /// <param name="vector">XY window-relative coordinates</param>
        /// <returns>boardLayout position</returns>
        public static int CalculateBoardPosition(Vector2 vector)
        {
            return (int)((((vector.Y / 50) - 1) * 15) + (vector.X / 50) - 1);
        }

        /// <summary>
        /// Convert XY board-relative to a single number
        /// </summary>
        /// <param name="vector">XY board-relative coordinates</param>
        /// <returns>boardLayout position</returns>
        public static int CalculateBoardRelativePosition(Vector2 vector)
        {
            return (int)(((vector.Y - 1) * 15) + vector.X - 1);
        }

        /// <summary>
        /// Convert a single number to XY window-relative coordinates
        /// </summary>
        /// <param name="boardPosition">boardLayout position</param>
        /// <returns>XY window-relative coordinates</returns>
        public static Vector2 CalculateActualVector(int boardPosition)
        {
            int x = (boardPosition % 15) + 1;
            int y = (boardPosition + 15 - (boardPosition % 15)) / 15;
            return new Vector2((x - 1) * 50, (y - 1) * 50 + 30);
        }

        /// <summary>
        /// Convert a single number to XY board-relative coordinates
        /// </summary>
        /// <param name="boardPosition">boardLayout position</param>
        /// <returns>XY board-relative coordinates</returns>
        public static Vector2 CalculateBoardVector(int boardPosition)
        {
            int x = (boardPosition % 15) + 1;
            int y = (boardPosition + 15 - (boardPosition % 15)) / 15;
            return new Vector2(x, y);
        }

        /// <summary>
        /// Convert XY window-relative coordinates to XY board-relative coordinates
        /// </summary>
        /// <param name="vector">XY window-relative coordinates</param>
        /// <returns>XY board-relative coordinates</returns>
        public static Vector2 DivideVector(Vector2 vector)
        {
            return new Vector2((int)(vector.X / 50) + 1, (int)((vector.Y - 30) / 50) + 1);
        }
    }
    #endregion
}
