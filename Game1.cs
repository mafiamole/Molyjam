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
		public GlassesUI 		glasses;
		
		public Controls				controls;
		
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
			graphics.IsFullScreen = false;
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
						this.controls = new Controls(this,glasses);

			
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
			

			
			controls.Update(gameTime,level,player);
			
			level.Update(gameTime,controls.changeVector);
			
			bkgnd.Update(gameTime,controls.changeVector);
			glasses.UpdateMouse(); 
			player.Update(gameTime,controls.changeVector,characterJumped);


			base.Update (gameTime);
			
			controls.ClearChangeVector();
			
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
