namespace PlatformGame.Shared.GuiToolkit.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal interface IItem
    {
        Rectangle Bounds { get; }

        int Height { get; set; }

        int Width { get; set; }

        int X { get; set; }

        int Y { get; set; }

        void Draw(SpriteBatch spriteBatch);

        void Update();
    }
}
