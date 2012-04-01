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
        GlassesSprite glasses1;
        GlassesSprite glasses2;
        GlassesSprite glasses3;
        GlassesSprite glasses4;

        int selectedGlasses = 0;

        public GlassesUI(Game game, SpriteBatch spriteBatch)
        {

            glassesTexture = game.Content.Load<Texture2D>("glasses");
			
            Console.WriteLine(game.Window.ClientBounds.Width + "\t" + game.Window.ClientBounds.Height);
            glassesObjects.Add(glasses1 = new GlassesSprite(game, glassesTexture, spriteBatch, new Vector2(20, game.Window.ClientBounds.Height - glassesTexture.Height + 50), MapLoader.TileType.Glasses, GlassesUI.Glasses.Light));
            glassesObjects.Add(glasses2 = new GlassesSprite(game, glassesTexture, spriteBatch, new Vector2(glasses1.ObjectPosition.X + glassesTexture.Width + 60, game.Window.ClientBounds.Height - glassesTexture.Height + 50), MapLoader.TileType.Glasses, GlassesUI.Glasses.PhysObjects));
            glassesObjects.Add(glasses3 = new GlassesSprite(game, glassesTexture, spriteBatch, new Vector2(glasses2.ObjectPosition.X + glassesTexture.Width + 60, game.Window.ClientBounds.Height - glassesTexture.Height + 50), MapLoader.TileType.Glasses, GlassesUI.Glasses.Enemies));
            glassesObjects.Add(glasses4 = new GlassesSprite(game, glassesTexture, spriteBatch, new Vector2(glasses3.ObjectPosition.X + glassesTexture.Width + 60, game.Window.ClientBounds.Height - glassesTexture.Height + 50), MapLoader.TileType.Glasses, GlassesUI.Glasses.Items));
			
            ((GlassesSprite)(glassesObjects[0])).isSelected = true;


        }

        public void SelectGlasses(int indexToSetActive){
            ((GlassesSprite)(glassesObjects[selectedGlasses])).isSelected = false;
            selectedGlasses = indexToSetActive;
            ((GlassesSprite)(glassesObjects[selectedGlasses])).isSelected = true;
        }

         public enum Glasses{
			Items,
            Enemies,
			Light,
            PhysObjects,
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
		
        public GlassesSprite(Game game, Texture2D tileSheet, SpriteBatch Batch, Vector2 pos, MapLoader.TileType t, GlassesUI.Glasses glassesType = GlassesUI.Glasses.Light)
            : base(game, tileSheet, Batch, pos, t, false)
            {    
                glasses = glassesType;
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
					
	                        Texture2D dummyTexture = new Texture2D(new GraphicsDevice(), 1, 1);
	                        dummyTexture.SetData(new Color[] { Color.White });
	
	                        Rectangle rect = new Rectangle((int)Position.X -20, (int)Position.Y - 20, TileSheet.Width + 40, TileSheet.Height + 40);
	
	                        if (thisSelected)
	                        {
	                            spriteBatch.Draw(dummyTexture, rect, Color.Green);
	                        }
	                        
	                       spriteBatch.Draw(TileSheet, new Rectangle((int)Position.X, (int)Position.Y, TileSheet.Width, TileSheet.Height), Color.White);
	
	
	                    }
				}
             
                
            }

    }


}
