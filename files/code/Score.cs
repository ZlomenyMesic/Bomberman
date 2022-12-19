﻿using Microsoft.Xna.Framework;
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

            switch (scoreLength)
            {
                case 1: return new Vector2(330, 5);
                case 2: return new Vector2(325, 5);
                case 3: return new Vector2(320, 5);
                case 4: return new Vector2(315, 5);
                case 5: return new Vector2(310, 5);
                case 6: return new Vector2(305, 5);
                default: return new Vector2(0, 0);
            }
        }
    }
    #endregion
}
