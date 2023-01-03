using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    #region Eric Movement
    internal class EricMovement
    {
        public static int directionChangeCountdown;

        public static void ChangeTexture(Texture2D newTexture)
        {
            directionChangeCountdown = 5;
            Game.eric.texture = newTexture;
        }

        public static void TextureUpdates()
        {
            if (directionChangeCountdown > 0)
                directionChangeCountdown--;

            if (directionChangeCountdown <= 0)
                Game.eric.texture = Game.ericTexture;
        }
    }
    #endregion
}
