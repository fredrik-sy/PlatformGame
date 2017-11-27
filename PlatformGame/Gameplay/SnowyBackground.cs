namespace PlatformGame.Gameplay
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class SnowyBackground
    {
        private const string AssetName = "Winter\\Background\\Background";

        private Vector2 _leftBackgroundPosition;
        private Vector2 _rightBackgroundPosition;
        private Texture2D _texture;

        public SnowyBackground()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Camera.WorldToScreen(_leftBackgroundPosition), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(_texture, Camera.WorldToScreen(_rightBackgroundPosition), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public void LoadContent(Game game)
        {
            _texture = game.Content.Load<Texture2D>(AssetName);
        }

        public void Update()
        {
            int leftBackgroundIndex = (int)Camera.Position.X / _texture.Width;
            int rightBackgroundIndex = ((int)Camera.Position.X + Camera.ViewportWidth) / _texture.Width;
            _leftBackgroundPosition = new Vector2(leftBackgroundIndex * _texture.Width, 0);
            _rightBackgroundPosition = new Vector2(rightBackgroundIndex * _texture.Width, 0);
        }
    }
}
