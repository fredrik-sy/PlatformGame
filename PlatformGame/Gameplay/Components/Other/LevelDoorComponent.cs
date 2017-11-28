namespace PlatformGame.Gameplay.Components.Other
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
    using PlatformGame.Shared.IO;

    internal class LevelDoorComponent : IComponent
    {
        private GameplayManager _gameplayManager;
        private List<GameObject> _collisionObjects;

        public LevelDoorComponent(GameplayManager gameplayManager, List<GameObject> collisionObjects)
        {
            _gameplayManager = gameplayManager;
            _collisionObjects = collisionObjects;
        }

        public int Order => 6;

        public void Update(GameObject gameObject, GameTime gameTime)
        {
            Keys[] keys = KeyboardReader.GetClickedKeys();

            if (keys.Contains(Keys.W))
            {
                foreach (GameObject collisionObject in _collisionObjects)
                {
                    if (collisionObject.Bounds.Contains(gameObject.Bounds))
                    {
                        _gameplayManager.NextLevel();
                    }
                }
            }
        }
    }
}
