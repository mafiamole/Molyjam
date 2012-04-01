using System;
using System.Collections;
using System.Linq;
using System.Text;

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
    public class GlassesUI{
        ArrayList glassesObjects = new ArrayList();
        Texture2D glassesTexture;
        Texture2D star;

        GlassesSprite glasses1;
        GlassesSprite glasses2;
        GlassesSprite glasses3;
        GlassesSprite glasses4;

        int selectedGlasses = 0;

        public GlassesUI(Game game, SpriteBatch spriteBatch)
        {
            star = game.Content.Load<Texture2D>("star");
            glassesTexture = game.Content.Load<Texture2D>("glasses");
            glassesObjects.Add(glasses1 = new GlassesSprite(game, glassesTexture, star, spriteBatch, new Vector2(20, game.Window.ClientBounds.Height - 40 + 50), MapLoader.TileType.Glasses, GlassesUI.Glasses.Items));
            glassesObjects.Add(glasses2 = new GlassesSprite(game, glassesTexture, star, spriteBatch, new Vector2(glasses1.ObjectPosition.X + 70 + 60, game.Window.ClientBounds.Height - 40 + 50), MapLoader.TileType.Glasses, GlassesUI.Glasses.Enemies));
            glassesObjects.Add(glasses3 = new GlassesSprite(game, glassesTexture, star, spriteBatch, new Vector2(glasses2.ObjectPosition.X + 70 + 60, game.Window.ClientBounds.Height - 40 + 50), MapLoader.TileType.Glasses, GlassesUI.Glasses.Light));
            glassesObjects.Add(glasses4 = new GlassesSprite(game, glassesTexture, star, spriteBatch, new Vector2(glasses3.ObjectPosition.X + 70 + 60, game.Window.ClientBounds.Height - 40 + 50), MapLoader.TileType.Glasses, GlassesUI.Glasses.PhysObjects));
			
            ((GlassesSprite)(glassesObjects[0])).isSelected = true;


        }

        public int SelectGlasses { 
            get {
                return selectedGlasses;
            }
            set
            {
                ((GlassesSprite)(glassesObjects[selectedGlasses])).isSelected = false;
                selectedGlasses = value;
                ((GlassesSprite)(glassesObjects[selectedGlasses])).isSelected = true;
            }
        }

         public enum Glasses{
             Items,
             Enemies,
             Light,
             PhysObjects
        }
		 public void drawGlasses(GameTime gameTime)
		  {
		   foreach (GlassesSprite i in glassesObjects)
		            {
		                i.Draw(gameTime);
		            }
		        }
    }


    public class GlassesSprite : RenderTarget2DSample.Sprite
    {

        private GlassesUI.Glasses glasses;
        private bool thisSelected = false;
        private Texture2D background;
		
        public GlassesSprite(Game game, Texture2D tileSheet, Texture2D backgroundHighlight, SpriteBatch Batch, Vector2 pos, MapLoader.TileType t, GlassesUI.Glasses glassesType = GlassesUI.Glasses.Light)
            : base(game, tileSheet, Batch, pos, t, false)
            {    
                glasses = glassesType;
                background = backgroundHighlight;
            }

            public Vector2 ObjectPosition{
                get{
                   return Position;
                } 
                set{
                    Position = value;
                }
            }

            public bool isSelected
            {
                get
                {
                    return thisSelected;
                }
                set
                {
                    thisSelected = value;
                }
            }

            public virtual void Update(GameTime gameTime)
            {
			
            }

            public virtual void Draw(GameTime gameTime)
            {
			
				if (isVisible) {
	                if (Tile == MapLoader.TileType.Glasses)
	                    {
					
	                       // Texture2D dummyTexture = new Texture2D(new GraphicsDevice(), 1, 1);
	                       // dummyTexture.SetData(new Color[] { Color.White });
	
	                        Rectangle rect = new Rectangle((int)Position.X, (int)Position.Y - 20, 70, 40 + 10);
	
	                        if (thisSelected)
	                        {
	                            // spriteBatch.Draw(dummyTexture, rect, Color.Chartreuse);
                                spriteBatch.Draw(background, rect, Color.White);
	                        }
	                        
	                       spriteBatch.Draw(TileSheet, new Rectangle((int)Position.X, (int)Position.Y, 70, 40), 
                                            new Rectangle(70*(int)glasses,0,70,40),Color.White);
	
	
	                    }
				}
             
                
            }

    }


}
