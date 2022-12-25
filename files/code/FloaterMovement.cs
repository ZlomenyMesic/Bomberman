using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bomberman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.MediaFoundation;

namespace Bomberman
{
    #region Floater Movement
    internal class FloaterMovement
    {
        private static int preventMultipleTurns = 0;
        public static int preventTurningAway = 0;

        /// <summary>
        /// Choose a new direction of the floater.
        /// Used when a floater hits a wall
        /// </summary>
        /// <param name="floater">Game object reference</param>
        /// <param name="currentDirection">The current direction of the floater</param>
        public static void ChangeDirection(ref GameObject floater, Direction currentDirection)
        {
            // List all directions, then remove the current one from the list, then choose a random new one

            Direction[] directions = { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
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
            if ((preventTurningAway < 0) && (new Random().Next(0, 500) == 1))
                ChangeDirection(ref floater, floater.direction);
        }

        /// <summary>
        /// Chance that the floater will turn sideways at an intersection
        /// </summary>
        /// <param name="floater">Game object reference</param>
        public static void RandomTurnAtIntersection(ref GameObject floater)
        {
            if (BlockStates.IsIntersection(VectorMath.DivideVector(new Vector2(floater.position.X, floater.position.Y + 1)), Game.boardLayout) 
                && (preventMultipleTurns < 0) && (preventTurningAway < 0))
            {
                TurnSideways(ref floater, floater.direction);
                preventMultipleTurns = 200;
            }

            if (preventMultipleTurns > -1)
                preventTurningAway--;
            if (preventTurningAway > -1)
                preventTurningAway--;

        }

        /// <summary>
        /// When a floater is at an intersection, check if the player is in any of the 4 directions, if yes, turn the floater
        /// </summary>
        /// <param name="floater">Game object reference</param>
        public static void ChasePlayer(ref GameObject floater)
        {
            // Vertical checking

            if (BlockStates.IsIntersection(VectorMath.DivideVector(new Vector2(floater.position.X + 1, floater.position.Y + 1)), Game.boardLayout) 
                && ((int)(floater.position.Y / 50) == (int)((Game.eric.position.Y - 2) / 50)) && (preventTurningAway < 0))
            {
                if (floater.position.X > Game.eric.position.X)
                    floater.direction = Direction.Left;
                else floater.direction = Direction.Right;

                preventTurningAway = 100;
            }

            // Horizontal checking

            if (BlockStates.IsIntersection(VectorMath.DivideVector(new Vector2(floater.position.X + 1, floater.position.Y + 1)), Game.boardLayout)
                && ((int)(floater.position.X / 50) == (int)((Game.eric.position.X + 2) / 50)) && (preventTurningAway < 0))
            {
                if (floater.position.Y > Game.eric.position.Y)
                    floater.direction = Direction.Up;
                else floater.direction = Direction.Down;

                preventTurningAway = 100;
            }
        }

        /// <summary>
        /// All floater updates: moving, changing directions, checking for collision
        /// </summary>
        public static void Updates(ref GameObject floater)
        {
            // Move the floaters 33% faster than the player

            if (Game.floaterSpeedClock == Game.additionalFloaterSpeed)
            {
                MoveGameObject.Move(ref floater, floater.direction);
                Game.floaterSpeedClock = 0;
            }
            else Game.floaterSpeedClock++;

            FloaterMovement.RandomDirectionChange(ref floater);
            FloaterMovement.ChasePlayer(ref floater);
            FloaterMovement.RandomTurnAtIntersection(ref floater);
            FloaterCollision.CheckForCollision(floater);
            MoveGameObject.Move(ref floater, floater.direction);
        }
    }
    #endregion
}
