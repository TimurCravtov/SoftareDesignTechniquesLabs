using System;
using Laboratory.Characters;

namespace Laboratory.GameEntities.Items.Powerup
{
    public class ElectricGuitar : IPowerup, IRenderableItem
    {
        public string Name => "Electric Guitar";
        public string[] Sprite => new[] { "🎸" };
        public int Duration => 20; // ticks

        public void Use(GameEntity user)
        {
            // gives a shield (no direct console output; rendering should be handled by renderers)
        }
    }
}
