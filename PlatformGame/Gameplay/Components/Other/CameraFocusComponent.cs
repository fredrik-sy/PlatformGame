namespace PlatformGame.Gameplay.Components.Other
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using PlatformGame.Gameplay.Components.Templates;
    using PlatformGame.Gameplay.Objects.Templates;

    internal class CameraFocusComponent : IComponent
    {
        private const int CameraScreenBottom = CameraScreenTop + 400;
        private const int CameraScreenLeft = 200;
        private const int CameraScreenRight = CameraScreenLeft + 880;
        private const int CameraScreenTop = 200;

        public CameraFocusComponent()
        {
        }

        public int Order => 1;

        public void Update(GameObject gameObject, GameTime gameTime)
        {
            Rectangle screenBounds = Camera.WorldToScreen(gameObject.Bounds);

            if (screenBounds.Left < CameraScreenLeft)
            {
                Camera.Move(new Vector2(screenBounds.Left - CameraScreenLeft, 0));
            }
            else if (screenBounds.Right > CameraScreenRight)
            {
                Camera.Move(new Vector2(screenBounds.Right - CameraScreenRight, 0));
            }

            if (screenBounds.Top < CameraScreenTop)
            {
                Camera.Move(new Vector2(0, screenBounds.Top - CameraScreenTop));
            }
            else if (screenBounds.Bottom > CameraScreenBottom)
            {
                Camera.Move(new Vector2(0, screenBounds.Bottom - CameraScreenBottom));
            }
        }
    }
}
