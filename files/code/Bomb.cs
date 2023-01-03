using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bomberman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using SharpDX.MediaFoundation;

namespace Bomberman
{
    #region Bomb
    static class Bomb
    {
        private static int bombCountdown = -1;
        private static bool preventBombPlacement = false;

        public static void Place()
        {
            Vector2 ericCoords = VectorMath.DivideVector(new Vector2(Game.eric.position.X + 25, Game.eric.position.Y + 25));

            if (!BlockStates.IsOutOfRange(ericCoords) && BlockStates.IsFree(ericCoords, Game.boardLayout) && !BlockStates.IsBomb(ericCoords) && (bombCountdown == -1) 
                && (!Game.boardLayout.Contains(3)) && (!Game.boardLayout.Contains(4)))
            {
                bombCountdown = Game.framesPerSecond * 2;
                Game.boardLayout[VectorMath.CalculateBoardRelativePosition(ericCoords)] = 3;
                Game.gameBoard[VectorMath.CalculateBoardRelativePosition(ericCoords)].ChangeType(BlockType.Bomb);
            }
        }

        private static List<int> Size(int pos, int radius)
        {
            List<int> destructableBlocks = new List<int>();
            Vector2 bombPos = VectorMath.CalculateBoardVector(pos);

            for (int index = (int)bombPos.X - 1; index > bombPos.X - radius - 1; index--)
            {
                if (BlockStates.CanExplode(new Vector2(index, bombPos.Y)))
                    destructableBlocks.Add(VectorMath.CalculateBoardRelativePosition(new Vector2(index, bombPos.Y)));
                else break;
            }

            for (int index = (int)bombPos.X + 1; index < bombPos.X + radius + 1; index++)
            {
                if (BlockStates.CanExplode(new Vector2(index, bombPos.Y)))
                    destructableBlocks.Add(VectorMath.CalculateBoardRelativePosition(new Vector2(index, bombPos.Y)));
                else break;
            }

            for (int index = (int)bombPos.Y - 1; index > bombPos.Y - radius - 1; index--)
            {
                if (BlockStates.CanExplode(new Vector2(bombPos.X, index)))
                    destructableBlocks.Add(VectorMath.CalculateBoardRelativePosition(new Vector2(bombPos.X, index)));
                else break;
            }

            for (int index = (int)bombPos.Y + 1; index < bombPos.Y + radius + 1; index++)
            {
                if (BlockStates.CanExplode(new Vector2(bombPos.X, index)))
                    destructableBlocks.Add(VectorMath.CalculateBoardRelativePosition(new Vector2(bombPos.X, index)));
                else break;
            }

            destructableBlocks.Add(VectorMath.CalculateBoardRelativePosition(bombPos));

            return destructableBlocks;
        }

        private static void Explosion()
        {
            bombCountdown = -1;
            for (int index = 0; index < 165; index++)
            {
                if (Game.boardLayout[index] == 3)
                {
                    foreach (int position in Size(index, 2))
                    {
                        Game.gameBoard[position].ChangeType(BlockType.Smoke);
                        Game.boardLayout[position] = 4;
                    }
                    break;
                }
            }
        }

        private static void ClearExplosion()
        {
            for (int index = 0; index < 165; index++)
            {
                if (Game.boardLayout[index] == 4)
                {
                    Game.gameBoard[index].ChangeType(BlockType.Air);
                    Game.boardLayout[index] = 0;
                }
            }
        }

        public static void BombCountdown()
        {
            if (Game.boardLayout.Contains(4))
            {
                if ((bombCountdown <= 0) && !preventBombPlacement)
                {
                    preventBombPlacement = true;
                    bombCountdown = (int)(Game.framesPerSecond / 1.67);
                }
                else bombCountdown--;

                if (bombCountdown == 0)
                {
                    preventBombPlacement = false;
                    ClearExplosion();
                }
            }
            else
            {
                if (bombCountdown != -1) bombCountdown--;

                if (bombCountdown == 0)
                {
                    Game.bombExplosion.Play();
                    Explosion();
                }
            }
        }

        public static void CheckForDeath(ref GameObject gameObject)
        {
            Vector2 gameObjectCoordinates = VectorMath.DivideVector(new Vector2(gameObject.position.X + 25, gameObject.position.Y + 25));
            
            for (int index = 0; index < 165; index++)
                if ((Game.boardLayout[index] == 4) && (index == VectorMath.CalculateBoardRelativePosition(gameObjectCoordinates)))
                    gameObject.Kill();
        }

        public static void ResetCountdowns()
        {
            bombCountdown = -1;
            preventBombPlacement = false;
        }

        public static void Updates()
        {
            Bomb.BombCountdown();

            Bomb.CheckForDeath(ref Game.eric);
            Bomb.CheckForDeath(ref Game.floater1);
            Bomb.CheckForDeath(ref Game.floater2);
        }
    }
    #endregion
}
