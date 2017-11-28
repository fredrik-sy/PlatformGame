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
        private Rectangle _cameraScreen;

        public CameraFocusComponent(Rectangle cameraScreen)
        {
            _cameraScreen = cameraScreen;
        }

        public int Order => 1;

        public void Update(GameObject gameObject, GameTime gameTime)
        {
            Rectangle screenBounds = Camera.WorldToScreen(gameObject.Bounds);

            if (screenBounds.Left < _cameraScreen.Left)
            {
                Camera.Move(new Vector2(screenBounds.Left - _cameraScreen.Left, 0));
            }
            else if (screenBounds.Right > _cameraScreen.Right)
            {
                Camera.Move(new Vector2(screenBounds.Right - _cameraScreen.Right, 0));
            }

            if (screenBounds.Top < _cameraScreen.Top)
            {
                Camera.Move(new Vector2(0, screenBounds.Top - _cameraScreen.Top));
            }
            else if (screenBounds.Bottom > _cameraScreen.Bottom)
            {
                Camera.Move(new Vector2(0, screenBounds.Bottom - _cameraScreen.Bottom));
            }
        }
    }
}
