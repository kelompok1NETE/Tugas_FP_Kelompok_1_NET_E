using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Keliling_Indonesia
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ////enum GameState
        ////{
        ////    startMenu,
        ////    loading,
        ////    playing,
        ////    paused,
        ////    highscore
        ////}
        ////GameState gameState;

        //button game

        HighScoreScreen highScoreScreen;
        About aboutScreen;
        Playing playScreen;
        Screen screen;


        private Texture2D startButton;
        private Texture2D highScoreButton;
        private Texture2D aboutButton;

        private Texture2D puzzleFont;

        private Texture2D loadingScreen;

        private Vector2 startButtonPosition;
        private Vector2 highScoreButtonPosition;
        private Vector2 aboutButtonPosition;

        MouseState nowMouseState;
        MouseState oldMouseState;

        //background game
        private Texture2D backgroundImage;        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            highScoreScreen = new HighScoreScreen(this.Content, new EventHandler(afterEvent));
            playScreen = new Playing(this.Content, new EventHandler(afterEvent), GraphicsDevice);
            aboutScreen = new About(this.Content, new EventHandler(afterEvent));

            screen = null;

            backgroundImage = Content.Load<Texture2D>(@"picture/keliling indonesia");
            startButton = Content.Load<Texture2D>(@"picture/button main");
            startButtonPosition = new Vector2(350, 260);

            highScoreButton = Content.Load<Texture2D>(@"picture/button nilai tertinggi");
            highScoreButtonPosition = new Vector2(305, 310);

            aboutButton = Content.Load<Texture2D>(@"picture/button tentang");
            aboutButtonPosition = new Vector2(335, 360);

            puzzleFont = Content.Load<Texture2D>(@"picture/tulisan puzzle");
            // TODO: use this.Content to load your game content here
        }

        public void afterEvent(object obj, EventArgs e)
        {
            Console.WriteLine("halo");
            screen = null;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (screen == null)
            {
                nowMouseState = Mouse.GetState();
                if (oldMouseState.LeftButton == ButtonState.Pressed && nowMouseState.LeftButton == ButtonState.Released)
                {
                    mouseClicked(nowMouseState.X, nowMouseState.Y);
                }
                oldMouseState = nowMouseState;
            }
            else
            {
                screen.Update(gameTime);
            }

            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (screen == null)
            {
                spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                spriteBatch.Draw(puzzleFont, new Vector2(120, 30), Color.White);
                spriteBatch.Draw(startButton, new Vector2(350, 260), Color.White);
                spriteBatch.Draw(highScoreButton, new Vector2(305, 310), Color.White);
                spriteBatch.Draw(aboutButton, new Vector2(335, 360), Color.White);
            }
            else
            {
                screen.Draw(spriteBatch);
            }

            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }

        void mouseClicked(int x, int y)
        {
            Rectangle mouseClickedRect = new Rectangle(x, y, 10, 10);
            Rectangle startButtonRect = new Rectangle((int)startButtonPosition.X, (int)startButtonPosition.Y,125, 40);
            Rectangle highScoreButtonRect = new Rectangle((int)highScoreButtonPosition.X, (int)highScoreButtonPosition.Y, 200, 40);
            Rectangle aboutButtonRect = new Rectangle((int)aboutButtonPosition.X, (int)aboutButtonPosition.Y, 135, 40);

            Console.WriteLine(GraphicsDevice.Viewport.Width + "  " +GraphicsDevice.Viewport.Height);
            if (mouseClickedRect.Intersects(startButtonRect))
            {                
                screen = playScreen;
            }
            else if (mouseClickedRect.Intersects(highScoreButtonRect))
            {
                screen = highScoreScreen;
            }
            else if (mouseClickedRect.Intersects(aboutButtonRect))
            {
                screen = aboutScreen;
            }
        }
    }
}
