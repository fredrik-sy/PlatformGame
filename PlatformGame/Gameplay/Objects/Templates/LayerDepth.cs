namespace PlatformGame.Gameplay.Objects.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal enum LayerDepth
    {
        Background = 1,
        Foreground = 3,
        Interactive = 2,
        None = 0
    }

    internal static class Extension
    {
        public static float ToFloat(this LayerDepth layerDepth)
        {
            return (int)layerDepth * 0.1F;
        }
    }
}
