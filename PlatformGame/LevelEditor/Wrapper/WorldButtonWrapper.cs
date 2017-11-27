namespace PlatformGame.LevelEditor.Wrapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using PlatformGame.Shared.GuiToolkit;
    using PlatformGame.Shared.GuiToolkit.Templates;

    internal class WorldButtonWrapper : Button, IItem
    {
        private World _world;

        public WorldButtonWrapper(World world)
        {
            _world = world;
        }

        public World World => _world;

        public override void Update()
        {
            if (!World.Enabled)
            {
                base.Update();
            }
        }

        protected override void OnClick(EventArgs e)
        {
            World.Enabled = true;
            base.OnClick(e);
        }
    }
}
