namespace PlatformGame.Gameplay.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using PlatformGame.Gameplay.Components.Templates;
    using PlatformGame.Gameplay.Objects.Templates;

    [Serializable]
    internal class Player : MovableGameObject
    {
        public Player(IAnimationComponent animationComponent, params IComponent[] components) : base(animationComponent, components)
        {
        }
    }
}
