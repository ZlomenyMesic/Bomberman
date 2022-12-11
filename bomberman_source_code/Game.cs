using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
         */

        public static int[] boardLayout = new int[165];
        public static Block[] gameBoard = new Block[165];

        public static Texture2D textureWall;
        public static Texture2D textureWeakWall;
        public static Texture2D textureTreasure;
        public static Texture2D textureExitPortal;
        public static Texture2D ericTexture;
        public static Texture2D floaterTexture;
        public static Texture2D bombTexture;
        public static Texture2D smokeTexture;

        public SpriteFont mainFont;

        private KeyboardState keyboardState;

        public static GameObject eric;
        public static GameObject floater1;
        public static GameObject floater2;

        public static int FramesPerSecond = 100;

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

            BlockUtilities.LoadBoardLayout(level: 0);

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

            ericTexture = Content.Load<Texture2D>("Eric");
            floaterTexture = Content.Load<Texture2D>("Floater");

            bombTexture = Content.Load<Texture2D>("Bomb");
            smokeTexture = Content.Load<Texture2D>("Explosion");

            mainFont = Content.Load<SpriteFont>("MainFont");
        }

        /// <summary>
        /// Update the game at 100 FPS
        /// </summary>
        /// <param name="gameTime">Time since the last update</param>
        protected override void Update(GameTime gameTime)
        {
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1d / FramesPerSecond);

            // Keyboard updates

            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            KeyBinds.KeyboardMovePlayer(keyboardState);
            KeyBinds.KeyboardPlaceBomb(keyboardState);

            // Floater updates

            MoveGameObject.Move(ref floater1, floater1.direction);
            MoveGameObject.Move(ref floater2, floater2.direction);

            FloaterMovement.RandomDirectionChange(ref floater1);
            FloaterMovement.RandomDirectionChange(ref floater2);

            FloaterCollision.CheckForCollision(floater1);
            FloaterCollision.CheckForCollision(floater2);

            FloaterMovement.RandomTurnAtIntersection(ref floater1);
            FloaterMovement.RandomTurnAtIntersection(ref floater2);

            // Bomb updates

            Bomb.BombCountdown();

            Bomb.CheckForDeath(ref eric);
            Bomb.CheckForDeath(ref floater1);
            Bomb.CheckForDeath(ref floater2);

            // Structure updates (tresure and exit portal)

            Treasure.CheckForPlayerCollision();
            ExitPortal.CheckForPlayerCollision();

            StructureUpdates.UpdateTextures();
            StructureUpdates.UpdateBoardLayout();

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

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Actions after loading the game window
        /// </summary>
        public static void Start()
        {
            // Load some important stuff

            LevelManager.LoadNewStartPositions(0);
            Treasure.GenerateTreasure();
            ExitPortal.GenerateExitPortal();
            BlockUtilities.UpdateAllTextures();
        }

        /// <summary>
        /// Reset all variables and timers, then either go to level 0 or load a new level
        /// </summary>
        /// <param name="newLevel">true if a new level should be loaded, false to go back to level 0</param>
        public static void Restart(bool newLevel)
        {
            if (LevelManager.preventMultipleRestarts)
            {
                LevelManager.preventMultipleRestarts = false;

                LevelManager.level = newLevel ? ++LevelManager.level : 0;

                Bomb.ResetCountdowns();

                // Prevent loading the treasure and the exit portal

                Treasure.treasureFound = true;
                ExitPortal.exitPortalFound = true;

                // Load the new starting positions for the player and the floaters

                LevelManager.LoadNewStartPositions(newLevel ? LevelManager.level : 0);

                // Update the board and the textures

                BlockUtilities.LoadBoardLayout(newLevel ? LevelManager.level : 0);
                BlockUtilities.UpdateAllTextures();

                // Generate a new treasure and a new exit portal

                Treasure.GenerateTreasure();
                ExitPortal.GenerateExitPortal();
            }
        }
    }
    #endregion
}