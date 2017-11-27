namespace PlatformGame.Gameplay.Components.Animation
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

    [Serializable]
    internal class ZombieAnimationComponent : IAnimationComponent
    {
        private const int MillisecondsPerFrame = 100;

        private AnimationState _animationState;
        private SpriteEffects _effects;
        private bool _loopAnimation;
        private double _timeSinceLastFrame;
        private int _frameIndex;

        [NonSerialized]
        private Texture2D _texture;

        [NonSerialized]
        private Texture2D[][] _textures;

        public ZombieAnimationComponent()
        {
        }

        private enum AnimationState
        {
            Idle,
            Walk
        }

        public Texture2D Texture => _texture;

        public void Draw(GameObject gameObject, SpriteBatch spriteBatch)
        {
            float layerDepth = gameObject.LayerDepth.ToFloat();
            spriteBatch.Draw(Texture, Camera.WorldToScreen(gameObject.Position), null, Color.White, 0, Vector2.Zero, 1, _effects, layerDepth);
        }

        public void LoadContent(Game game)
        {
            _textures = new Texture2D[2][];
            _textures[(int)AnimationState.Idle] = new Texture2D[10];
            _textures[(int)AnimationState.Walk] = new Texture2D[10];

            foreach (AnimationState animationState in Enum.GetValues(typeof(AnimationState)).Cast<AnimationState>())
            {
                for (int i = 0; i < _textures[(int)animationState].Length; i++)
                {
                    _textures[(int)animationState][i] = game.Content.Load<Texture2D>("Zombie\\" + animationState.ToString() + "\\" + i);
                }
            }

            _texture = _textures[(int)AnimationState.Idle][0];
        }

        public void Update(GameObject gameObject, GameTime gameTime)
        {
            UpdateAnimationState(gameObject as MovableGameObject);
            UpdateFrameIndex(gameTime);
            _texture = _textures[(int)_animationState][_frameIndex];
        }

        private void UpdateAnimationState(MovableGameObject movableGameObject)
        {
            AnimationState animationState = _animationState;

            if (movableGameObject.OnGround)
            {
                if (movableGameObject.Velocity == Vector2.Zero)
                {
                    animationState = AnimationState.Idle;
                    _loopAnimation = true;
                }

                if (movableGameObject.VelocityX > 0)
                {
                    animationState = AnimationState.Walk;
                    _effects = SpriteEffects.None;
                    _loopAnimation = true;
                }

                if (movableGameObject.VelocityX < 0)
                {
                    animationState = AnimationState.Walk;
                    _effects = SpriteEffects.FlipHorizontally;
                    _loopAnimation = true;
                }
            }
            else
            {
                if (movableGameObject.VelocityX > 0)
                {
                    _effects = SpriteEffects.None;
                }

                if (movableGameObject.VelocityX < 0)
                {
                    _effects = SpriteEffects.FlipHorizontally;
                }
            }

            if (movableGameObject.Dead)
            {
                // animationState = AnimationState.Dead;
                _loopAnimation = false;
            }

            if (_animationState != animationState)
            {
                _frameIndex = 0;
                _animationState = animationState;
            }
        }

        private void UpdateFrameIndex(GameTime gameTime)
        {
            _timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_timeSinceLastFrame >= MillisecondsPerFrame)
            {
                _timeSinceLastFrame %= MillisecondsPerFrame;

                if (_loopAnimation)
                {
                    _frameIndex++;
                    _frameIndex %= _textures[(int)_animationState].Length;
                }
                else
                {
                    int length = _textures[(int)_animationState].Length;

                    if (_frameIndex < length - 1)
                    {
                        _frameIndex++;
                    }
                }
            }
        }
    }
}
