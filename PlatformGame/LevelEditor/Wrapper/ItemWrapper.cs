namespace PlatformGame.LevelEditor.Wrapper
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PlatformGame.Gameplay.Objects.Templates;
    using PlatformGame.Shared.GuiToolkit.Templates;
    using PlatformGame.Shared.IO;

    internal class ItemWrapper : IItem
    {
        private GameObject _gameObject;
        private Rectangle _bounds;

        public ItemWrapper(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public event EventHandler<EventArgs> Click;

        public Rectangle Bounds => _bounds;

        public GameObject GameObject => _gameObject;

        public float LayerDepth { get; set; }

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

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = _gameObject.Texture;

            Rectangle innerBounds = Bounds;
            innerBounds.Inflate(-2, -2);

            float scaleX = (float)innerBounds.Width / texture.Width;
            float scaleY = (float)innerBounds.Height / texture.Height;
            float scale = scaleX > scaleY ? scaleY : scaleX;

            spriteBatch.Draw(texture, innerBounds.Location.ToVector2(), null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, LayerDepth);
        }

        public void Update()
        {
            if (MouseReader.LeftClick())
            {
                Point position = MouseReader.Position;

                if (Bounds.Contains(position))
                {
                    OnClick(new EventArgs());
                }
            }
        }

        private void OnClick(EventArgs e)
        {
            Click?.Invoke(this, e);
        }
    }
}
