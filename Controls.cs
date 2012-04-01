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
    class Controls
    {
        public Controls() { }

        public void Update(ref Vector2 direction,ref KeyboardState kbstate,ref GlassesUI glasses,ref Vector2 speed, int PlayrBaseSpeed,ref bool characterJumped)
        {
            // Grab current keyboar state on loop
            kbstate = Keyboard.GetState();

            // To prevent the character moving on for ever
            direction.X = 0;
            direction.Y = 0;
            if (kbstate.IsKeyDown(Keys.Left))
            {
                direction.X = 1;
            }
            if (kbstate.IsKeyDown(Keys.Right))
            {

                direction.X = -1;
            }

            if (kbstate.IsKeyDown(Keys.D0)) { Console.Clear(); }



            if (kbstate.IsKeyDown(Keys.D1))
            {
                glasses.SelectGlasses = 0;
            }
            else if (kbstate.IsKeyDown(Keys.D2))
            {
                glasses.SelectGlasses = 1;
            }
            else if (kbstate.IsKeyDown(Keys.D3))
            {
                glasses.SelectGlasses = 2;
            }
            else if (kbstate.IsKeyDown(Keys.D4))
            {
                glasses.SelectGlasses = 3;
            }

            if (kbstate.IsKeyDown(Keys.LeftShift))
            {
                speed.X = PlayrBaseSpeed * 10;
            }
            if (kbstate.IsKeyUp(Keys.LeftShift))
            {
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





        }


    }




}
