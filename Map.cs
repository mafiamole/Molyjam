using System;
using System.Collections;
using Microsoft.Xna.Framework;

namespace RenderTarget2DSample
{
	public class Map
	{
		private ArrayList mapObjDa;
		private int mapWidth;
		
		public ArrayList objects {
		
			get {
				return mapObjDa;
			}
			
		}

		
		public Map (ArrayList objects,int width)
		{
			mapObjDa = objects;
			mapWidth = width;
		}
		
		
		public void Update(GameTime gameTime,Vector2 changeVector)
		{
			foreach (Sprite obj in mapObjDa)
			{
				obj.Update(gameTime,changeVector);
			}
		}
		
		public void Draw(GameTime gameTime)
		{
			foreach (Sprite obj in mapObjDa)
			{
				obj.Draw(gameTime);
			}
		}
		
	}
}

