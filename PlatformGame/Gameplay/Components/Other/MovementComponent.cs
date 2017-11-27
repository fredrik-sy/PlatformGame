namespace PlatformGame.Gameplay.Components.Other
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

    internal class MovementComponent : IComponent
    {
        public const int FreeFallSpeed = 20;

        public MovementComponent()
        {
        }

        public int Order => 2;

        public void Update(GameObject gameObject, GameTime gameTime)
        {
            CalculateNextMove(gameObject as MovableGameObject, gameTime);
        }

        private void CalculateNextMove(MovableGameObject movableGameObject, GameTime gameTime)
        {
            movableGameObject.Position += movableGameObject.MoveAmount;

            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 moveAmount = movableGameObject.Velocity * elapsedTime;
            movableGameObject.MoveAmount = moveAmount;

            movableGameObject.VelocityY += FreeFallSpeed;
        }
    }
}
