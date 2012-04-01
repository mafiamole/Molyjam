using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace RenderTarget2DSample
{

	public class Background
	{
		
		public Texture2D texture;
		public Vector2[] positions;
		public Rectangle position;
		private int winWidth;
		protected Game1 game;
		
		public static bool Between(double val,double min, double max)
		{
  			return ( (val > min) && (val < max) );
		}
		
		public Background (Game1 game)
		{
			this.game = game;
			
		}
		
		public void Initialise(Texture2D texture,int winWidth)
		{
			
			this.texture = texture;
			
			this.winWidth = winWidth;
			
			positions = new Vector2[3];
			
			Vector2 position1 = new Vector2(texture.Width,0);
			Vector2 position2 = new Vector2(0,0);
			Vector2 position3 = new Vector2(-texture.Width,0);
			
			positions[0] = position1;
			positions[1] = position2;
			positions[2] = position3;
			
			position.Width = texture.Width;
			position.Height = texture.Height;
			
			position.X = 0;
			position.Y = 0;
		}
		
		public void Update(GameTime gametime,Vector2 changeVector)
		{

			for (int itr = 0; itr < positions.Length; itr++)
			{
				positions[itr] += changeVector;
				
			}
			
			if ( !Background.Between(positions[1].X,-texture.Width,texture.Width))
			{
				
				positions[0].X = texture.Width;
				positions[1].X = 0;
				positions[2].X = -texture.Width;
			}
			
			
		}
			
		public void Draw(GameTime gameTime,SpriteBatch spriteBatch)
		{
			
			foreach(Vector2 position in positions)
			{

				spriteBatch.Draw(texture,position,Color.Beige);
				
			}
			
		}
		
		
	}
}

 