namespace PlatformGame.StartMenu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PlatformGame.Shared;
    using PlatformGame.Shared.GuiToolkit;

    internal class StartMenuManager : Manager
    {
        private Texture2D _backgroundImage;
        private SpriteBatch _spriteBatch;
        private Button _levelEditorButton;
        private Button _playButton;

        public StartMenuManager(Game1 game) : base(game)
        {
            Height = 600;
            Width = 800;
        }

        public override void Initialize()
        {
            _levelEditorButton = new Button()
            {
                Height = 76,
                Width = 155,
                X = 322,
                Y = 300
            };

            _playButton = new Button()
            {
                Height = 76,
                Width = 155,
                X = 322,
                Y = 200
            };

            _levelEditorButton.Click += LevelEditorButton_Click;
            _playButton.Click += PlayButton_Click;

            ButtonState[] buttonStates = new ButtonState[] { ButtonState.Normal, ButtonState.Hover, ButtonState.Pressed, ButtonState.Disabled };
            int paddingLeft = 10;

            for (int i = 0; i < buttonStates.Length; ++i)
            {
                _levelEditorButton.SourceRectangles.Add(buttonStates[i], new Rectangle((311 * i) + (paddingLeft * i), 0, 311, 153));
                _playButton.SourceRectangles.Add(buttonStates[i], new Rectangle((311 * i) + (paddingLeft * i), 0, 311, 153));
            }

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Game.IsActive)
            {
                _levelEditorButton.Update();
                _playButton.Update();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_backgroundImage, Vector2.Zero, Color.White);
            _levelEditorButton.Draw(_spriteBatch);
            _playButton.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _backgroundImage = Game.Content.Load<Texture2D>("StartMenu\\BackgroundImage");
            _levelEditorButton.Texture = Game.Content.Load<Texture2D>("StartMenu\\LevelEditorButton");
            _playButton.Texture = Game.Content.Load<Texture2D>("StartMenu\\PlayButton");
            base.LoadContent();
        }

        private void LevelEditorButton_Click(object sender, EventArgs e)
        {
            Game.GameState = GameState.LevelEditor;
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            Game.GameState = GameState.Gameplay;
        }
    }
}
