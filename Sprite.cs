using System;
using System.Collections;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RenderTarget2DSample
{
	public class Sprite : DrawableGameComponent
    {
        protected Vector2 			Position;
        protected MapLoader.TileType 	Tile;
        protected SpriteBatch 		spriteBatch;
        protected bool 				isDynamic; 
        protected Texture2D		 	TileSheet;
		protected bool 				isSpriteVisible = true;
		protected bool				imageFlippedHorizontally;
		protected bool				imageFlippedVertically;
	
        public Sprite(Game game, Texture2D tileSheet, SpriteBatch Batch, Vector2 pos, MapLoader.TileType t, bool dynamicObject = false) :
		base(game)
        {
            Position = pos;
            Tile = t;
            spriteBatch = Batch;
            TileSheet = tileSheet;
			
        }
		
		public Vector2 mapLocation {
		
			get {
				return Position;
			}
			
		}
	
		public bool flippedHorizonally
		{
			get
				{
					return imageFlippedHorizontally;
				}
			set {
				imageFlippedHorizontally = value;
			}
		}
	
		public bool flippedVertically
		{
			get { return imageFlippedVertically; }
			set { imageFlippedVertically = value; }
		}
	
		public bool isVisible
        {
            get
            {
                return isSpriteVisible;
            }
            set
            {
                isSpriteVisible = value;
            }
        }

        public void Update(GameTime gameTime,Vector2 changeVector
			)
        {

            if (Tile == MapLoader.TileType.Glasses) changeVector.X = 0;


			Position += changeVector;


            switch (Tile)
            {
                case MapLoader.TileType.Goo:
                case MapLoader.TileType.Spikes:
                    if (((Game1)base.Game).glasses.SelectGlasses == 3)
                    {
                        isVisible = true;
                    }
                    else
                    {
                        isVisible = false;
                    }
                    break;
                case MapLoader.TileType.Ground:
                case MapLoader.TileType.Rocks:
                    Vector2 playerVect = ((Game1)this.Game).GetPlayer().Position;
/*
                    Rectangle tmp1 = new Rectangle((int)Position.X , (int)Position.Y, 32,32);
                    Rectangle tmp2 = new Rectangle((int)playerVect.X,(int)playerVect.Y ,32,64);
*/
                    if (/*tmp1.Intersects(tmp2)*/ 
						Collision.RectDetection((int)Position.X,(int)Position.Y,32,32, (int)playerVect.X,(int)playerVect.Y,32,64)
						)
                    {
                        Console.WriteLine("nick");
						((Game1)Game).collideCount += 1;
                    }
                    else
                    {
                        //   Console.WriteLine("none");
					

                    }

                break;

                case MapLoader.TileType.Life:
                case MapLoader.TileType.Key:
                if (((Game1)base.Game).glasses.SelectGlasses == 0)
                {
                    isVisible = true;
                }
                else
                {
                    isVisible = false;
                }
                break;
                
            default:
                break;


            }




        }


        public bool CheckCol(int X1, int Y1, int width1, int height1, int X2, int Y2, int width2, int height2)
        {            
            //colliding
            if (X1 <= (X2 + width2) & Y1 <= (Y2 + height2) & X2 <= (X1 + width1) & Y2 <= (Y1 + height1))
            {
                return true;
                
            }//not colliding
            else
            {
                return false;
            }
        }



        public void Draw(GameTime gameTime)
        {
			if (isVisible) {


                SpriteEffects eff;

                Vector2 origin = new Vector2(16, 16);

                if (this.flippedHorizonally)
                {
                    eff = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    eff = SpriteEffects.None;
                }

                if(isDynamic) { // Is player or enemy


                }else{ // Is normal tile

                    switch (Tile)
                    {
                        case MapLoader.TileType.Glasses:
                            spriteBatch.Draw(TileSheet, new Rectangle((int)Position.X, (int)Position.Y-16, 32, 64),
                                      new Rectangle(((Game1)base.Game).glasses.SelectGlasses * 32, 64 , 32, 64), Color.White, 0.0f, origin, eff, 0.0f);
                            break;
                        default:
                            spriteBatch.Draw(TileSheet, new Rectangle((int)Position.X, (int)Position.Y, 32, 32),
                                      new Rectangle(32 * (int)Tile, 0, 32, 32), Color.White, 0.0f, origin, eff, 0.0f);

                            break;

                    }


                }
			}
         
            
        }
	}
}
