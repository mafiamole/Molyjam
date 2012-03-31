using System;
using System.Collections;
using Microsoft.Xna.Framework;

namespace RenderTarget2DSample
{
	public class Map
	{
		private ArrayList mapObjDa;
		private int mapWidth;
		private Vector2 position;
		
		public int Width {
			get {
				return mapWidth;	
			}
		}
		
		public ArrayList objects {
		
			get {
				return mapObjDa;
			}
			
		}
		
		public Vector2 Position {
			get {
				
				return position;	
				
			}
		}
		
		public Map (ArrayList objects,int width)
		{
			mapObjDa = objects;
			mapWidth = width;
		}
		
		
		public void Update(GameTime gameTime,Vector2 changeVector)
		{
			position += changeVector;
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

