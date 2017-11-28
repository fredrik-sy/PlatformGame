namespace PlatformGame.Gameplay.Components.Other
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using PlatformGame.Gameplay.Components.Templates;
    using PlatformGame.Gameplay.Objects.Templates;
    using PlatformGame.Shared.IO;

    internal class TeleporterComponent : IComponent
    {
        private List<GameObject> _teleporterObjects;

        public TeleporterComponent(List<GameObject> collisionObjects)
        {
            _teleporterObjects = collisionObjects;
        }

        public int Order => 6;

        public void Update(GameObject gameObject, GameTime gameTime)
        {
            Keys[] keys = KeyboardReader.GetClickedKeys();

            if (keys.Contains(Keys.W))
            {
                foreach (GameObject teleporterObject in _teleporterObjects)
                {
                    if (teleporterObject.Bounds.Contains(gameObject.Bounds))
                    {
                        GameObject[] teleporters = _teleporterObjects.Where(t => t != teleporterObject).ToArray();

                        if (teleporters.Length > 0)
                        {
                            GameObject teleporter = teleporters[Game1.Random.Next(0, teleporters.Length)];
                            gameObject.Position = teleporter.Position + (gameObject.Position - teleporterObject.Position);
                            break;
                        }
                    }
                }
            }
        }
    }
}
