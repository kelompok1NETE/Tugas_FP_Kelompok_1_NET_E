using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Keliling_Indonesia
{
    class HighScoreScreen : Screen
    {
        Texture2D puzzleFont;

        Texture2D highScoreFont;

        Texture2D backButton;
        Texture2D highScoreBackground;
        Texture2D grayBackground;

        SpriteFont highScore;

        MouseState nowMouseState;
        MouseState oldMouseState;

        Vector2 backButtonPosition;
        
        highscore HighScore;
        List<dataScore> dataHighScore;

        bool init = false;

        public HighScoreScreen(ContentManager content, EventHandler screenEvent):base(screenEvent)
        {
            HighScore = new highscore();
            dataHighScore = HighScore.getHighScore();
            highScoreBackground = content.Load<Texture2D>(@"picture/taman safari indonesia background");
            grayBackground = content.Load<Texture2D>(@"picture/gray");
            highScoreFont = content.Load<Texture2D>(@"picture/nilai tertinggi");            
            puzzleFont = content.Load<Texture2D>(@"picture/tulisan puzzle");            
            highScore = content.Load<SpriteFont>(@"highScore");
            backButton = content.Load<Texture2D>(@"picture/button kembali");
            backButtonPosition = new Vector2(625, 415);
        }

        public override void Update(GameTime time)
        {
            if (!init)
            {

                init = true;
            }

            nowMouseState = Mouse.GetState();
            if (oldMouseState.LeftButton == ButtonState.Pressed && nowMouseState.LeftButton == ButtonState.Released)
            {
                mouseClicked(nowMouseState.X, nowMouseState.Y);
            }
            oldMouseState = nowMouseState;

            base.Update(time);
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(highScoreBackground, Vector2.Zero, Color.White);
            batch.Draw(grayBackground, Vector2.Zero, Color.White * 0.8f);
            batch.Draw(puzzleFont, new Rectangle(30, 30,150,50), Color.White);
            batch.Draw(highScoreFont, new Vector2(240, 20), Color.White);
            int i = 0, counter = 0;

            foreach(var temp in dataHighScore)
            {
                if(i<10)
                {
                    batch.DrawString(highScore, temp.getName(), new Vector2(280, 150+counter), Color.Purple);
                    batch.DrawString(highScore, temp.getScore().ToString(), new Vector2(470, 150+counter), Color.Purple);
                }
                counter += 20;
                i++;
            }

            batch.Draw(backButton, new Vector2(625, 415), Color.White);
            base.Draw(batch);
        }

        void mouseClicked(int x, int y)
        {
            Rectangle mouseClickedRect = new Rectangle(x, y, 10, 10);
            Rectangle backButtonRect = new Rectangle((int)backButtonPosition.X, (int)backButtonPosition.Y, 160, 40);

            if (mouseClickedRect.Intersects(backButtonRect))
            {
                Console.WriteLine("coba");
                init = false;
              //  dataHighScore = null;
                dataHighScore = HighScore.getHighScore();
                screenEvent.Invoke(this, new EventArgs());
            }
        }
    }
}
