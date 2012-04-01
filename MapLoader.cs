using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace RenderTarget2DSample
{
	public static class MapLoader
	{
		
		public enum TileType
			{
			 Ground,  //#
			 Water,   //$
			 Goo,     //&
			 Rocks,   //%
			 Spikes,  //^
			 Key,     //1
			 Life,    //2
			 NPC_ground, //3
			 NPC_Flyer, //4
			 Player,   // No Symbol
			 Glasses // No Symbol
			}
        public static Map ReadFile(string filename, Texture2D TileSheet,Texture2D AnimatedTileSheet, Game1 game)
        {
            Int32 x = 0;
            Int32 y = 0;
			int largestWidth = 0;
            ArrayList tmp = new ArrayList();
            ArrayList tmp2 = new ArrayList();
			
            using (StreamReader sReader = new StreamReader(filename))
            {
                while (sReader.Peek() >= 0)
                {
                    x = 0;
                    foreach (char c in sReader.ReadLine())
                    {
switch (c)
                        {
                            case '#':
                                tmp2.Add(new Sprite(game,TileSheet,game.GetSpriteBatch(),new Vector2(x*32, y*32), TileType.Ground));
                                break;
                            case '$':
                                tmp2.Add(new Sprite(game,TileSheet, game.GetSpriteBatch(), new Vector2(x*32, y*32), TileType.Water));
                                break;
                            case '&':
                                tmp2.Add(new Sprite(game, TileSheet, game.GetSpriteBatch(), new Vector2(x * 32, y * 32), TileType.Goo));
                                break;
                            case '%':
                                tmp2.Add(new Sprite(game, TileSheet, game.GetSpriteBatch(), new Vector2(x * 32, y * 32), TileType.Rocks));
                                break;
                            case '^':
                                tmp2.Add(new Sprite(game, TileSheet, game.GetSpriteBatch(), new Vector2(x * 32, y * 32), TileType.Spikes));
                                break;
                            case '2':
                                tmp2.Add(new Sprite(game, TileSheet, game.GetSpriteBatch(), new Vector2(x * 32, y * 32), TileType.Life));
                                break;
                            case '1':
                                tmp2.Add(new Sprite(game, TileSheet, game.GetSpriteBatch(), new Vector2(x * 32, y * 32), TileType.Key));
                                break;
                            case '3':
                                tmp.Add(new AnimatedSprite(game, AnimatedTileSheet, game.GetSpriteBatch(), new Vector2(x * 32, y * 32), TileType.NPC_ground));
                                break;
                            case '4':
                                tmp.Add(new AnimatedSprite(game, AnimatedTileSheet, game.GetSpriteBatch(), new Vector2(x * 32, y * 32), TileType.NPC_Flyer));
                                break;
                            case '\t':
                                x += 7;
                                break;
                            default:
                                // Char is whitespace or non recognised tile, ignore.
                                break;
                        }

                        x += 1;
                    }

                    if (x > largestWidth) {
						largestWidth = x;
					}

                    y += 1;
                }
            }
			Map tempMap = new Map(tmp2,tmp,largestWidth * 32);
            return tempMap;
        }
	



	}
}

