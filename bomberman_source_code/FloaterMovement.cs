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
        /// <summary>
        /// Choose a new direction of the floater
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
        /// 0.5% chance that the floater will change it's direction
        /// </summary>
        /// <param name="floater">Game object reference</param>
        public static void RandomDirectionChange(ref GameObject floater)
        {
            if (new Random().Next(0, 200) == 1)
                ChangeDirection(ref floater, floater.direction);
        }

        public static void RandomTurnAtIntersection(ref GameObject floater)
        {
            // TODO
        }
    }
    #endregion
}
