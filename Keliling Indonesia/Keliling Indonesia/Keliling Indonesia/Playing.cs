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
    class Playing : Screen
    {
        enum GameState
        {
            pre,
            middle,
            post
        }
        GameState state;

        int move;

        List<Texture2D> puzzlePieces;
        List<int> puzzlePiecesIndex;

        Texture2D grayBackground;
        Texture2D puzzleFont;
        Texture2D pauseFont;
        Texture2D moveFont;

        Texture2D playButton;
        Texture2D pauseButton;
        Texture2D backButton;
        Texture2D playingBackground;
        Texture2D resumeButton;
        Texture2D successFont;

        Texture2D tile;

        Texture2D pictureGame;

        Texture2D pictureFull;

        MouseState nowMouseState;
        MouseState oldMouseState;

        Vector2 backButtonPosition;
        Vector2 playButtonPosition;
        Vector2 pauseButtonPosition;
        Vector2 resumeButtonPosition;

        ContentManager ct;

        Random rnd;

        SpriteFont moveCounter;
        Picture picture;
        bool pause = false;
        bool saveData = false;
        GraphicsDevice graphic;
        public Playing(ContentManager content, EventHandler screenEvent, GraphicsDevice gd):base(screenEvent)
        {
            graphic = gd;
            playingBackground = content.Load<Texture2D>(@"picture/candi borobudur background");
            puzzleFont = content.Load<Texture2D>(@"picture/tulisan puzzle");
            pauseFont = content.Load<Texture2D>(@"picture/tulisan puzzle");
            moveFont = content.Load<Texture2D>(@"picture/counter pindah");
            moveCounter = content.Load<SpriteFont>(@"move");
            successFont = content.Load<Texture2D>(@"picture/berhasil");

            state = GameState.post;            
            picture = new Picture();
            pictureGame = content.Load<Texture2D>(@"picture/" + picture.getNamePicture() + "");

            move = 0;
                //pictureGame = content.Load<Texture2D>(@"picture/siap");
            ct = content;
            grayBackground = content.Load<Texture2D>(@"picture/gray");
            backButton = content.Load<Texture2D>(@"picture/button kembali");
            playButton = content.Load<Texture2D>(@"picture/button main");
            pauseButton = content.Load<Texture2D>(@"picture/button berhenti");
            resumeButton = content.Load<Texture2D>(@"picture/button lanjut");
            backButtonPosition = new Vector2(20, 420);
            playButtonPosition = new Vector2(40, 370);
            pauseButtonPosition = new Vector2(40, 370);
            resumeButtonPosition = new Vector2(40, 370);
        }

        private void splitAndShufflePicture()
        {
            puzzlePieces = new List<Texture2D>();
            puzzlePiecesIndex = new List<int>();
            int number = 0;
            rnd = new Random();
            List<int> listNumber = new List<int>();
            
            for (int i = 1; i <= 9; i++)
            {
                Console.WriteLine("coba " + i);
                bool thereIs = false;
                while(!thereIs)
                {                    
                    number = rnd.Next(0, 9);
                    if (listNumber.Count == 0)
                    {
                        listNumber.Add(number);
                        thereIs = true;
                        Console.WriteLine("     coba " + number);
                    }
                    else
                    {
                        if (!listNumber.Contains(number))
                        {
                            thereIs = true;
                            listNumber.Add(number);
                            Console.WriteLine("     asas " + number);
                        }
                    }
                }
                if (ct != null)
                {
                    Console.WriteLine(number);
                    if(number!=0)
                        puzzlePieces.Add(ct.Load<Texture2D>(@"puzzle/" + picture.getNamePicture() + "/level 1/" + number + ""));
                    else
                        puzzlePieces.Add(ct.Load<Texture2D>(@"picture/gray"));
                    puzzlePiecesIndex.Add(number); 
                }                                   
            }
        }

        public override void Update(GameTime time)
        {
            nowMouseState = Mouse.GetState();
            if (oldMouseState.LeftButton == ButtonState.Pressed && nowMouseState.LeftButton == ButtonState.Released)
            {
                mouseClicked(nowMouseState.X, nowMouseState.Y);
            }
            oldMouseState = nowMouseState;
            if (state == GameState.middle)
            {
                if (!pause)
                {
                    checkedStatusGame();
                }
            }
            else if (state == GameState.post)
            {
                if (!saveData)
                {
                    highscore hg = new highscore();
                    hg.saveData("kelompok .NET", move);
                    Console.WriteLine("berhasil disimpan");
                    saveData = true;
                }                
            }
            base.Update(time);
        }

        private void checkedStatusGame()
        {
            for (int i = 1; i <= puzzlePiecesIndex.Count; i++)
            {
                if (puzzlePiecesIndex[i - 1] != 0)
                {
                    if (puzzlePiecesIndex[i - 1] != i)
                    {
                        return;
                    }
                }
            }
            state = GameState.post;
        }

        public override void Draw(SpriteBatch batch)
        {            
            batch.Draw(playingBackground, Vector2.Zero, Color.White);
            batch.Draw(grayBackground, Vector2.Zero, Color.White * 0.8f);
            batch.Draw(puzzleFont, new Rectangle(30, 30, 150, 50), Color.White);
            batch.Draw(backButton, new Vector2(20, 420), Color.White);
            if (state == GameState.pre)
            {                
                batch.Draw(playButton, new Vector2(40, 370), Color.White);                
                batch.Draw(pictureGame, new Rectangle(300,30,426,426) , Color.White);
            }
            else if (state == GameState.middle)
            {
                if (pause == false)
                {
                    batch.Draw(moveFont, new Vector2(25, 200), Color.White);
                    if(move < 10)
                       batch.DrawString(moveCounter, move.ToString(), new Vector2(220, 197), Color.White);                    
                    else
                        batch.DrawString(moveCounter, move.ToString(), new Vector2(213, 197), Color.White);                    
                    batch.Draw(pauseButton, new Vector2(25, 370), Color.White);
                    int counter = 0;
                    int initX = 300;
                    int initY = 30;
                    for (int i = 0; i < 3; i++)
                    {
                        initX = 300;
                        for (int j = 0; j < 3; j++)
                        {
                            if (puzzlePiecesIndex[counter] != 0)
                                batch.Draw(puzzlePieces[counter], new Rectangle(initX, initY, 142, 142), Color.White);
                            else
                                batch.Draw(puzzlePieces[counter], new Rectangle(initX, initY, 142, 142), Color.White * 0);
                            initX += 142;
                            counter++;
                        }
                        initY += 142;
                    }
                }
                else
                {                    
                    batch.Draw(resumeButton, new Vector2(25, 370), Color.White);
                    batch.Draw(pictureGame, new Rectangle(300, 30, 426, 426), Color.White);
                }
            }
            else if (state == GameState.post)
            {
                batch.Draw(moveFont, new Vector2(25, 200), Color.White);
                if (move < 10)
                    batch.DrawString(moveCounter, move.ToString(), new Vector2(220, 197), Color.White);
                else
                    batch.DrawString(moveCounter, move.ToString(), new Vector2(213, 197), Color.White);                    
                batch.Draw(pictureGame, new Rectangle(300, 30, 426, 426), Color.White);
                batch.Draw(successFont, new Vector2(400, 150), Color.White);
            }
            base.Draw(batch);
        }

        private void swapTile(int a, int b)
        {
            Texture2D tempTex = puzzlePieces[a];
            int tempInt = puzzlePiecesIndex[a];

            puzzlePiecesIndex[a] = puzzlePiecesIndex[b];
            puzzlePieces[a] = puzzlePieces[b];

            puzzlePieces[b] = tempTex;
            puzzlePiecesIndex[b] = tempInt;
        }

        private void checkTile(int numberTile)
        {
            if (numberTile == 0)
            {
                if (puzzlePiecesIndex[1] == 0)
                {
                    swapTile(0, 1);
                }
                else if (puzzlePiecesIndex[3] == 0)
                {
                    swapTile(0, 3);
                }
            }
            else if (numberTile == 2)
            {
                if (puzzlePiecesIndex[1] == 0)
                {
                    swapTile(1, 2);
                }
                else if (puzzlePiecesIndex[5] == 0)
                {
                    swapTile(2, 5);
                }
            }
            else if(numberTile == 6)
            {
                if (puzzlePiecesIndex[3] == 0)
                {
                    swapTile(6, 3);
                }
                else if (puzzlePiecesIndex[7] == 0)
                {
                    swapTile(6, 7);
                }
            }
            else if(numberTile == 8)
            {
                if (puzzlePiecesIndex[5] == 0)
                {
                    swapTile(8, 5);
                }
                else if (puzzlePiecesIndex[7] == 0)
                {
                    swapTile(7, 8);
                }
            }
            else if (numberTile == 1)
            {
                int left = 0;
                int bottom = 4;
                int right = 2;                
                if (puzzlePiecesIndex[left] == 0)
                {
                    swapTile(numberTile, left);
                }
                else if (puzzlePiecesIndex[bottom] == 0)
                {
                    swapTile(numberTile, bottom);
                }
                else if (puzzlePiecesIndex[right] == 0)
                {
                    swapTile(numberTile, right);
                }
            }
            else if(numberTile == 3)
            {
                int top = 0;
                int bottom = 6;
                int right = 4;
                if (puzzlePiecesIndex[top] == 0)
                {
                    swapTile(numberTile, top);
                }
                else if (puzzlePiecesIndex[bottom] == 0)
                {
                    swapTile(numberTile, bottom);
                }
                else if (puzzlePiecesIndex[right] == 0)
                {
                    swapTile(numberTile, right);
                }
            }
            else if (numberTile == 5)
            {
                int top = 2;
                int bottom = 8;
                int left = 4;

                if (puzzlePiecesIndex[top] == 0)
                {
                    swapTile(numberTile, top);
                }
                else if (puzzlePiecesIndex[bottom] == 0)
                {
                    swapTile(numberTile, bottom);
                }
                else if (puzzlePiecesIndex[left] == 0)
                {
                    swapTile(numberTile, left);
                }
            }
            else if (numberTile == 7)
            {
                int top = 4;
                int right = 8;
                int left = 6;

                if (puzzlePiecesIndex[top] == 0)
                {
                    swapTile(numberTile, top);
                }
                else if (puzzlePiecesIndex[right] == 0)
                {
                    swapTile(numberTile, right);
                }
                else if (puzzlePiecesIndex[left] == 0)
                {
                    swapTile(numberTile, left);
                }
            }
            else
            {
                if (puzzlePiecesIndex[1] == 0)
                {
                    swapTile(numberTile, 1);
                }
                else if (puzzlePiecesIndex[7] == 0)
                {
                    swapTile(numberTile, 7);
                }
                else if (puzzlePiecesIndex[3] == 0)
                {
                    swapTile(numberTile, 3);
                }
                else if (puzzlePiecesIndex[5] == 0)
                {
                    swapTile(numberTile, 5);
                }
            }            
        }

        private void mouseClicked(int x, int y)
        {
            Rectangle mouseClickedRect = new Rectangle(x, y, 10, 10);
            Rectangle backButtonRect = new Rectangle((int)backButtonPosition.X, (int)backButtonPosition.Y, 160, 40);            
            if (mouseClickedRect.Intersects(backButtonRect))
                {                    
                    picture = new Picture();
                    pictureGame = ct.Load<Texture2D>(@"picture/" + picture.getNamePicture() + "");
                    splitAndShufflePicture();
                    screenEvent.Invoke(this, new EventArgs());
                    state = GameState.pre;
                    pause = false;
                }
            if (state == GameState.pre)
            {
                Rectangle playButtonRect = new Rectangle((int)playButtonPosition.X, (int)playButtonPosition.Y, 125, 40);                
                if (mouseClickedRect.Intersects(playButtonRect))
                {
                    splitAndShufflePicture();            
                    state = GameState.middle;
                }
            }
            else if (state == GameState.middle)
            {
                if (!pause)
                {                    
                    Rectangle pauseButtonRect = new Rectangle((int)pauseButtonPosition.X, (int)pauseButtonPosition.Y, 140, 40);
                    if (mouseClickedRect.Intersects(pauseButtonRect))
                    {
                        pause = true;
                    }
                    Rectangle tile1Rect = new Rectangle(300, 30, 142, 142);
                    Rectangle tile2Rect = new Rectangle(442, 30, 142, 142);
                    Rectangle tile3Rect = new Rectangle(584, 30, 142, 142);
                    Rectangle tile4Rect = new Rectangle(300, 172, 142, 142);
                    Rectangle tile5Rect = new Rectangle(442, 172, 142, 142);
                    Rectangle tile6Rect = new Rectangle(584, 172, 142, 142);
                    Rectangle tile7Rect = new Rectangle(300, 314, 142, 142);
                    Rectangle tile8Rect = new Rectangle(442, 314, 142, 142);
                    Rectangle tile9Rect = new Rectangle(584, 314, 142, 142);
                    
                    if (mouseClickedRect.Intersects(tile1Rect))
                    {
                        checkTile(0);
                        move++;
                    }
                    else if (mouseClickedRect.Intersects(tile2Rect))
                    {
                        checkTile(1);
                        move++;
                    }
                    else if (mouseClickedRect.Intersects(tile3Rect))
                    {
                        checkTile(2);
                        move++;
                    }
                    else if (mouseClickedRect.Intersects(tile4Rect))
                    {
                        checkTile(3);
                        move++;
                    }
                    else if (mouseClickedRect.Intersects(tile5Rect))
                    {
                        checkTile(4);
                        move++;
                    }
                    else if (mouseClickedRect.Intersects(tile6Rect))
                    {
                        checkTile(5);
                        move++;
                    }
                    else if (mouseClickedRect.Intersects(tile7Rect))
                    {
                        checkTile(6);
                        move++;
                    }
                    else if (mouseClickedRect.Intersects(tile8Rect))
                    {
                        checkTile(7);
                        move++;
                    }
                    else if (mouseClickedRect.Intersects(tile9Rect))
                    {
                        checkTile(8);
                        move++;
                    }

                }
                else
                {
                    Rectangle resumeButtonRect = new Rectangle((int)resumeButtonPosition.X, (int)resumeButtonPosition.Y, 140, 40);
                    if (mouseClickedRect.Intersects(resumeButtonRect))
                    {
                        pause = false; 
                    }
                }
            }
            else if (state == GameState.post)
            {

            }
        }
    }
}
