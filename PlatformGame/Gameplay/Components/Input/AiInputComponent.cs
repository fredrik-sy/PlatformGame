namespace PlatformGame.Gameplay.Components.Input
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using PlatformGame.Gameplay.Components.Templates;
    using PlatformGame.Gameplay.Objects.Templates;

    internal class AiInputComponent : IComponent
    {
        private const float MoveSpeed = 130;

        private float _lastVelocityX;

        public AiInputComponent()
        {
        }

        public int Order => 1;

        public void Update(GameObject gameObject, GameTime gameTime)
        {
            MovableGameObject movableGameObject = gameObject as MovableGameObject;

            if (movableGameObject.VelocityX == 0)
            {
                if (_lastVelocityX != movableGameObject.VelocityX)
                {
                    movableGameObject.VelocityX = -_lastVelocityX;
                }
                else
                {
                    movableGameObject.VelocityX = Game1.Random.Next(2) == 0 ? -MoveSpeed : MoveSpeed;
                }

                _lastVelocityX = movableGameObject.VelocityX;
            }
        }
    }
}
