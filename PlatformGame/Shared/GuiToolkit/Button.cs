namespace PlatformGame.Shared.GuiToolkit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PlatformGame.Shared.IO;

    internal class Button
    {
        private ButtonState _buttonState;
        private Rectangle _bounds;

        public Button()
        {
            _bounds = Rectangle.Empty;
        }

        public event EventHandler Click;

        public Rectangle Bounds => _bounds;

        public bool Enabled { get; set; } = true;

        public float LayerDepth { get; set; }

        public Texture2D Texture { get; set; }

        public Dictionary<ButtonState, Rectangle> SourceRectangles { get; set; } = new Dictionary<ButtonState, Rectangle>();

        public int Height
        {
            get { return _bounds.Height; }
            set { _bounds.Height = value; }
        }

        public int Width
        {
            get { return _bounds.Width; }
            set { _bounds.Width = value; }
        }

        public int X
        {
            get { return _bounds.X; }
            set { _bounds.X = value; }
        }

        public int Y
        {
            get { return _bounds.Y; }
            set { _bounds.Y = value; }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Enabled)
            {
                spriteBatch.Draw(Texture, _bounds, SourceRectangles[_buttonState], Color.White, 0, Vector2.Zero, SpriteEffects.None, LayerDepth);
            }
        }

        public virtual void Update()
        {
            if (Enabled)
            {
                if (Bounds.Contains(MouseReader.Position))
                {
                    if (MouseReader.LeftClick())
                    {
                        OnClick(new EventArgs());
                    }
                    else if (MouseReader.LeftPressed())
                    {
                        _buttonState = ButtonState.Pressed;
                    }
                    else
                    {
                        _buttonState = ButtonState.Hover;
                    }
                }
                else
                {
                    _buttonState = ButtonState.Normal;
                }
            }
        }

        protected virtual void OnClick(EventArgs e)
        {
            Click?.Invoke(this, e);
        }
    }
}
