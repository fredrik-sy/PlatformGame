namespace PlatformGame.Shared.GuiToolkit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using PlatformGame.Shared.GuiToolkit.Templates;

    internal class Toolbox
    {
        private Rectangle _bounds;
        private List<IItem> _items;

        public Toolbox()
        {
            _bounds = Rectangle.Empty;
            _items = new List<IItem>();
        }

        public float Alpha { get; set; } = 1;

        public Rectangle Bounds => _bounds;

        public int Columns { get; set; }

        public bool Enabled { get; set; } = true;

        public float LayerDepth { get; set; }

        public Texture2D Texture { get; set; }

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

        public void Add(IItem item)
        {
            _items.Add(item);
            Refresh();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Bounds, null, Color.White * Alpha, 0, Vector2.Zero, SpriteEffects.None, LayerDepth);

            // Avoid asynchronous issue.
            IItem[] items = _items.ToArray();

            foreach (IItem item in items)
            {
                if (_bounds.Contains(item.Bounds))
                {
                    item.Draw(spriteBatch);
                }
            }
        }

        public void Remove(IItem item)
        {
            _items.Remove(item);
            Refresh();
        }

        public void RemoveAll(Predicate<IItem> match)
        {
            _items.RemoveAll(match);
            Refresh();
        }

        public void Refresh()
        {
            Rectangle contentBounds = _bounds;
            contentBounds.Inflate(-2, -2);

            for (int i = 0; i < _items.Count; ++i)
            {
                int size = contentBounds.Width / Columns;
                _items[i].Height = size;
                _items[i].Width = size;
                _items[i].X = contentBounds.X + (size * (i % Columns));
                _items[i].Y = contentBounds.Y + (size * (i / Columns));
            }
        }

        public void Update()
        {
            // Avoid asynchronous issue.
            IItem[] items = _items.ToArray();

            foreach (IItem item in items)
            {
                item.Update();
            }
        }
    }
}
