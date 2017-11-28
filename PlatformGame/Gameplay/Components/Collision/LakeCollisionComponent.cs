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

    internal class LakeCollisionComponent : IComponent
    {
        private List<GameObject> _collisionObjects;

        public LakeCollisionComponent(List<GameObject> collisionObjects)
        {
            _collisionObjects = collisionObjects;
        }

        public int Order => 5;

        public void Update(GameObject gameObject, GameTime gameTime)
        {
            MovableGameObject movableGameObject = gameObject as MovableGameObject;

            if (movableGameObject.OnGround)
            {
                Rectangle bounds = movableGameObject.Bounds;

                foreach (GameObject collisionObject in _collisionObjects)
                {
                    if (bounds.Intersects(collisionObject.Bounds))
                    {
                        if (Algorithm.PixelCollision(movableGameObject, collisionObject))
                        {
                            (gameObject as MovableGameObject).Dead = true;
                        }
                    }
                }
            }
        }
    }
}
