namespace PlatformGame.LevelEditor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using PlatformGame.Gameplay;
    using PlatformGame.Gameplay.Objects.Templates;
    using PlatformGame.Shared.IO;

    internal class World
    {
        private SnowyBackground _snowyBackground;
        private RectangleDrawer _rectangleDrawer;
        private Vector2? _snapPosition;

        public World(Game1 game)
        {
            _snowyBackground = new SnowyBackground();
            _snowyBackground.LoadContent(game);
            _rectangleDrawer = new RectangleDrawer(game.GraphicsDevice)
            {
                Colour = Color.Red,
                LayerDepth = 0.34F
            };
        }

        public LayerDepth ActiveLayerDepth { get; set; } = LayerDepth.None;

        public bool Enabled { get; set; } = true;

        public Rectangle Bounds => new Rectangle(X, Y, Width, Height);

        public int Height { get; set; }

        public int Width { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public GameObject DndItem { get; set; }

        public List<GameObject> Items { get; set; } = new List<GameObject>();

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Enabled)
            {
                _snowyBackground.Draw(spriteBatch);

                foreach (GameObject item in Items)
                {
                    if (ActiveLayerDepth == LayerDepth.None || ActiveLayerDepth == item.LayerDepth)
                    {
                        float layerDepth = item.LayerDepth.ToFloat();
                        spriteBatch.Draw(item.Texture, Camera.WorldToScreen(item.Position), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, layerDepth);
                    }
                }

                Vector2 mousePosition = MouseReader.Position.ToVector2();

                if (Bounds.Contains(mousePosition))
                {
                    if (DndItem != null)
                    {
                        float layerDepth = DndItem.LayerDepth.ToFloat();
                        _rectangleDrawer.Draw(spriteBatch);
                        spriteBatch.Draw(DndItem.Texture, _snapPosition.GetValueOrDefault(mousePosition), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, layerDepth);
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                _snowyBackground.Update(gameTime);

                UpdateCamera();

                if (DndItem != null)
                {
                    Vector2 mousePosition = MouseReader.Position.ToVector2();

                    if (Bounds.Contains(mousePosition))
                    {
                        FindSnapPosition(mousePosition);
                        UpdateDndLayer();
                        UpdateRectangleDrawer(mousePosition);

                        if (MouseReader.LeftClick())
                        {
                            GameObject gameObject = (GameObject)DndItem.Clone();
                            gameObject.Position = Camera.ScreenToWorld(_snapPosition.GetValueOrDefault(mousePosition));
                            Items.Add(gameObject);
                        }

                        if (MouseReader.RightClick())
                        {
                            for (int i = Items.Count - 1; i >= 0; --i)
                            {
                                if (Items[i].Bounds.Contains(Camera.ScreenToWorld(mousePosition)))
                                {
                                    Items.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void FindSnapPosition(Vector2 mousePosition)
        {
            _snapPosition = null;

            if (!KeyboardReader.GetPressedKeys().Any(k => k == Keys.LeftControl || k == Keys.RightControl))
            {
                List<Rectangle> destinations = Items.Where(o => Camera.ObjectIsVisible(o))
                                                          .Select(o => Camera.WorldToScreen(o.Bounds))
                                                          .ToList();

                Rectangle source = DndItem.Bounds;
                source.X = (int)mousePosition.X;
                source.Y = (int)mousePosition.Y;

                _snapPosition = Snap.FindContainerBounds(_snapPosition, source, Bounds);
                _snapPosition = Snap.FindCornerPosition(_snapPosition, source, destinations.ToArray());
            }
        }

        private void UpdateCamera()
        {
            foreach (Keys key in KeyboardReader.GetPressedKeys())
            {
                switch (key)
                {
                    case Keys.Down:
                        Camera.Move(new Vector2(0, 5));
                        break;
                    case Keys.Left:
                        Camera.Move(new Vector2(-5, 0));
                        break;
                    case Keys.Right:
                        Camera.Move(new Vector2(5, 0));
                        break;
                    case Keys.Up:
                        Camera.Move(new Vector2(0, -5));
                        break;
                    default:
                        break;
                }
            }
        }

        private void UpdateDndLayer()
        {
            if (KeyboardReader.GetClickedKeys().Contains(Keys.L))
            {
                DndItem.LayerDepth = Enum.GetValues(typeof(LayerDepth))
                                     .Cast<LayerDepth>()
                                     .Where(l => l != LayerDepth.None)
                                     .ElementAt((int)DndItem.LayerDepth % 3);
            }
        }

        private void UpdateRectangleDrawer(Vector2 mousePosition)
        {
            switch (DndItem.LayerDepth)
            {
                case LayerDepth.Background:
                    _rectangleDrawer.Colour = Color.Red;
                    break;
                case LayerDepth.Foreground:
                    _rectangleDrawer.Colour = Color.Yellow;
                    break;
                case LayerDepth.Interactive:
                    _rectangleDrawer.Colour = Color.Green;
                    break;
            }

            _rectangleDrawer.Update(new Rectangle(_snapPosition.GetValueOrDefault(mousePosition).ToPoint(), new Point(DndItem.Width, DndItem.Height)));
        }
    }
}
