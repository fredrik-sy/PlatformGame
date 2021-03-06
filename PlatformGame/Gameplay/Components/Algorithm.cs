﻿namespace PlatformGame.Gameplay.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PlatformGame.Gameplay.Objects.Templates;

    internal static class Algorithm
    {
        public static bool PixelCollision(GameObject gameObject, GameObject collisionObject)
        {
            Texture2D texture = gameObject.Texture;
            Texture2D otherTexture = collisionObject.Texture;
            Rectangle bounds = gameObject.Bounds;
            Rectangle otherBounds = collisionObject.Bounds;

            Color[] data = new Color[texture.Width * texture.Height];
            texture.GetData(data);

            Color[] otherData = new Color[otherTexture.Width * otherTexture.Height];
            otherTexture.GetData(otherData);

            // Calculate the intersection rectangle.
            int left = Math.Max(bounds.Left, otherBounds.Left);
            int top = Math.Max(bounds.Top, otherBounds.Top);

            int right = Math.Min(bounds.Right, otherBounds.Right);
            int bottom = Math.Min(bounds.Bottom, otherBounds.Bottom);

            // Compare each pixel in the intersection rectangle.
            for (int y = top; y < bottom; ++y)
            {
                for (int x = left; x < right; ++x)
                {
                    Color colorA = data[(x - bounds.Left) + ((y - bounds.Top) * bounds.Width)];
                    Color colorB = otherData[(x - otherBounds.Left) + ((y - otherBounds.Top) * otherBounds.Width)];

                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
