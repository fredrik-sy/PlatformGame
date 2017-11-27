namespace PlatformGame.Gameplay.Objects.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PlatformGame.Gameplay.Components.Templates;

    [Serializable]
    internal abstract class GameObject : ICloneable
    {
        private IAnimationComponent _animationComponent;
        private List<IComponent> _components;

        public GameObject(IAnimationComponent animationComponent, params IComponent[] components)
        {
            _animationComponent = animationComponent;
            _components = components.OrderBy(o => o.Order).ToList();
        }

        public LayerDepth LayerDepth { get; set; } = LayerDepth.Interactive;

        public bool Enabled { get; set; } = true;

        public Texture2D Texture => _animationComponent.Texture;

        public int Height => Texture.Height;

        public int Width => Texture.Width;

        public float X { get; set; }

        public float Y { get; set; }

        public Rectangle Bounds
        {
            get
            {
                Texture2D texture = Texture;
                return new Rectangle((int)X, (int)Y, texture.Width, texture.Height);
            }
        }

        public Vector2 Position
        {
            get
            {
                return new Vector2(X, Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public void AddRangeComponent(params IComponent[] collection)
        {
            _components.AddRange(collection);
            _components = _components.OrderBy(o => o.Order).ToList();
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Enabled && Camera.ObjectIsVisible(this))
            {
                _animationComponent.Draw(this, spriteBatch);
            }
        }

        public void LoadContent(Game game)
        {
            _animationComponent.LoadContent(game);
        }

        public void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                _animationComponent.Update(this, gameTime);

                foreach (IComponent component in _components)
                {
                    component.Update(this, gameTime);
                }
            }
        }
    }
}
