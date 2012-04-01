using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if ANDROID
using Android.App;
#endif

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RenderTarget2DSample
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		const int PlayrBaseSpeed =  500;
		
		
		SpriteFont 				font;
		/// <summary>
		/// The GraphicsDeviceManager is what creates and automagically manages the game's GraphicsDevice.
		/// </summary>
		GraphicsDeviceManager 	graphics;

		/// <summary>
		/// We use SpriteBatch to draw all of our 2D graphics.
		/// </summary>
		SpriteBatch 			spriteBatch;

		/// <summary>
		/// This is the rendertarget we'll be drawing to.
		/// </summary>
		RenderTarget2D 			renderTarget;
		
		FPSCounterComponent 	fps;
		
		public int 				collideCount;
		
		Texture2D 				animatedTilesheet;
		/// <summary>
		/// The players current speed.
		/// </summary>
		Vector2 				speed;// = new Vector2(PLAYASPEED,0);
		/// <summary>
		/// The direction the player is traveling
		/// </summary>
		Vector2 				direction;// = new Vector2(0,0);
		/// <summary>
		/// The change vector is the displacement since the last frame.
		/// </summary>
		Vector2 				changeVector;
		/// <summary>
		/// The kbstate is used to hold the current keyboard related states each loop
		/// </summary>
		KeyboardState 			kbstate;
		/// <summary>
		/// The bkgnd is used to update and draw the scrolling background.
		/// </summary>
		Background 				bkgnd;
		/// <summary>
		/// The level object contains a arraylist of a map's object and its size
		/// </summary>
		Map 					level;
		/// <summary>
		/// The player class is responcable for containing the current state of the player.
		/// </summary>
		Player					player;
		/// <summary>
		/// The tile sheet.
		/// </summary>
        Texture2D 				TileSheet;
		/// <summary>
		/// The glasses class UI is used to indicate which pair of glasses is being drawn.
		/// </summary>
		public GlassesUI 				glasses;
		
		bool 					shiftDown;

		/// <summary>
		/// The constructor for our Game1 class.
		/// </summary>
        public Game1 ()  
		{
			// Create the GraphicsDeviceManager for our game.
			graphics = new GraphicsDeviceManager (this);
			
#if ANDROID || IOS
            graphics.IsFullScreen = true;
#else
			graphics.PreferredBackBufferWidth = 800;
			graphics.PreferredBackBufferHeight = 600;
			graphics.IsFullScreen = true;
#endif

			// Set the root directory of the game's ContentManager to the "Content" folder.
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			bkgnd 		= new Background(this);
			speed 		= new Vector2(PlayrBaseSpeed,0);
			direction 	= new Vector2(0,0);	
			
			base.Initialize ();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{           
			
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch 	= new SpriteBatch (GraphicsDevice);

			// Create a rendertarget that matches the back buffer's dimensions, does not generate mipmaps automatically
			// (the Reach profile requires power of 2 sizing in order to do that), uses an RGBA color format, and
			// has no depth buffer or stencil buffer.
			
			renderTarget 	= new RenderTarget2D (GraphicsDevice, GraphicsDevice.PresentationParameters.BackBufferWidth,GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None);
			
            TileSheet 			= Content.Load<Texture2D>("tilesheet.png");
			font 				= Content.Load<SpriteFont>("spriteFont1");
			animatedTilesheet 	= Content.Load<Texture2D>("AnimationTiles.png");
			
			fps 				= new FPSCounterComponent(this,spriteBatch,font);
			level 				= MapLoader.ReadFile("./Content/map.txt", TileSheet,animatedTilesheet, this);
			glasses 			= new GlassesUI(this, spriteBatch);
			player				= new Player(this);
			Vector2 playerPos 	= new Vector2(
				(this.Window.ClientBounds.Width / 2 ) - 16,
				(this.Window.ClientBounds.Height / 2) - 32
				);
			
			player.Initalise(this,animatedTilesheet,spriteBatch,playerPos);
			bkgnd.Initialise(Content.Load<Texture2D>("Background2"),800);			
			
			Components.Add(fps);
			


		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent ()
		{
			// While not strictly necessary, you should always dispose of assets you create
			// (excluding those you load the the ContentManager) when those assets implement
			// IDisposable. RenderTarget2D is one such asset type, so we dispose of it properly.
			if (renderTarget != null) {
				// We put this in a try-catch block. The reason is that if for some odd reason this failed
				// (e.g. we were using threading and nulled out renderTarget on some other thread),
				// then none of the rest of the UnloadContent method would run. Here it doesn't make a
				// difference, but it's good practice nonethless.
				try {
					renderTarget.Dispose ();
					renderTarget = null;
				} catch {
				}
			}
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// Allows the game to exit. If this is a Windows version, I also like to check for an Esc key press. I put
			// it within an #if WINDOWS .. #endif block since that way it won't run on other platforms.
			collideCount = 0;
			bool characterJumped = false;
			
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
				|| Keyboard.GetState ().IsKeyDown (Keys.Escape)) 
			{
				this.Exit ();
			}

			// Grab current keyboar state on loop
			kbstate = Keyboard.GetState();
			
			// To prevent the character moving on for ever
			direction.X = 0;
			direction.Y = 0;
			if (kbstate.IsKeyDown(Keys.Left)) {
				direction.X = 1;
			}
			if (kbstate.IsKeyDown(Keys.Right)) {
				
				direction.X = -1;	
			}
       		
			if(kbstate.IsKeyDown(Keys.D1)){
                glasses.SelectGlasses = 0;
            }else if(kbstate.IsKeyDown(Keys.D2)){
                glasses.SelectGlasses = 1;
            }else if(kbstate.IsKeyDown(Keys.D3)){
                glasses.SelectGlasses = 2;
            }else if(kbstate.IsKeyDown(Keys.D4)){
                glasses.SelectGlasses = 3;
            }			
			
			if (kbstate.IsKeyDown(Keys.LeftShift))
			{
				speed.X = PlayrBaseSpeed * 10;
			}
			if (kbstate.IsKeyUp(Keys.LeftShift)) {
				speed.X = PlayrBaseSpeed;
			}

			if (kbstate.IsKeyDown(Keys.Up))
			{
				characterJumped = true;
			}
			else
			{
				characterJumped = false;	
			}
			
			
			if (kbstate.IsKeyDown(Keys.O))
			{
			
				Console.Clear();
			}
			


            //changeVector = direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
			
			
/*			
			if ( (player.MapLocation.X < (-(level.Width) + (Window.ClientBounds.Width /2)) ) || (player.MapLocation.X  > ( Window.ClientBounds.Width/2) ) ) {

				changeVector = -(direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
			}
			else {

				changeVector = direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
			}
*/
			
			int calculation1 = (int)level.Position.X - (Window.ClientBounds.Width / 2);
            int calculation2 = (int)player.MapLocation.X - (Window.ClientBounds.Width / 2);
         
            if ((direction.X == 1) && (calculation1 >= 0))//(changeVector.X >= 0) &&
            {
                changeVector.X = calculation1 * -1;// 0;//(level.Position.X-(Window.ClientBounds.Width / 2))

            }
            else if ((calculation2 <= -(level.Width)) && (direction.X == -1)) 
            {
                changeVector.X = (level.Width + calculation2) * -1;// calculation;// *-1;// 0;//

            }
            else
            {
                changeVector = direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
			
			

			
			level.Update(gameTime,changeVector);
			
			if ((this.collideCount) > 0) {
				changeVector.X = 0;Console.WriteLine ("BANG ON!");
			}
			
			bkgnd.Update(gameTime,changeVector);			
			glasses.UpdateMouse(); 
			player.Update(gameTime,changeVector,characterJumped);

			base.Update (gameTime);
			
			changeVector.X = 0; changeVector.Y = 0;
			
			if (player.MapLocation.Y > Window.ClientBounds.Height) {
				this.Exit();
				Console.WriteLine("You fell off the world!");	
			}
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			
            graphics.GraphicsDevice.Clear(Color.Black);


			spriteBatch.Begin();
				bkgnd.Draw(gameTime,spriteBatch);
				level.Draw(gameTime);
				glasses.drawGlasses(gameTime);
				player.Draw(gameTime,spriteBatch);
				base.Draw (gameTime);
			spriteBatch.End();
			
		}
		
		public static void GrabScreenshot(RenderTarget2D rendertarget)
        {
			Color[] data = new Color[(rendertarget.Width * rendertarget.Height) * 3];
            //OpenTK.Graphics.ES11.GL.ReadPixels(0, 0, rendertarget.Width, rendertarget.Height, OpenTK.Graphics.ES11.All.Rgb, OpenTK.Graphics.ES11.All.UnsignedByte, ref data);            
			rendertarget.GetData<Color>(data);
        }
		
        public SpriteBatch GetSpriteBatch()
        {
            return spriteBatch;
        }

        public Game GetGame()
        {
            return this;
        }
		
		public Player GetPlayer()
		{
			return player;	
		}
	}
}
