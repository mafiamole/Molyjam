using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RenderTarget2DSample
{
	public class Player
	{
		
		private GlassesUI.Glasses 	currentPair; // grow some.
		private Vector2 			screenPosition, mapPosition, origin;
		AnimatedSprite				sprite;
		Sprite						overlaySprite;
		bool						direction;
		bool						jumping;
		int							jumpStamp;
		const int					jumpTime = 35;
		
		public Player ()
		{
			
			screenPosition.X = 0; screenPosition.Y = 0;
			mapPosition.X = 0; mapPosition.Y = 0;
			
		}
		
		public Vector2 Position {
			get {
				return screenPosition;	
			}
		}
		
		public Vector2 MapLocation {
		
			get {
			
				return mapPosition;
				
			}
		}
		
		public void Initalise(Game1 game,Texture2D playerTexture,SpriteBatch spriteBatch,Vector2 screenPosition)
		{
			this.origin = new Vector2(playerTexture.Width /2,playerTexture.Height / 2);
			this.screenPosition =screenPosition;
			this.sprite = new AnimatedSprite(game,playerTexture,spriteBatch,screenPosition,MapLoader.TileType.Player);
			this.overlaySprite = new Sprite(game,playerTexture,spriteBatch,screenPosition,MapLoader.TileType.Glasses);
		}
		
		public void Update(GameTime gameTime,Vector2 changeVector,bool jump,ArrayList objects)
		{
			if (objects.Count > 0)
			{
			Console.WriteLine("We have impact!");
				Console.WriteLine(objects.Count);
				foreach (Sprite tile in objects)
				{
					//tile.mapLocation.X;
					//tile.mapLocation.Y;
					if (tile.mapLocation.X > this.mapPosition.X || (tile.mapLocation.X + 32) < this.mapPosition.X)
					{
						Console.WriteLine("It is in the x directions!");	
					}
					if (tile.mapLocation.Y > this.mapPosition.Y || (tile.mapLocation.Y + 32) < this.mapPosition.Y)
					{
						Console.WriteLine("it is in the y directions");
					}
				}
					
			}
			if (jumping)
			{
				changeVector.Y -= (float)5;
				Console.WriteLine("Jumpin");
				if (gameTime.ElapsedGameTime.Milliseconds > (-jumpStamp + jumpTime )) {
				jumping = false;
				Console.WriteLine("END OF JUMP");
				}
			}			
			
			else {
			
				//if (mapPosition.Y  < 0) {
					if (jump)
					{
						if (!jumping)
						{
						jumpStamp = gameTime.ElapsedGameTime.Milliseconds;
						jumping = true;
						}
					}					
					changeVector.Y += gameTime.ElapsedGameTime.Milliseconds * 0.03f; //Gravity

				//}
			}

			mapPosition += changeVector;

			if ( changeVector.X > 0)
			{
				sprite.flippedHorizonally = true;
				overlaySprite.flippedHorizonally = true;
				direction = true;
			}
			else
			{
				sprite.flippedHorizonally = false;
				overlaySprite.flippedHorizonally = false;
				direction = false;	
			}
			
			sprite.Update(gameTime,changeVector);
			overlaySprite.Update(gameTime,changeVector);
		}
		
		public void Draw(GameTime gameTime,SpriteBatch spriteBatch)	
		{
			sprite.Draw(gameTime);
			overlaySprite.Draw(gameTime);
		}
		
	}
}
