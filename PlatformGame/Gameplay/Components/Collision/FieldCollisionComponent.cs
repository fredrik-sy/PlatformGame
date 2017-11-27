namespace PlatformGame.Gameplay.Components.Collision
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using PlatformGame.Gameplay.Components.Templates;
    using PlatformGame.Gameplay.Objects.Templates;

    internal class FieldCollisionComponent : IComponent
    {
        private List<GameObject> _collisionObjects;

        public FieldCollisionComponent(List<GameObject> collisionObjects)
        {
            _collisionObjects = collisionObjects;
        }

        public int Order => 4;

        public void Update(GameObject gameObject, GameTime gameTime)
        {
            gameObject.Position = new Vector2(MathHelper.Clamp(gameObject.X, Camera.WorldRectangle.X, Camera.WorldRectangle.Width - gameObject.Width),
                                              MathHelper.Clamp(gameObject.Y, Camera.WorldRectangle.Y, Camera.WorldRectangle.Height - gameObject.Height));

            if (gameObject.Y + gameObject.Height == Camera.WorldRectangle.Bottom)
            {
                (gameObject as MovableGameObject).Dead = true;
            }

            HorizontalCollision(gameObject as MovableGameObject);
            VerticalCollision(gameObject as MovableGameObject);
        }

        private void HorizontalCollision(MovableGameObject movableGameObject)
        {
            if (movableGameObject.MoveAmountX != 0)
            {
                Vector2 position = movableGameObject.Position;
                position.X += movableGameObject.MoveAmountX;

                Rectangle bounds = new Rectangle((int)position.X, (int)position.Y, movableGameObject.Width, movableGameObject.Height);

                foreach (GameObject collisionObject in _collisionObjects)
                {
                    if (bounds.Intersects(collisionObject.Bounds))
                    {
                        movableGameObject.MoveAmountX = 0;
                        break;
                    }
                }
            }
        }

        private void VerticalCollision(MovableGameObject movableGameObject)
        {
            if (movableGameObject.MoveAmountY != 0)
            {
                Vector2 position = movableGameObject.Position;
                position += movableGameObject.MoveAmount;

                Rectangle bounds = new Rectangle((int)position.X, (int)position.Y, movableGameObject.Width, movableGameObject.Height);

                foreach (GameObject collisionObject in _collisionObjects)
                {
                    if (bounds.Intersects(collisionObject.Bounds))
                    {
                        if (movableGameObject.MoveAmountY > 0)
                        {
                            movableGameObject.OnGround = true;
                        }

                        movableGameObject.VelocityY = 0;
                        movableGameObject.MoveAmountY = 0;
                        break;
                    }
                }
            }
        }
    }
}
