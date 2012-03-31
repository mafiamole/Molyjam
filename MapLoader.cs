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
            Ground,
            Water,
            Item,
            NPC
        }
		
        public static Map ReadFile(string filename, Texture2D TileSheet, Game1 game)
        {
            Int32 x = 0;
            Int32 y = 0;
			int largestWidth = 0;
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
                            case '1':
                                tmp2.Add(new Sprite(game, TileSheet, game.GetSpriteBatch(), new Vector2(x*32, y*32), TileType.Item));
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
			Map tempMap = new Map(tmp2,largestWidth * 32);
            return tempMap;
        }
	
	}
}

