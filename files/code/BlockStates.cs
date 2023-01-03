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
        public static bool IsFree(Vector2 vector, int[] board)
        {
            return board[VectorMath.CalculateBoardRelativePosition(vector)] == 0 ;
        }

        public static bool IsOutOfRange(Vector2 vector)
        {
            return (vector.X > 15) || (vector.X < 1) || (vector.Y > 11) || (vector.Y < 1);
        }

        public static bool IsBomb(Vector2 vector)
        {
            return Game.boardLayout[VectorMath.CalculateBoardRelativePosition(vector)] == 3;
        }

        public static bool IsDestructable(Vector2 vector)
        {
            return Game.boardLayout[VectorMath.CalculateBoardRelativePosition(vector)] == 2;
        }
        
        public static bool CanExplode(Vector2 vector)
        {
            return !IsOutOfRange(vector) && (IsFree(vector, Game.boardLayout) || IsDestructable(vector));
        }

        public static bool CanBeWalkedThrough(Vector2 vector)
        {
            return new int[]{ 0, 3, 4, 5, 6, 7, 8}.Contains(Game.boardLayout[VectorMath.CalculateBoardRelativePosition(vector)]);
        }

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
