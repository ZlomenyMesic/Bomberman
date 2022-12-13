using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    #region Walking Sound Effects
    internal class WalkingSFX
    {
        public static int isPlaying = 0;

        /// <summary>
        /// Play the walking sound effect
        /// </summary>
        public static void PlaySound()
        {
            if (isPlaying <= 0)
            {
                isPlaying = 26;
                Game.ericWalking.Play();
            }
        }

        /// <summary>
        /// Wait 260 ms to play another sound effect
        /// </summary>
        public static void SoundUpdates() => isPlaying--;
    }
    #endregion
}
