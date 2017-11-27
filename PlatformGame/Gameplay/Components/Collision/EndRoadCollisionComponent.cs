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

    internal class EndRoadCollisionComponent : IComponent
    {
        private List<GameObject> _collisionObjects;

        public EndRoadCollisionComponent(List<GameObject> collisionObjectList)
        {
            _collisionObjects = collisionObjectList;
        }

        public int Order => 3;

        public void Update(GameObject gameObject, GameTime gameTime)
        {
            ReverseDirectionOnEndRoad(gameObject as MovableGameObject);
        }

        private void ReverseDirectionOnEndRoad(MovableGameObject movableGameObject)
        {
            if (movableGameObject.VelocityX != 0)
            {
                Vector2 position = movableGameObject.Position;
                position += movableGameObject.MoveAmount;

                Rectangle bounds = new Rectangle((int)position.X, (int)position.Y, movableGameObject.Width, movableGameObject.Height);

                int floorCollisionWidth = 0;

                foreach (GameObject gameObject in _collisionObjects)
                {
                    Rectangle gameObjectBounds = gameObject.Bounds;

                    if (bounds.Intersects(gameObjectBounds))
                    {
                        floorCollisionWidth += Rectangle.Intersect(bounds, gameObjectBounds).Width;
                    }
                }

                if (floorCollisionWidth > 0 && floorCollisionWidth < bounds.Width)
                {
                    movableGameObject.VelocityX = 0;
                }
            }
        }
    }
}
