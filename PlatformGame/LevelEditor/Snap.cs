namespace PlatformGame.LevelEditor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;

    internal static class Snap
    {
        private const int AutoSnapDistance = 20;

        public static Vector2? FindCornerPosition(Vector2? position, Rectangle source, Rectangle[] destinations)
        {
            foreach (Rectangle destination in destinations)
            {
                Vector2 bottom = new Vector2(destination.Left, destination.Bottom);
                Vector2 left = new Vector2(destination.Left - source.Width, destination.Top);
                Vector2 leftBottomCorner = new Vector2(destination.Left - source.Width, destination.Bottom);
                Vector2 leftTopCorner = new Vector2(destination.Left - source.Width, destination.Top - source.Height);
                Vector2 right = new Vector2(destination.Right, destination.Top);
                Vector2 rightBottomCorner = new Vector2(destination.Right, destination.Bottom);
                Vector2 rightTopCorner = new Vector2(destination.Right, destination.Y - source.Height);
                Vector2 top = new Vector2(destination.Left, destination.Top - source.Height);

                Vector2[] corners = new Vector2[]
                {
                    bottom,
                    left,
                    leftBottomCorner,
                    leftTopCorner,
                    right,
                    rightBottomCorner,
                    rightTopCorner,
                    top
                };

                foreach (Vector2 corner in corners)
                {
                    if (Vector2.Distance(source.Location.ToVector2(), corner) < AutoSnapDistance)
                    {
                        return corner;
                    }
                }
            }

            return position;
        }

        public static Vector2? FindContainerBounds(Vector2? position, Rectangle source, Rectangle destination)
        {
            Vector2 leftBottomCorner = new Vector2(destination.Left, destination.Bottom - source.Height);
            Vector2 leftTopCorner = new Vector2(destination.Left, destination.Top);
            Vector2 rightBottomCorner = new Vector2(destination.Right - source.Width, destination.Bottom - source.Height);
            Vector2 rightTopCorner = new Vector2(destination.Right - source.Width, destination.Top);
            Vector2 bottom = new Vector2(source.Left, destination.Bottom - source.Height);
            Vector2 left = new Vector2(destination.Left, source.Top);
            Vector2 right = new Vector2(destination.Right - source.Width, source.Top);
            Vector2 top = new Vector2(source.Left, destination.Top);

            Vector2[] corners = new Vector2[]
            {
                    leftBottomCorner,
                    leftTopCorner,
                    rightBottomCorner,
                    rightTopCorner,
                    bottom,
                    left,
                    right,
                    top
            };

            foreach (Vector2 corner in corners)
            {
                if (Vector2.Distance(source.Location.ToVector2(), corner) < AutoSnapDistance)
                {
                    return corner;
                }
            }

            return position;
        }
    }
}
