namespace PlatformGame
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using PlatformGame.Gameplay;
    using PlatformGame.LevelEditor;
    using PlatformGame.Shared;
    using PlatformGame.Shared.IO;
    using PlatformGame.StartMenu;

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private ListDictionary _managers;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _managers = new ListDictionary();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public static Random Random { get; } = new Random();

        public GameState GameState
        {
            set
            {
                foreach (DictionaryEntry pair in _managers)
                {
                    if ((GameState)pair.Key == value)
                    {
                        ((Manager)pair.Value).Active = true;
                    }
                    else
                    {
                        ((Manager)pair.Value).Active = false;
                    }
                }
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Services.AddService(typeof(GraphicsDeviceManager), _graphics);

            GameplayManager gameplayManager = new GameplayManager(this);
            LevelEditorManager levelEditorManager = new LevelEditorManager(this);
            StartMenuManager startMenuManager = new StartMenuManager(this);

            Components.Add(gameplayManager);
            Components.Add(levelEditorManager);
            Components.Add(startMenuManager);

            _managers.Add(GameState.Gameplay, gameplayManager);
            _managers.Add(GameState.LevelEditor, levelEditorManager);
            _managers.Add(GameState.StartMenu, startMenuManager);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            GameState = GameState.StartMenu;
            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (IsActive)
            {
                KeyboardReader.Update();
                MouseReader.Update();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);
            base.Draw(gameTime);
        }
    }
}
