namespace PlatformGame.LevelEditor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class RectangleDrawer
    {
        private Color _color;
        private Dictionary<Color, Texture2D> _colors;

        private Rectangle _bottomLine;
        private Rectangle _leftLine;
        private Rectangle _rightLine;
        private Rectangle _topLine;

        public RectangleDrawer(GraphicsDevice graphicsDevice)
        {
            _colors = new Dictionary<Color, Texture2D>()
            {
                [Color.Green] = CreateColorTexture(graphicsDevice, Color.Green),
                [Color.Red] = CreateColorTexture(graphicsDevice, Color.Red),
                [Color.Yellow] = CreateColorTexture(graphicsDevice, Color.Yellow)
            };
        }

        public Color Colour
        {
            set
            {
                _color = value;
            }
        }

        public float LayerDepth { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_colors[_color], _bottomLine, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, LayerDepth);
            spriteBatch.Draw(_colors[_color], _leftLine, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, LayerDepth);
            spriteBatch.Draw(_colors[_color], _rightLine, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, LayerDepth);
            spriteBatch.Draw(_colors[_color], _topLine, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, LayerDepth);
        }

        public void Update(Rectangle rectangle)
        {
            _bottomLine = new Rectangle(rectangle.Left, rectangle.Bottom - 1, rectangle.Width, 1);
            _leftLine = new Rectangle(rectangle.Left, rectangle.Top, 1, rectangle.Height);
            _rightLine = new Rectangle(rectangle.Right - 1, rectangle.Top, 1, rectangle.Height);
            _topLine = new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, 1);
        }

        private Texture2D CreateColorTexture(GraphicsDevice graphicsDevice, Color color)
        {
            Texture2D texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData(new Color[] { color });
            return texture;
        }
    }
}
