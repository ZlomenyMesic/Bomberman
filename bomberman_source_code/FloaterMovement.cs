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
    #region Floater Movement
    internal class FloaterMovement
    {
        private static int preventMultipleTurns = 0;

        /// <summary>
        /// Choose a new direction of the floater.
        /// Used when a floater hits a wall
        /// </summary>
        /// <param name="floater">Game object reference</param>
        /// <param name="currentDirection">The current direction of the floater</param>
        public static void ChangeDirection(ref GameObject floater, Direction currentDirection)
        {
            // List all directions, then remove the current one from the list, then choose a random new one

            Direction[] directions = { Direction.Up, Direction.Down, Direction.Left, Direction.Right};
            directions = directions.Where(direction => direction != currentDirection).ToArray();
            Direction newDirection = directions[new Random().Next(0, 3)];

            floater.direction = newDirection;
        }

        /// <summary>
        /// Choose a new direction for the floater.
        /// Only turn sideways (no turning in the opposite direction)
        /// </summary>
        /// <param name="floater">Game object reference</param>
        /// <param name="currentDirection">The current direction of the floater</param>
        public static void TurnSideways(ref GameObject floater, Direction currentDirection)
        {
            Direction[] directions = { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
            directions = directions.Where(direction => (direction != currentDirection) && (direction != MoveGameObject.oppositeDirection[direction])).ToArray();
            Direction newDirection = directions[new Random().Next(0, 2)];

            floater.direction = newDirection;
        }

        /// <summary>
        /// 0.1% chance that the floater will change it's direction
        /// </summary>
        /// <param name="floater">Game object reference</param>
        public static void RandomDirectionChange(ref GameObject floater)
        {
            if (new Random().Next(0, 500) == 1)
                ChangeDirection(ref floater, floater.direction);
        }

        /// <summary>
        /// Chance that the floater will turn sideways at an intersection
        /// </summary>
        /// <param name="floater">Game object reference</param>
        public static void RandomTurnAtIntersection(ref GameObject floater)
        {
            if (BlockStates.IsIntersection(VectorMath.DivideVector(new Vector2(floater.position.X, floater.position.Y + 1)), Game.boardLayout) && (preventMultipleTurns < 0))
            {
                TurnSideways(ref floater, floater.direction);
                preventMultipleTurns = 200;
            }

            preventMultipleTurns--;
        }
    }
    #endregion
}
