namespace PlatformGame.Gameplay
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using PlatformGame.Gameplay.Objects.Templates;

    internal static class Camera
    {
        private static Vector2 position = Vector2.Zero;
        private static Vector2 viewportSize = Vector2.Zero;

        public static Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = new Vector2(MathHelper.Clamp(value.X, WorldRectangle.X, WorldRectangle.Width - ViewportWidth),
                                       MathHelper.Clamp(value.Y, WorldRectangle.Y, WorldRectangle.Height - ViewportHeight));
            }
        }

        public static Rectangle Viewport => new Rectangle((int)Position.X, (int)Position.Y, ViewportWidth, ViewportHeight);

        public static Rectangle WorldRectangle { get; set; } = Rectangle.Empty;

        public static int ViewportWidth
        {
            get { return (int)viewportSize.X; }
            set { viewportSize.X = value; }
        }

        public static int ViewportHeight
        {
            get { return (int)viewportSize.Y; }
            set { viewportSize.Y = value; }
        }

        public static void Move(Vector2 offset)
        {
            Position += offset;
        }

        public static bool ObjectIsVisible(GameObject gameObject)
        {
            return Viewport.Intersects(gameObject.Bounds);
        }

        public static Rectangle ScreenToWorld(Rectangle screenRectangle)
        {
            return new Rectangle(screenRectangle.X + (int)position.X,
                                 screenRectangle.Y + (int)position.Y,
                                 screenRectangle.Width,
                                 screenRectangle.Height);
        }

        public static Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return screenPosition + position;
        }

        public static Rectangle WorldToScreen(Rectangle worldRectangle)
        {
            return new Rectangle(worldRectangle.X - (int)position.X,
                                 worldRectangle.Y - (int)position.Y,
                                 worldRectangle.Width,
                                 worldRectangle.Height);
        }

        public static Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return worldPosition - position;
        }
    }
}
