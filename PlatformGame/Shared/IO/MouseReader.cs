namespace PlatformGame.Shared.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    internal class MouseReader
    {
        private static MouseState lastMouseState;
        private static MouseState mouseState;

        public static Point Position => mouseState.Position;

        public static bool LeftClick()
        {
            return mouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool LeftPressed()
        {
            return mouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool LeftReleased()
        {
            return mouseState.LeftButton == ButtonState.Released;
        }

        public static bool RightClick()
        {
            return mouseState.RightButton == ButtonState.Pressed && lastMouseState.RightButton == ButtonState.Released;
        }

        public static void Update()
        {
            lastMouseState = mouseState;
            mouseState = Mouse.GetState();
        }
    }
}
