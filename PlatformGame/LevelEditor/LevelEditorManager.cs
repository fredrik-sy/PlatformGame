namespace PlatformGame.LevelEditor
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PlatformGame.Gameplay;
    using PlatformGame.Gameplay.Components.Animation;
    using PlatformGame.Gameplay.Objects;
    using PlatformGame.Gameplay.Objects.Templates;
    using PlatformGame.LevelEditor.Wrapper;
    using PlatformGame.Shared;
    using PlatformGame.Shared.GuiToolkit;
    using PlatformGame.Shared.IO;

    internal class LevelEditorManager : Manager
    {
        public const string LevelDirectory = "Levels";
        public const string LevelExtension = ".level";

        private SpriteBatch _spriteBatch;
        private Toolbox _bottomToolbox;
        private Toolbox _rightToolbox;
        private List<WorldButtonWrapper> _worldButtons;
        private World _activeWorld;
        private LayerDepth _activeLayerDepth;

        public LevelEditorManager(Game1 game) : base(game)
        {
            Height = 800;
            Width = 1400;
        }

        public override bool Active
        {
            set
            {
                if (value)
                {
                    LoadLevels();
                    Camera.WorldRectangle = new Rectangle(0, 0, 5400, 893);
                }

                base.Active = value;
            }
        }

        public override void Initialize()
        {
            _bottomToolbox = new Toolbox()
            {
                Columns = 35,
                LayerDepth = 0.4F,
                Height = 40,
                Width = 1330,
                X = 0,
                Y = 760
            };

            _rightToolbox = new Toolbox()
            {
                Columns = 2,
                LayerDepth = 0.4F,
                Height = 800,
                Width = 70,
                X = 1330,
                Y = 0
            };

            _worldButtons = new List<WorldButtonWrapper>();

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);
            _activeWorld?.Draw(_spriteBatch);
            _bottomToolbox.Draw(_spriteBatch);
            _rightToolbox.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (Game.IsActive)
            {
                _activeWorld?.Update();
                _bottomToolbox.Update();
                _rightToolbox.Update();
            }

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            Texture2D darkGray = new Texture2D(GraphicsDevice, 1, 1);
            darkGray.SetData(new Color[] { new Color(40, 40, 40) });

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _bottomToolbox.Texture = darkGray;
            _rightToolbox.Texture = darkGray;

            LoadButton();
            LoadTile();
            LoadDecoration();
            LoadPlayer();
            LoadEnemy();

            base.LoadContent();
        }

        private void LoadLevels()
        {
            try
            {
                if (Directory.Exists(LevelDirectory))
                {
                    string[] fileName = Directory.GetFiles(LevelDirectory, '*' + LevelExtension);

                    for (int i = 0; i < fileName.Length; i++)
                    {
                        GameObject[] gameObjects = FileHandler.ReadFromBinaryFile<GameObject[]>(fileName[i]);
                        gameObjects.ToList().ForEach(o => o.LoadContent(Game));

                        WorldButtonWrapper worldButton = CreateWorldButton();

                        worldButton.Click += WorldButton_Click;
                        worldButton.World.Items = gameObjects.ToList();

                        _bottomToolbox.Add(worldButton);
                        _worldButtons.Add(worldButton);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private WorldButtonWrapper CreateWorldButton()
        {
            World world = new World(Game)
            {
                Enabled = false,
                Height = 760,
                Width = 1330,
                X = 0,
                Y = 0
            };

            Dictionary<ButtonState, Rectangle> sourceRectangles = new Dictionary<ButtonState, Rectangle>
            {
                [ButtonState.Normal] = new Rectangle(0, 0, 151, 152),
                [ButtonState.Hover] = new Rectangle(166, 0, 151, 152),
                [ButtonState.Pressed] = new Rectangle(332, 0, 151, 152),
                [ButtonState.Disabled] = new Rectangle(498, 0, 151, 152)
            };

            WorldButtonWrapper worldButton = new WorldButtonWrapper(world)
            {
                LayerDepth = 0.42F,
                Texture = Game.Content.Load<Texture2D>("FantasyGameGUI\\WorldButton"),
                SourceRectangles = sourceRectangles
            };

            return worldButton;
        }

        #region BottomToolbox Content
        private void LoadButton()
        {
            Dictionary<ButtonState, Rectangle> sourceRectangles = new Dictionary<ButtonState, Rectangle>()
            {
                [ButtonState.Normal] = new Rectangle(0, 0, 151, 152),
                [ButtonState.Hover] = new Rectangle(166, 0, 151, 152),
                [ButtonState.Pressed] = new Rectangle(332, 0, 151, 152),
                [ButtonState.Disabled] = new Rectangle(498, 0, 151, 152)
            };

            ButtonWrapper homeButton = new ButtonWrapper()
            {
                LayerDepth = 0.42F,
                Texture = Game.Content.Load<Texture2D>("FantasyGameGUI\\HomeButton"),
                SourceRectangles = sourceRectangles
            };

            ButtonWrapper saveButton = new ButtonWrapper()
            {
                LayerDepth = 0.42F,
                Texture = Game.Content.Load<Texture2D>("FantasyGameGUI\\SaveButton"),
                SourceRectangles = sourceRectangles
            };

            ButtonWrapper deleteButton = new ButtonWrapper()
            {
                LayerDepth = 0.42F,
                Texture = Game.Content.Load<Texture2D>("FantasyGameGUI\\DeleteButton"),
                SourceRectangles = sourceRectangles
            };

            ButtonWrapper addButton = new ButtonWrapper()
            {
                LayerDepth = 0.42F,
                Texture = Game.Content.Load<Texture2D>("FantasyGameGUI\\AddButton"),
                SourceRectangles = sourceRectangles
            };

            ButtonWrapper layerButton = new ButtonWrapper()
            {
                LayerDepth = 0.42F,
                Texture = Game.Content.Load<Texture2D>("FantasyGameGUI\\LayerButton"),
                SourceRectangles = sourceRectangles.ToDictionary(p => p.Key, p => p.Value) // Deep copy.
            };

            homeButton.Click += HomeButton_Click;
            saveButton.Click += SaveButton_Click;
            deleteButton.Click += DeleteButton_Click;
            addButton.Click += AddButton_Click;
            layerButton.Click += LayerButton_Click;

            _bottomToolbox.Add(homeButton);
            _bottomToolbox.Add(saveButton);
            _bottomToolbox.Add(deleteButton);
            _bottomToolbox.Add(addButton);
            _bottomToolbox.Add(layerButton);
        }

        #endregion

        #region RightToolbox Content
        private void LoadTile()
        {
            for (int i = 0; i < 16; ++i)
            {
                Tile tile = new Tile(new StillImageAnimationComponent("Winter\\Tile\\" + i));
                tile.LoadContent(Game);
                ItemWrapper item = new ItemWrapper(tile);
                item.Click += Item_Click;
                item.LayerDepth = 0.42F;
                _rightToolbox.Add(item);
            }

            for (int i = 16; i < 18; ++i)
            {
                Lake lake = new Lake(new StillImageAnimationComponent("Winter\\Tile\\" + i));
                lake.LoadContent(Game);
                ItemWrapper item = new ItemWrapper(lake);
                item.Click += Item_Click;
                item.LayerDepth = 0.42F;
                _rightToolbox.Add(item);
            }
        }

        private void LoadDecoration()
        {
            foreach (string s in new string[] { "Crate", "Crystal", "IceBox", "Igloo", "Sign_0", "Sign_1", "SnowMan", "Stone", "Tree_0", "Tree_1" })
            {
                Decoration decoration = new Decoration(new StillImageAnimationComponent("Winter\\Object\\" + s));
                decoration.LoadContent(Game);
                ItemWrapper item = new ItemWrapper(decoration);
                item.Click += Item_Click;
                item.LayerDepth = 0.42F;
                _rightToolbox.Add(item);
            }
        }

        private void LoadPlayer()
        {
            Player player = new Player(new AdventurerGirlAnimationComponent());
            player.LoadContent(Game);
            ItemWrapper item = new ItemWrapper(player);
            item.Click += Item_Click;
            item.LayerDepth = 0.42F;
            _rightToolbox.Add(item);
        }

        private void LoadEnemy()
        {
            Enemy enemy = new Enemy(new ZombieAnimationComponent());
            enemy.LoadContent(Game);
            ItemWrapper item = new ItemWrapper(enemy);
            item.Click += Item_Click;
            item.LayerDepth = 0.42F;
            _rightToolbox.Add(item);
        }
        #endregion

        #region Click Event
        private void AddButton_Click(object sender, EventArgs e)
        {
            World world = new World(Game)
            {
                Enabled = false,
                Height = 760,
                Width = 1330,
                X = 0,
                Y = 0
            };

            Dictionary<ButtonState, Rectangle> sourceRectangles = new Dictionary<ButtonState, Rectangle>
            {
                [ButtonState.Normal] = new Rectangle(0, 0, 151, 152),
                [ButtonState.Hover] = new Rectangle(166, 0, 151, 152),
                [ButtonState.Pressed] = new Rectangle(332, 0, 151, 152),
                [ButtonState.Disabled] = new Rectangle(498, 0, 151, 152)
            };

            WorldButtonWrapper worldButton = new WorldButtonWrapper(world)
            {
                LayerDepth = 0.42F,
                Texture = Game.Content.Load<Texture2D>("FantasyGameGUI\\WorldButton"),
                SourceRectangles = sourceRectangles
            };

            worldButton.Click += WorldButton_Click;

            _bottomToolbox.Add(worldButton);
            _worldButtons.Add(worldButton);
        }

        private void WorldButton_Click(object sender, EventArgs e)
        {
            if (_activeWorld != null)
            {
                _activeWorld.Enabled = false;
            }

            WorldButtonWrapper worldButton = sender as WorldButtonWrapper;

            _activeWorld = worldButton.World;
            _activeWorld.Enabled = true;
            _activeWorld.ActiveLayerDepth = _activeLayerDepth;

            Camera.Position = Vector2.Zero;
            Camera.ViewportWidth = _activeWorld.Width;
            Camera.ViewportHeight = _activeWorld.Height;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (_activeWorld != null)
            {
                WorldButtonWrapper worldButton = _worldButtons.Find(b => b.World == _activeWorld);
                _worldButtons.Remove(worldButton);
                _bottomToolbox.Remove(worldButton);
                _activeWorld = null;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(LevelDirectory))
            {
                Directory.CreateDirectory(LevelDirectory);
            }

            FileHandler.DeleteFiles(LevelDirectory, '*' + LevelExtension);

            for (int i = 0; i < _worldButtons.Count; ++i)
            {
                FileHandler.WriteToBinaryFile(LevelDirectory + "\\" + i + LevelExtension, _worldButtons[i].World.Items.ToArray());
            }
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            Game.GameState = GameState.StartMenu;
        }

        private void LayerButton_Click(object sender, EventArgs e)
        {
            if (_activeWorld != null)
            {
                Dictionary<ButtonState, Rectangle> sourceRectangles = (sender as ButtonWrapper).SourceRectangles;
                int paddingLeft = 167;
                int maxPaddingLeft = 668;

                foreach (ButtonState buttonState in Enum.GetValues(typeof(ButtonState)))
                {
                    Rectangle rectangle = sourceRectangles[buttonState];
                    rectangle.Y = (rectangle.Y + paddingLeft) % maxPaddingLeft;
                    sourceRectangles[buttonState] = rectangle;
                }

                _activeLayerDepth = (LayerDepth)((int)++_activeLayerDepth % Enum.GetValues(typeof(LayerDepth)).Length);
                _activeWorld.ActiveLayerDepth = _activeLayerDepth;
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            if (_activeWorld != null)
            {
                ItemWrapper item = sender as ItemWrapper;
                _activeWorld.DndItem = item.GameObject;
            }
        }
        #endregion
    }
}
