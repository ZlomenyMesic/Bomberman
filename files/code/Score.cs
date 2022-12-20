using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bomberman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.Xml;

namespace Bomberman
{
    #region Score
    static class Score
    {
        public static int score = 0;
        public static string scoreboard = "SCORE: 0";

        /// <summary>
        /// Add score and update the scoreboard
        /// </summary>
        /// <param name="amount">The score to add</param>
        public static void Add(int amount)
        {
            score += amount;
            if (score < 0)
                score = 0;
            scoreboard = $"SCORE: {score}";
        }

        /// <summary>
        /// Set the score to a specified number and update the scoreboard
        /// </summary>
        /// <param name="amount">The score to set</param>
        public static void Set(int amount)
        {
            score = amount;
            scoreboard = $"SCORE: {score}";
        }

        /// <summary>
        /// Calculate the position of the scoreboard based on the score amount.
        /// Needed to keep the scoreboard centered
        /// </summary>
        /// <returns>XY window-relative coordinates</returns>
        public static Vector2 CalculateScoreBoardPosition()
        {
            // Get the length of the score

            int scoreLength = score.ToString().Length;

            // Calculate the position

            return new Vector2(335 - (scoreLength * 5), 5);
        }
    }
    #endregion
}
