using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bomberman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Text;

namespace Bomberman
{
    #region Game Object Movement
    internal static class MoveGameObject
    {
        public static Dictionary<Direction, Direction> oppositeDirection = new Dictionary<Direction, Direction> 
        {
            { Direction.Up, Direction.Down },
            { Direction.Down, Direction.Up },
            { Direction.Left, Direction.Right },
            { Direction.Right, Direction.Left }
        };

        /// <summary>
        /// Move a game object (player or floater) 1 pixel in a specified direction
        /// </summary>
        /// <param name="gameObject">The object to move (player or floater)</param>
        /// <param name="direction">The direction in which the object will move</param>
        public static void Move(ref GameObject gameObject, Direction direction)
        {
            GetDirectionCoords(direction, out int move_X, out int move_Y, out int add_X1, out int addY1, out int addX2, out int addY2);

            // Get the game object coordinates

            Vector2 topLeft = VectorMath.DivideVector(new Vector2(gameObject.position.X + add_X1, gameObject.position.Y + addY1));
            Vector2 bottomRight = VectorMath.DivideVector(new Vector2(gameObject.position.X + addX2, gameObject.position.Y + addY2));

            // Get the coordinates of the blocks the object wants to move to

            Vector2 newCoordsTop = new Vector2(topLeft.X + move_X, topLeft.Y + move_Y);
            Vector2 newCoordsBottom = new Vector2(bottomRight.X + move_X, bottomRight.Y + move_Y);

            // Check if it is possible to move to those blocks

            if (!BlockStates.IsOutOfRange(newCoordsTop) && !BlockStates.IsOutOfRange(newCoordsBottom)
                && BlockStates.CanBeWalkedThrough(newCoordsTop) && BlockStates.CanBeWalkedThrough(newCoordsBottom))
            {
                // Move the game object 1 pixel in the given direction

                gameObject.MoveTo(new Vector2(gameObject.position.X + move_X, gameObject.position.Y + move_Y));
            }
            else if (!gameObject.isPlayer)
            {
                // If a floater couldn't move in that direction, change it's direction
                // Needed to change the floater's direction when it hits a wall

                FloaterMovement.ChangeDirection(ref gameObject, direction);
            }

            // If the floater hits a bomb, change it's direction to the opposite

            if (!BlockStates.IsOutOfRange(newCoordsTop) && BlockStates.IsBomb(newCoordsTop)
                || !BlockStates.IsOutOfRange(newCoordsBottom) && BlockStates.IsBomb(newCoordsBottom) && !gameObject.isPlayer)
                gameObject.direction = oppositeDirection[gameObject.direction];
        }

        /// <summary>
        /// Express a direction with coordinates
        /// Calculate additional pixels to center a game object
        /// </summary>
        /// <param name="direction">The direction in which the game object is moving</param>
        /// <param name="move_X">The direction expressed in X coordinates</param>
        /// <param name="move_Y">The direction expressed in Y coordinates</param>
        /// <param name="addX1">Additional X pixels for the top left corner</param>
        /// <param name="addY1">Additional Y pixels for the top left corner</param>
        /// <param name="addX2">Additional X pixels for the bottom right corner</param>
        /// <param name="addY2">Additional pixels for the bottom right corner</param>
        private static void GetDirectionCoords(Direction direction, out int move_X, out int move_Y, out int addX1, out int addY1, out int addX2, out int addY2)
        {
            move_X = 0; move_Y = 0; addX1 = 0; addY1 = 0; addX2 = 0; addY2 = 0;

            // Return the amounts based on the given direction

            switch (direction)
            {
                case Direction.Up: move_X = 0; move_Y = -1; addX1 = 2; addY1 = 50; addX2 = 48; addY2 = 50; break;
                case Direction.Down: move_X = 0; move_Y = 1; addX1 = 2; addY1 = 0; addX2 = 48; addY2 = 0; break;
                case Direction.Left: move_X = -1; move_Y = 0; addX1 = 50; addY1 = 2; addX2 = 50; addY2 = 48; break;
                case Direction.Right: move_X = 1; move_Y = 0; addX1 = 0; addY1 = 2; addX2 = 0; addY2 = 48; break;
            }
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left, 
        Right
    }
    #endregion
}
