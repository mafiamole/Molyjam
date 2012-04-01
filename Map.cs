using System;
using System.Collections;
using Microsoft.Xna.Framework;

namespace RenderTarget2DSample
{
	public class Map
	{
		private ArrayList mapObjDa;
        private ArrayList dynObjDa;
		private int mapWidth;
		private Vector2 position;
		protected Game1 game;
		
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
		
		public Map (ArrayList objects, ArrayList dynamicObjects,int width,Game1 game)
		{
			mapObjDa = objects;
            dynObjDa = dynamicObjects;
			mapWidth = width;
			this.game = game;
		}
		
		public void Update(GameTime gameTime,Vector2 changeVector)
		{
			// Perform collision detection
			foreach (Sprite obj in mapObjDa)
			{
				obj.Collision(gameTime,changeVector);                
			}
            foreach (AnimatedSprite obj in dynObjDa)
            {
                obj.Collision(gameTime,changeVector);  
			}
			
			if (this.game.collideCount > 0)
				changeVector.X = 0;
			// cue other update actions
			position += changeVector;
			foreach (Sprite obj in mapObjDa)
			{
				obj.Update(gameTime,changeVector);                
			}
            foreach (AnimatedSprite obj in dynObjDa)
            {
                obj.Update(gameTime, changeVector);
            }
		}
		
		public void Draw(GameTime gameTime)
		{
			foreach (Sprite obj in mapObjDa)
			{
				obj.Draw(gameTime);
			}
            foreach (AnimatedSprite obj in dynObjDa)
            {
                obj.Draw(gameTime);
            }
		}

    
		
	}
}

