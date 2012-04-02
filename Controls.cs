using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RenderTarget2DSample
{
	public class Controls
	{
		protected const int 	PLAYERBASESPEED = 500;
		protected 				KeyboardState keyboardState;
		protected Vector2 		direction;
		protected Vector2		speed;
		protected Vector2		changeVect;		
		protected bool 			characterJumped;
		protected GlassesUI 	glasses;
		protected bool 			ShiftDown;

		Game1 					game;
		
		public Controls (Game1 game,GlassesUI glasses)
		{
			this.game 		= game;
			this.glasses 	= glasses;
			speed 			= new Vector2(PLAYERBASESPEED,0);
			direction 		= new Vector2(0,0);	
			
		}
		
		public Vector2 changeVector {
		
			get {
				return changeVect;
			}
			set {
				this.changeVect = value;
			}
		}
		
		// cos I am lazy. 
		public void SetChangeVectorX (int x)
		{
			this.changeVect.X = x;
		}
		public void SetChangeVectorY (int x)
		{
			this.changeVect.Y = x;
		}		

		public void keyboard ()
		{
            
			// Grab current keyboar state on loop
			this.keyboardState = Keyboard.GetState ();
			
			if (keyboardState.IsKeyDown (Keys.Left)) {
				direction.X = 1;
			}
			if (keyboardState.IsKeyDown (Keys.Right)) {

				direction.X = -1;
			}

			if (keyboardState.IsKeyDown (Keys.Up)) {
				direction.Y = -1;
			}
			if (keyboardState.IsKeyDown (Keys.Down) ){
				direction.Y = 1;
			}
			
			
			if (keyboardState.IsKeyDown (Keys.D0)) {
				Console.Clear ();
			}



			if (keyboardState.IsKeyDown (Keys.D1)) {
				glasses.SelectGlasses = 0;
			} else if (keyboardState.IsKeyDown (Keys.D2)) {
				glasses.SelectGlasses = 1;
			} else if (keyboardState.IsKeyDown (Keys.D3)) {
				glasses.SelectGlasses = 2;
			} else if (keyboardState.IsKeyDown (Keys.D4)) {
				glasses.SelectGlasses = 3;
			}

			if (keyboardState.IsKeyDown (Keys.LeftShift)) {
				speed.X = PLAYERBASESPEED * 10;
				speed.Y = PLAYERBASESPEED * 10;

				
			}
			if (keyboardState.IsKeyUp (Keys.LeftShift)) {
				speed.X = PLAYERBASESPEED;
				speed.Y = PLAYERBASESPEED;
			}

		}

		public void Update (GameTime gameTime, Map level,Player player)
		{
			

			// To prevent the character moving on for ever
			direction.X = 0;
			direction.Y = 0;
  
			this.keyboard ();
			
			int calc1 = (int)level.Position.X - (game.Window.ClientBounds.Width / 2);
            int calc2 = (int)player.MapLocation.X - (game.Window.ClientBounds.Width / 2);			
			
			if ((game.collideCount) > 0) {
				changeVect.X = 0;
				changeVect.Y = 0;
			}			
			
            if ((direction.X == 1) && (calc1 >= 0))//(changeVector.X >= 0) &&
            {
                changeVect.X = calc1 * -1;// 0;//(level.Position.X-(Window.ClientBounds.Width / 2))
				changeVect.Y = direction.Y * speed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

            }
            else if ((calc2 <= -(level.Width)) && (direction.X == -1)) 
            {
                changeVect.X = (level.Width + calc2) * -1;// calculation;// *-1;// 0;//
				changeVect.Y = direction.Y * speed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

            }
            else
            {
                changeVect = direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            }

		}
		
		
		public void ClearChangeVector()
		{
			this.changeVect = Vector2.Zero;	
		}

		
	}




}
