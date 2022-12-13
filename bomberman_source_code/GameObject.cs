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
    #region Game Objects
    public class GameObject
    {
        public Vector2 position;
        public Rectangle rectangle;
        public Texture2D texture;
        public Direction direction = Direction.Down;
        public bool isPlayer;

        /// <summary>
        /// Create a new game object (player or floater)
        /// </summary>
        /// <param name="startPos">XY window-relative coordinates of the game object, top left corner</param>
        /// <param name="player">true if the game object is a player, otherwise false</param>
        public GameObject(Vector2 startPos, bool player)
        {
            position = startPos;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 50, 50);
            texture = player ? Game.ericTexture : Game.floaterTexture;
            isPlayer = player;
        }

        /// <summary>
        /// Move the game object to a different location
        /// </summary>
        /// <param name="newPos">XY coordinates of the new location, top left corner of the game object/param>
        public void MoveTo(Vector2 newPos)
        {
            position = newPos;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 50, 50);
        }

        /// <summary>
        /// Kill the game object
        /// </summary>
        public void Kill()
        {
            LevelManager.preventMultipleRestarts = true;
            if (!isPlayer) 
                this.MoveTo(new Vector2(9999999, 9999999));
            LevelManager.Death(isPlayer);
        }
    }
    #endregion
}
