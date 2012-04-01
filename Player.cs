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
			
			ArrayList subList;
			
			
			if (jump)
			{
			changeVector.Y -= (float)5;	
				System.Console.WriteLine(changeVector.Y);
			}
			else {
			
				if (mapPosition.Y  < 0) {
					changeVector.Y += gameTime.ElapsedGameTime.Milliseconds * 0.3f;

				}
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
			if (direction)
			{
/*
			spriteBatch.Draw(
					blarg,
					screenPosition,
					null,
					Color.Beige,
					0.0f,
					origin,
					0.0f,
					SpriteEffects.FlipHorizontally,
					0.0f);
					*/
			}
			else 
			{
			//spriteBatch.Draw(blarg,screenPosition,Color.Beige);	
			}
			
			sprite.Draw(gameTime);
			overlaySprite.Draw(gameTime);
		}
		
	}
}
