using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RenderTarget2DSample
{
	public class Player
	{
		
		private Vector2 			screenPosition, mapPosition, origin;
		AnimatedSprite				sprite;
		Sprite						overlaySprite;
		int							direction;
		bool						jumping;
		int							jumpStamp;
		const int					jumpTime = 35;
		Game1						game; // reference to the game.
		
		public Player (Game1 game)
		{
			this.game = game;
			screenPosition.X = 0;
			screenPosition.Y = 0;
			mapPosition.X = 0;
			mapPosition.Y = 0;
			direction = 0;
		}
		
		public Vector2 Position {
			get {
				return sprite.GetPosition; // SetPosition is wanged	
			}
		}
		
		public Vector2 MapLocation {
		
			get {
			
				return mapPosition;
				
			}
		}
		
		public Vector2 Offset {
		
			get {
				
				return screenPosition;
				
			}
			
		}
		
		public void Initalise (Game1 game, Texture2D playerTexture, SpriteBatch spriteBatch, Vector2 screenPosition)
		{
			this.origin = new Vector2 (playerTexture.Width / 2, playerTexture.Height / 2);
			this.screenPosition = screenPosition;
			this.sprite = new AnimatedSprite (game, playerTexture, spriteBatch, screenPosition, MapLoader.TileType.Player);
			this.overlaySprite = new Sprite (game, playerTexture, spriteBatch, screenPosition, MapLoader.TileType.Glasses);
		}
		
		public void Update (GameTime gameTime, Vector2 changeVector, bool jump)
		{
		

			if (!(this.game.collideCount > 0 & changeVector.Y < 0)) {
				if (jumping) {
					changeVector.Y -= (float)5;
					Console.WriteLine ("Jumpin");
					if (gameTime.ElapsedGameTime.Milliseconds > (-jumpStamp + jumpTime)) {
						jumping = false;
						Console.WriteLine ("END OF JUMP");
					}
				} else {
			
					//if (mapPosition.Y  < 0) {
					if (jump) {
						if (!jumping) {
							jumpStamp = gameTime.ElapsedGameTime.Milliseconds;
							jumping = true;
						}
					}					
					//changeVector.Y += gameTime.ElapsedGameTime.Milliseconds * 0.03f; //Gravity

					//}
				}
			}

			mapPosition += changeVector;

			if (changeVector.X > 0) {
					
				sprite.flippedHorizonally = true;
				overlaySprite.flippedHorizonally = true;
				direction = 1;
			} else {
				sprite.flippedHorizonally = false;
				overlaySprite.flippedHorizonally = false;
				direction = -1;
			}
			
			sprite.Update (gameTime, changeVector);
			overlaySprite.Update (gameTime, changeVector);
		}
		
		public void Draw (GameTime gameTime, SpriteBatch spriteBatch)
		{
			sprite.Draw (gameTime);
			overlaySprite.Draw (gameTime);
		}
		
	}
}
