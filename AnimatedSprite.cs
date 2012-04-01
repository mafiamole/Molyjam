using System;
using System.Collections.Generic;
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
    class AnimatedSprite : RenderTarget2DSample.Sprite
    {
        private int currFrame = 0;
        private int animationSpeed = 100; // Milliseconds before next frame is shown
        int lastFrameTime;
        int yOffset = 0;
        private bool stopAnimation;
		/// <summary>
		/// Initializes a new instance of the <see cref="RenderTarget2DSample.AnimatedSprite"/> class.
		/// </summary>
		/// <param name='game'>
		/// Game.
		/// </param>
		/// <param name='tileSheet'>
		/// Tile sheet.
		/// </param>
		/// <param name='Batch'>
		/// Batch.
		/// </param>
		/// <param name='pos'>
		/// Position on screen
		/// </param>
		/// <param name='t'>
		/// T.
		/// </param>
        public AnimatedSprite(Game game, Texture2D tileSheet, SpriteBatch Batch, Vector2 pos, MapLoader.TileType t)
            : base(game, tileSheet, Batch, pos, t, false)
            {
                // As diff animated tiles have diff heights, get the offset from the top of the animated Tileset file 
                if (t == MapLoader.TileType.NPC_ground) yOffset = 128;
                if (t == MapLoader.TileType.NPC_Flyer) yOffset = 160;

                lastFrameTime = System.Environment.TickCount;
				stopAnimation = false;
            }



        public virtual void Update(GameTime gameTime,Vector2 changeVector)
        {
	        if (System.Environment.TickCount >= (lastFrameTime + animationSpeed)) {
	            nextFrame();                  
	        }
            if (Tile == MapLoader.TileType.Player)
			{
			changeVector.X = 0;	
			}
				
			base.Update(gameTime,changeVector);
        }




        public virtual void Draw(GameTime gameTime)
        {
            if (isVisible)
            {                
                
                //Rectangle rect = new Rectangle((int)Position.X - 20, (int)Position.Y - 20, TileSheet.Width + 40, TileSheet.Height + 40);
                //spriteBatch.Draw(TileSheet, new Rectangle((int)Position.X, (int)Position.Y, TileSheet.Width, TileSheet.Height), Color.White);
				
                if (Tile == MapLoader.TileType.Player)
                {
					SpriteEffects eff;
					
					Vector2 origin = new Vector2(16,32);
					
					if (this.flippedHorizonally)
					{
						eff = SpriteEffects.FlipHorizontally;
					}
					else
					{
 						eff = SpriteEffects.None;
					}
					
                   	spriteBatch.Draw(TileSheet, new Rectangle((int)Position.X, (int)Position.Y, 32, 64),
                                         new Rectangle(32 * currFrame, yOffset, 32, 64), Color.White,0.0f,origin,eff,0.0f);

                }
                else
                {

                    spriteBatch.Draw(TileSheet, new Rectangle((int)Position.X, (int)Position.Y, 32, 32),
                                         new Rectangle(32 * currFrame, yOffset, 32, 32), Color.White);
                }




            }
       }



        public void nextFrame()
        {
            currFrame += 1;
            if (currFrame == 4) currFrame = 0;
            lastFrameTime = System.Environment.TickCount;
        }


    }
}
