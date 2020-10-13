namespace PlatformGame.Gameplay
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class SnowyBackground
    {
        private const int BackgroundSpeed = 1;
        private const int MiddlegroundSpeed = 5;

        private Texture2D _background;
        private Texture2D _middleground1;
        private Texture2D _middleground2;

        private Vector2 _leftBackgroundPosition;
        private Vector2 _rightBackgroundPosition;

        private Vector2 _leftMiddlegroundPosition1;
        private Vector2 _rightMiddlegroundPosition1;

        private Vector2 _leftMiddlegroundPosition2;
        private Vector2 _rightMiddlegroundPosition2;

        private float _cameraPositionX;

        public SnowyBackground()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, Camera.WorldToScreen(_leftBackgroundPosition), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(_background, Camera.WorldToScreen(_rightBackgroundPosition), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(_middleground1, Camera.WorldToScreen(_leftMiddlegroundPosition1), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0.01F);
            spriteBatch.Draw(_middleground1, Camera.WorldToScreen(_rightMiddlegroundPosition1), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0.01F);
            spriteBatch.Draw(_middleground2, Camera.WorldToScreen(_leftMiddlegroundPosition1), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0.01F);
            spriteBatch.Draw(_middleground2, Camera.WorldToScreen(_rightMiddlegroundPosition1), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0.01F);
        }

        public void LoadContent(Game game)
        {
            _background = game.Content.Load<Texture2D>("Forest\\Background");
            _leftBackgroundPosition = new Vector2(0, 0);
            _rightBackgroundPosition = new Vector2(_background.Width, 0);

            _middleground1 = game.Content.Load<Texture2D>("Forest\\Clouds_1");
            _leftMiddlegroundPosition1 = new Vector2(0, 0);
            _rightMiddlegroundPosition1 = new Vector2(_middleground1.Width, 0);

            _middleground2 = game.Content.Load<Texture2D>("Forest\\Clouds_2");
            _leftMiddlegroundPosition1 = new Vector2(0, 0);
            _rightMiddlegroundPosition1 = new Vector2(_middleground2.Width, 0);

            _cameraPositionX = Camera.Position.X;
        }

        public void Update(GameTime gameTime)
        {
            ChangePosition(_background, ref _leftBackgroundPosition, ref _rightBackgroundPosition);
            ChangePosition(_middleground1, ref _leftMiddlegroundPosition1, ref _rightMiddlegroundPosition1);
            ChangePosition(_middleground2, ref _leftMiddlegroundPosition2, ref _rightMiddlegroundPosition2);
            UpdateCamera(_background, BackgroundSpeed);
            UpdateCamera(_middleground1, MiddlegroundSpeed);
            UpdateCamera(_middleground2, MiddlegroundSpeed);
        }

        private void UpdateCamera(Texture2D texture, int speed)
        {
            if (_cameraPositionX != Camera.Position.X)
            {
                _leftMiddlegroundPosition1.X += (_cameraPositionX - Camera.Position.X) * speed;
                _rightMiddlegroundPosition1.X += (_cameraPositionX - Camera.Position.X) * speed;
                _cameraPositionX = Camera.Position.X;
            }
        }

        private void ChangePosition(Texture2D texture, ref Vector2 left, ref Vector2 right)
        {
            if (left.X < right.X && Camera.Position.X >= right.X)
            {
                left.X = right.X + texture.Width;
            }

            if (right.X < left.X && Camera.Position.X >= left.X)
            {
                right.X = left.X + texture.Width;
            }

            if (left.X < right.X && Camera.Position.X < left.X)
            {
                right.X = left.X - texture.Width;
            }

            if (right.X < left.X && Camera.Position.X < right.X)
            {
                left.X = right.X - texture.Width;
            }
        }
    }
}
