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
		
        public void Update(GameTime gameTime,Vector2 changeVector)
        {
			Position += changeVector;
        }

        public void Draw(GameTime gameTime)
        {
			if (isVisible) {
			
                if(isDynamic) { // Is player or enemy


                }else{ // Is normal tile
                    //spriteBatch.Draw(TileSheet, new Rectangle(0, 0, TileSheet.Width, TileSheet.Height), Color.White);
                   spriteBatch.Draw(TileSheet, new Rectangle((int)Position.X, (int)Position.Y, 32, 32),
                                      new Rectangle(32*(int)Tile, 0, 32, 32), Color.White);

                }
			}
         
            
        }
	}
}
