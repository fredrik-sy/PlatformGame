namespace PlatformGame.Gameplay.Components.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PlatformGame.Gameplay.Objects.Templates;

    internal interface IAnimationComponent
    {
        Texture2D Texture { get; }

        void Draw(GameObject gameObject, SpriteBatch spriteBatch);

        void LoadContent(Game game);

        void Update(GameObject gameObject, GameTime gameTime);
    }
}
