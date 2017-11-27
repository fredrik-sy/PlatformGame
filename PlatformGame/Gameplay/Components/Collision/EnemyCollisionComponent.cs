namespace PlatformGame.Gameplay.Components.Collision
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PlatformGame.Gameplay.Components.Templates;
    using PlatformGame.Gameplay.Objects.Templates;

    internal class EnemyCollisionComponent : IComponent
    {
        private List<GameObject> _collisionObjects;

        public EnemyCollisionComponent(List<GameObject> collisionObjects)
        {
            _collisionObjects = collisionObjects;
        }

        public int Order => 5;

        public void Update(GameObject gameObject, GameTime gameTime)
        {
            MovableGameObject movableGameObject = gameObject as MovableGameObject;
            Rectangle bounds = movableGameObject.Bounds;

            foreach (GameObject collisionObject in _collisionObjects)
            {
                if (bounds.Intersects(collisionObject.Bounds))
                {
                    if (PixelCollision(movableGameObject, collisionObject))
                    {
                        (gameObject as MovableGameObject).Dead = true;
                    }
                }
            }
        }

        private bool PixelCollision(GameObject movableGameObject, GameObject collisionObject)
        {
            Texture2D texture = movableGameObject.Texture;
            Rectangle bounds = movableGameObject.Bounds;

            Texture2D otherTexture = collisionObject.Texture;
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
