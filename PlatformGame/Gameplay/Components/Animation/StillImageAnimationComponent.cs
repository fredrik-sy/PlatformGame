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
    internal class StillImageAnimationComponent : IAnimationComponent
    {
        private string _assetName;

        [NonSerialized]
        private Texture2D _texture;

        public StillImageAnimationComponent(string assetName)
        {
            _assetName = assetName;
        }

        public Texture2D Texture => _texture;

        public void Draw(GameObject gameObject, SpriteBatch spriteBatch)
        {
            float layerDepth = gameObject.LayerDepth.ToFloat();
            spriteBatch.Draw(_texture, Camera.WorldToScreen(gameObject.Position), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, layerDepth);
        }

        public void LoadContent(Game game)
        {
            _texture = game.Content.Load<Texture2D>(_assetName);
        }

        public void Update(GameObject gameObject, GameTime gameTime)
        {
        }
    }
}
