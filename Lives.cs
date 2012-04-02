using System;
using System.Collections;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RenderTarget2DSample
{
    class Lives
    {
        private int lifeCount = 2;
        private Vector2 respawnPosition = new Vector2(100,100); // Should set to map start point initially
        private ArrayList lifeSprites = new ArrayList();
        private GameOver gameOver;

        public Lives(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            gameOver = new GameOver(game,spriteBatch,spriteFont);
            game.Components.Add(gameOver);

        }

        public void Draw(GameTime gameTime)
        {
            // call life sprites.draw
            gameOver.Draw(gameTime);

        }




        //Life sprite class?


        public int NumberOfLives
        {
            get
            {
                return lifeCount;
            }
            set
            {
                lifeCount = value;
            }
        }



        private class GameOver  : DrawableGameComponent
        {
            SpriteBatch spriteBatch;
            SpriteFont spriteFont;
            bool isGameOver = true;
           
            public bool IsGameOver{ get{ return isGameOver; } set { isGameOver = value; } }

            public GameOver(Game game, SpriteBatch Batch, SpriteFont Font) : base(game){
                    spriteFont = Font;
			        spriteBatch = Batch;
            }


            public override void Draw(GameTime gameTime)
            {
                if (IsGameOver)
                {
                    string str = "GAME OVER";
                    spriteBatch.DrawString(spriteFont, str, new Vector2(100, 210), Color.Red, 0.0f, new Vector2(0, 0), 5.0f, new SpriteEffects(), 0.0f);
                    spriteBatch.DrawString(spriteFont, str, new Vector2(90,200),Color.Yellow,0.0f,new Vector2(0,0),5.0f,new SpriteEffects(),0.0f);
                }
            }

        }

    }
}
