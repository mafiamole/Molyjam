using System;
using System.Collections;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RenderTarget2DSample
{
	public class Sprite
    {
            private Vector2 			Position;
            private MapLoader.TileType 	Tile;
            private SpriteBatch 		spriteBatch;
            private bool 				isDynamic; 
            private Texture2D		 	TileSheet;
			private bool 				hide;
		
            public Sprite(Game game, Texture2D tileSheet, SpriteBatch Batch, Vector2 pos, MapLoader.TileType t, bool dynamicObject = false)
            {
                Position = pos;
                Tile = t;
                spriteBatch = Batch;
                TileSheet = tileSheet;

            }

            public void Update(GameTime gameTime,Vector2 changeVector)
            {
				Position += changeVector;
            }

            public void Draw(GameTime gameTime)
            {
                if(isDynamic) { // Is player or enemy


                }else{ // Is normal tile
                    //spriteBatch.Draw(TileSheet, new Rectangle(0, 0, TileSheet.Width, TileSheet.Height), Color.White);
                   spriteBatch.Draw(TileSheet, new Rectangle((int)Position.X, (int)Position.Y, 32, 32),
                                      new Rectangle(32*(int)Tile, 0, 32, 32), Color.White);

                }
                
             
                
            }
	}
}
