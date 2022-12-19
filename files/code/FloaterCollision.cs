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
    #region Floater Collisions
    internal static class FloaterCollision
    {
        /// <summary>
        /// Check for a collision between the player and the floater
        /// </summary>
        /// <param name="floater">Game object reference</param>
        public static void CheckForCollision(GameObject floater) 
        {
            // Calculate the distance between the player and the floater

            int distance = (int)Vector2.Distance(Game.eric.position, floater.position);

            if (distance < 30)
                Game.eric.Kill();
        }
    }
    #endregion
}
