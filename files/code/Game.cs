using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Linq;
using System.Net.Mime;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Bomberman
{
    #region Main Game
    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        /*
         * 0 = air
         * 1 = wall
         * 2 = weak wall
         * 3 = placed bomb
         * 4 = smoke
         * 5 = loaded treasure
         * 6 = loaded exit portal
         * 7 = loaded wheelchair
         */

        public static int[] boardLayout = new int[165];
        public static Block[] gameBoard = new Block[165];

        public static Texture2D textureWall;
        public static Texture2D textureWeakWall;
        public static Texture2D bombTexture;
        public static Texture2D smokeTexture;
        public static Texture2D textureTreasure;
        public static Texture2D textureExitPortal;
        public static Texture2D textureWheelchair;

        public static Texture2D floaterTexture;
        public static Texture2D ericTexture;
        public static Texture2D ericLeftTexture;
        public static Texture2D ericRightTexture;

        public SpriteFont mainFont;

        private KeyboardState keyboardState;

        public static GameObject eric;
        public static GameObject floater1;
        public static GameObject floater2;

        public static Item treasure;
        public static Item exitPortal;
        public static Item wheelchair;

        public static SoundEffect bombExplosion;
        public static SoundEffect ericWalking;
        public static SoundEffect upgradeSound;

        public const int framesPerSecond = 120;

        public const int additionalFloaterSpeed = 2;
        public static int floaterSpeedClock = 0;

        public static int slownessTimer = -1;


        public Game()
        {
            // Game window settings

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.IsBorderless = true;

            _graphics.PreferredBackBufferWidth = 750;
            _graphics.PreferredBackBufferHeight = 580;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // Load the window

            base.Initialize();

            BlockUtilities.LoadBoardLayout();

            Start();
        }

        protected override void LoadContent()
        {
            // Load the textures

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            textureWall = Content.Load<Texture2D>("Wall");
            textureWeakWall = Content.Load<Texture2D>("WeakWall");
            textureTreasure = Content.Load<Texture2D>("Treasure");
            textureExitPortal = Content.Load<Texture2D>("Exit");
            textureWheelchair = Content.Load<Texture2D>("Wheelchair");

            floaterTexture = Content.Load<Texture2D>("Floater");
            ericTexture = Content.Load<Texture2D>("Eric");
            ericLeftTexture = Content.Load<Texture2D>("EricLeft");
            ericRightTexture = Content.Load<Texture2D>("EricRight");

            bombTexture = Content.Load<Texture2D>("Bomb");
            smokeTexture = Content.Load<Texture2D>("Explosion");

            bombExplosion = Content.Load<SoundEffect>("bombSound");
            ericWalking = Content.Load<SoundEffect>("walking");
            upgradeSound = Content.Load<SoundEffect>("treasureSound");

            mainFont = Content.Load<SpriteFont>("MainFont");
        }

        /// <summary>
        /// Update the game at 120 FPS
        /// </summary>
        /// <param name="gameTime">Time since the last update</param>
        protected override void Update(GameTime gameTime)
        {
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1d / framesPerSecond);

            // Key binds

            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            KeyBinds.KeyboardPlaceBomb(keyboardState);

            if (slownessTimer <= 0)
                KeyBinds.KeyboardMovePlayer(keyboardState);
            else if ((slownessTimer > 0) && (slownessTimer % 2) == 0)
                KeyBinds.KeyboardMovePlayer(keyboardState);
            if (slownessTimer > 0) slownessTimer--;

            // Eric position updates

            EricMovement.TextureUpdates();

            // Floater updates

            FloaterMovement.Updates(ref floater1);
            FloaterMovement.Updates(ref floater2);

            // Bomb updates

            Bomb.Updates();

            // Item updates

            StructureUpdates.CheckForCollision();
            StructureUpdates.UpdateTextures();

            // SFX updates

            WalkingSFX.SoundUpdates();

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw all blocks and game objects on the game window
        /// </summary>
        /// <param name="gameTime">Time since the last draw</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            // Go through every block and draw it

            foreach (Block block in gameBoard)
                if (BlockUtilities.GetBlockTypeTexture(block.blockType) != null)
                    _spriteBatch.Draw(BlockUtilities.GetBlockTypeTexture(block.blockType), new Rectangle(new Point((int)block.vector.X, (int)block.vector.Y), new Point(50, 50)), Color.White);

            // Draw Eric and the floaters

            _spriteBatch.Draw(eric.texture, eric.rectangle, Color.White);
            _spriteBatch.Draw(floater1.texture, floater1.rectangle, Color.White);
            _spriteBatch.Draw(floater2.texture, floater2.rectangle, Color.White);

            // Draw the scoreboard

            _spriteBatch.DrawString(mainFont, Score.scoreboard, Score.CalculateScoreBoardPosition(), Color.White);
            _spriteBatch.DrawString(mainFont, LevelManager.levelText, new Vector2(5, 5), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Actions after loading the game window
        /// </summary>
        public static void Start()
        {
            // Load some important stuff

            LevelManager.LoadNewStartPositions();

            treasure = new(5);
            exitPortal = new(6);
            wheelchair = new(7);

            treasure.GenerateItem();
            exitPortal.GenerateItem();

            BlockUtilities.UpdateAllTextures();
        }

        /// <summary>
        /// Reset all variables and timers, then either go to level 0 or load a new level
        /// </summary>
        /// <param name="newLevel">true if a new level should be loaded, false to go back to level 0</param>
        public static void Restart(bool newLevel)
        {
            LevelManager.level = newLevel ? LevelManager.level : 1;
            LevelManager.levelText = $"CURRENT STAGE: {LevelManager.level}";

            slownessTimer = -1;

            Bomb.ResetCountdowns();

            // Prevent loading the treasure, exit portal and wheelchair

            treasure.itemFound = true;
            exitPortal.itemFound = true;
            wheelchair.itemFound = true;

            treasure.itemGenerated = false;
            exitPortal.itemGenerated = false;
            wheelchair.itemGenerated = false;

            // Update the board and the textures

            BlockUtilities.LoadBoardLayout();
            BlockUtilities.UpdateAllTextures();

            // Load the new starting positions for the player and the floaters

            LevelManager.LoadNewStartPositions();

            // Generate a new treasure, exit portal and wheelchair

            treasure.GenerateItem();
            exitPortal.GenerateItem();
            if ((new Random().Next(0, 3) == 1) && (LevelManager.level > 2))
                wheelchair.GenerateItem();
        }
    }
    #endregion
}