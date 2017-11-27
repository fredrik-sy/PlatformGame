namespace PlatformGame.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;

    internal abstract class Manager : DrawableGameComponent
    {
        public Manager(Game1 game) : base(game)
        {
            Game = game;
            Enabled = false;
            Visible = false;
        }

        public new Game1 Game { get; private set; }

        public int Height { get; protected set; }

        public int Width { get; protected set; }

        public virtual bool Active
        {
            set
            {
                if (value)
                {
                    if (Game.Services.GetService(typeof(GraphicsDeviceManager)) is GraphicsDeviceManager graphics)
                    {
                        graphics.PreferredBackBufferHeight = Height;
                        graphics.PreferredBackBufferWidth = Width;
                        graphics.ApplyChanges();
                    }
                }

                Enabled = value;
                Visible = value;
            }
        }
    }
}
