namespace PlatformGame.Gameplay
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PlatformGame.Gameplay.Components.Collision;
    using PlatformGame.Gameplay.Components.Input;
    using PlatformGame.Gameplay.Components.Other;
    using PlatformGame.Gameplay.Objects;
    using PlatformGame.Gameplay.Objects.Templates;
    using PlatformGame.LevelEditor;
    using PlatformGame.Shared;
    using PlatformGame.Shared.IO;

    internal class GameplayManager : Manager
    {
        private const int ExitTimeAfterPlayerDied = 2000;

        private List<GameObject[]> _gameObjectArrays;
        private SpriteBatch _spriteBatch;
        private SnowyBackground _snowyBackground;

        private double _timeSincePlayerDied;

        public GameplayManager(Game1 game) : base(game)
        {
            _gameObjectArrays = new List<GameObject[]>();
            _snowyBackground = new SnowyBackground();
            Height = 800;
            Width = 1280;
        }

        public override bool Active
        {
            set
            {
                if (value)
                {
                    Camera.WorldRectangle = new Rectangle(0, 0, 5400, 893);
                    Camera.Position = Vector2.Zero;
                    Camera.ViewportWidth = Width;
                    Camera.ViewportHeight = Height;
                    LoadLevels();
                }

                base.Active = value;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);
            _snowyBackground.Draw(_spriteBatch);

            if (_gameObjectArrays.Count > 0)
            {
                foreach (GameObject gameObject in _gameObjectArrays.First())
                {
                    gameObject.Draw(_spriteBatch);
                }
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            _snowyBackground.Update();

            if (_gameObjectArrays.Count > 0)
            {
                foreach (GameObject gameObject in _gameObjectArrays.First())
                {
                    if (gameObject is Player player)
                    {
                        if (player.Dead)
                        {
                            _timeSincePlayerDied += gameTime.ElapsedGameTime.TotalMilliseconds;

                            if (_timeSincePlayerDied >= ExitTimeAfterPlayerDied)
                            {
                                _gameObjectArrays.Clear();
                                Game.GameState = GameState.StartMenu;
                            }
                        }
                    }

                    gameObject.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _snowyBackground.LoadContent(Game);
            base.LoadContent();
        }

        private void LoadLevels()
        {
            if (Directory.Exists(LevelEditorManager.LevelDirectory))
            {
                string[] filename = Directory.GetFiles(LevelEditorManager.LevelDirectory, '*' + LevelEditorManager.LevelExtension);

                for (int i = 0; i < filename.Length; i++)
                {
                    GameObject[] gameObjects = FileHandler.ReadFromBinaryFile<GameObject[]>(filename[i]);
                    gameObjects.ToList().ForEach(o => o.LoadContent(Game));
                    LoadComponent(gameObjects);
                    _gameObjectArrays.Add(gameObjects);
                }
            }

            _timeSincePlayerDied = 0;
        }

        private void LoadComponent(GameObject[] gameObjects)
        {
            List<GameObject> fieldCollisionObjects = new List<GameObject>();
            List<GameObject> enemyCollisionObjects = new List<GameObject>();
            List<GameObject> lakeCollisionObjects = new List<GameObject>();
            Player player = null;

            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject is Decoration || gameObject is Tile)
                {
                    if (gameObject.LayerDepth == LayerDepth.Interactive)
                    {
                        fieldCollisionObjects.Add(gameObject);
                    }
                }
                else if (gameObject is Enemy)
                {
                    enemyCollisionObjects.Add(gameObject);
                }
                else if (gameObject is Lake)
                {
                    lakeCollisionObjects.Add(gameObject);
                }
                else if (gameObject is Player)
                {
                    player = gameObject as Player;
                }
            }

            enemyCollisionObjects.ForEach(e =>
            {
                e?.AddRangeComponent(new FieldCollisionComponent(fieldCollisionObjects),
                                     new MovementComponent(),
                                     new AiInputComponent(),
                                     new EndRoadCollisionComponent(fieldCollisionObjects));
            });

            player?.AddRangeComponent(new FieldCollisionComponent(fieldCollisionObjects),
                                      new MovementComponent(),
                                      new UserInputComponent(),
                                      new EnemyCollisionComponent(enemyCollisionObjects.Concat(lakeCollisionObjects).ToList()),
                                      new CameraFocusComponent());
        }
    }
}
