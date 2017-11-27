namespace PlatformGame.Gameplay.Objects.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using PlatformGame.Gameplay.Components.Templates;

    [Serializable]
    internal class MovableGameObject : GameObject
    {
        public MovableGameObject(IAnimationComponent animationComponent, params IComponent[] components) : base(animationComponent, components)
        {
        }

        public bool Dead { get; set; }

        public bool OnGround { get; set; }

        public float MoveAmountX { get; set; }

        public float MoveAmountY { get; set; }

        public float VelocityX { get; set; }

        public float VelocityY { get; set; }

        public Vector2 MoveAmount
        {
            get
            {
                return new Vector2(MoveAmountX, MoveAmountY);
            }
            set
            {
                MoveAmountX = value.X;
                MoveAmountY = value.Y;
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return new Vector2(VelocityX, VelocityY);
            }
            set
            {
                VelocityX = value.X;
                VelocityY = value.Y;
            }
        }
    }
}
