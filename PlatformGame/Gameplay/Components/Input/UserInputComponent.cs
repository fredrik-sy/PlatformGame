namespace PlatformGame.Gameplay.Components.Input
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using PlatformGame.Gameplay.Components.Templates;
    using PlatformGame.Gameplay.Objects.Templates;

    internal class UserInputComponent : IComponent
    {
        private const float JumpPower = -600;
        private const float MoveSpeed = 200;

        public UserInputComponent()
        {
        }

        public int Order => 1;

        public void Update(GameObject gameObject, GameTime gameTime)
        {
            MovableGameObject movableGameObject = gameObject as MovableGameObject;

            movableGameObject.VelocityX = 0;

            if (!movableGameObject.Dead)
            {
                foreach (Keys key in Keyboard.GetState().GetPressedKeys())
                {
                    switch (key)
                    {
                        case Keys.A:
                            movableGameObject.VelocityX = -MoveSpeed;
                            break;
                        case Keys.D:
                            movableGameObject.VelocityX = MoveSpeed;
                            break;
                        case Keys.Space:
                            Jump(movableGameObject);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void Jump(MovableGameObject movableGameObject)
        {
            if (movableGameObject.OnGround)
            {
                movableGameObject.OnGround = false;
                movableGameObject.VelocityY = JumpPower;
            }
        }
    }
}
