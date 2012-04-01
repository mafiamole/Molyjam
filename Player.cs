using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RenderTarget2DSample
{
	public class Player
	{
		
		private GlassesUI.Glasses 	currentPair; // grow some.
		private Vector2 			screenPosition, mapPosition, origin;
		Texture2D 					blarg;
		bool						direction;
		
		public Player ()
		{
		}
		
		public Vector2 Position {
			get {
				return screenPosition;	
			}
		}
		
		public void Initalise(Texture2D playerTexture,Vector2 screenPosition)
		{
			this.blarg = playerTexture;
			this.origin = new Vector2(playerTexture.Width /2,playerTexture.Height / 2);
			this.screenPosition =screenPosition;
		}
		
		public void Update(GameTime gameTime,Vector2 changeVector)
		{
			mapPosition += changeVector;
			if ( changeVector.X > 0)
			{
				direction = true;
			}
			else
			{
				direction = false;	
			}
		}
		
		public void Draw(GameTime gameTime,SpriteBatch spriteBatch)	
		{
			if (direction)
			{

			spriteBatch.Draw(
					blarg,
					screenPosition,
					null,
					Color.Beige,
					0.0f,
					origin,
					0.0f,
					SpriteEffects.FlipHorizontally,
					0f);
					
			}
			else 
			{
			spriteBatch.Draw(blarg,screenPosition,Color.Beige);	
			}
		}
		
	}
}
