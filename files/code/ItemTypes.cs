using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bomberman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Media;
using System.Security.Principal;

namespace Bomberman
{
    #region All Hidden Items
    /// <summary>
    /// 
    /// </summary>
    public class Treasure : Item
    {
        public Treasure() : base(5) { }

        public override void Collected()
        {
            base.Collected();
            Score.Add((new Random().Next(16, 126)) * 10);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ExitPortal : Item
    {
        public ExitPortal() : base(6) { }

        public override void Collected()
        {
            base.Collected();
            LevelGeneration.levelText = $"CURRENT STAGE: {++LevelGeneration.level}";
            Game.Restart(newLevel: true);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Wheelchair : Item
    {
        public Wheelchair() : base(7) { }

        public override void Collected()
        {
            base.Collected();
            Game.slownessTimer = 360;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Trap : Item
    {
        public Trap() : base(8) { }

        public virtual void CheckForPlayerCollision(ref GameObject gameObject)
        {
            Vector2 gameObjectCoordinates = VectorMath.DivideVector(new Vector2(gameObject.position.X + 25, gameObject.position.Y + 25));

            for (int index = 0; index < 165; index++)
            {
                if ((Game.boardLayout[index] == 8) && (index == VectorMath.CalculateBoardRelativePosition(gameObjectCoordinates)))
                {
                    base.Collected();
                    gameObject.Kill();
                }
            }
        }
    }
    #endregion
}
