namespace PlatformGame.Shared.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework.Input;

    internal class KeyboardReader
    {
        private static KeyboardState lastKeyboardState;
        private static KeyboardState keyboardState;

        public static Keys[] GetClickedKeys()
        {
            List<Keys> keys = new List<Keys>();

            foreach (Keys key in keyboardState.GetPressedKeys())
            {
                if (keyboardState.IsKeyDown(key) && lastKeyboardState.IsKeyUp(key))
                {
                    keys.Add(key);
                }
            }

            return keys.ToArray();
        }

        public static Keys[] GetPressedKeys() => keyboardState.GetPressedKeys();

        public static void Update()
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
        }
    }
}
