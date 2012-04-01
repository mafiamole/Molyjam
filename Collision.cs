using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RenderTarget2DSample
{
	public class Collision
	{
		
		Rectangle box;
		
		protected ArrayList  collisions;
		
		public Collision (float x,float y,int w,int h)
		{
			box.X = (int)x;
			box.Y = (int)y;
			
			box.Width = w;
			box.Height = h;
			
		}
		
		public void Draw() {
			
		}
		
		public bool CornerCheck(float x,float y, int w, int h)
		{
			return (this.box.X <= (x + w ) & this.box.Y < (y + h) ) & ((x <= (this.box.X + this.box.Width)) & y <= (this.box.Y + this.box.Height) );
		}
		
		public bool RectCheck(float x,float y, int w, int h)
		{
			Rectangle box2 = new Rectangle((int)x,(int)y,w,h);
			return this.box.Intersects(box2);
		}
		
		public static bool RectDetection(int obj1x,int obj1y,int obj1w,int obj1h,int obj2x,int obj2y,int obj2w,int obj2h)
		{
            Rectangle tmp1 = new Rectangle(obj1x , obj1y, obj1w,obj1h);
            Rectangle tmp2 = new Rectangle(obj2x, obj2y,obj2w,obj2h);
			return tmp1.Intersects(tmp2);
		}
		
		public static bool CornerDetection(int X1, int Y1, int width1, int height1, int X2, int Y2, int width2, int height2)
		{
			return (X1 <= (X2 + width2) & Y1 <= (Y2 + height2) & X2 <= (X1 + width1) & Y2 <= (Y1 + height1));
		}
		
		
	}
}

