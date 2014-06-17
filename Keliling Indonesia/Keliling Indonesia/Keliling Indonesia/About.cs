using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Keliling_Indonesia
{
    class About : Screen
    {
        Texture2D puzzleFont;

        Texture2D aboutFont;

        Texture2D backButton;
        Texture2D aboutBackground;
        Texture2D grayBackground;

        SpriteFont aboutUs;

        MouseState nowMouseState;
        MouseState oldMouseState;

        Vector2 backButtonPosition;

        public About(ContentManager content, EventHandler screenEvent):base(screenEvent)
        {
            puzzleFont = content.Load<Texture2D>(@"picture/tulisan puzzle");
            aboutFont = content.Load<Texture2D>(@"picture/tentang");
            aboutBackground = content.Load<Texture2D>(@"picture/pantai kuta background");
            grayBackground = content.Load<Texture2D>(@"picture/gray");
            aboutUs = content.Load<SpriteFont>(@"about");
            backButton = content.Load<Texture2D>(@"picture/button kembali");
            backButtonPosition = new Vector2(625, 415);
        }

        public override void Update(GameTime time)
        {
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
            batch.Draw(aboutBackground, Vector2.Zero, Color.White);
            batch.Draw(grayBackground, Vector2.Zero, Color.White*0.8f);
            batch.Draw(puzzleFont, new Rectangle(30, 30, 150, 50), Color.White);
            batch.Draw(aboutFont, new Vector2(240, 20), Color.White);
            batch.DrawString(aboutUs, "Game Puzzle ini dibuat oleh :\n\n1) Novandi Banitama\n2) Satria\n3) Ryan\n4) Nyoman Bagus\n\nSebagai Final Project Pemrograman .NET \ntahun ajaran 2013/2014\n\nSELAMAT BERMAIN!!!", new Vector2(250,150), Color.Black);
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
                screenEvent.Invoke(this, new EventArgs());
            }            
        }
    }
}
