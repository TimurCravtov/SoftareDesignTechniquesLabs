using System;
using Laboratory.Characters;
using Laboratory.Game;
using Laboratory.Game.Effects;

namespace Laboratory.GameEntities.Items.Powerup
{
    public class ElectricGuitar : IPowerup, IRenderableItem
    {
        public string Name => "Electric Guitar";
        public string[] Sprite => new[] { "🎸" };
        public int Duration => 60; // ticks

        public void Use(GameEntity user)
        {
            // Grant shield effect via effect system
            StatusEffectManager.Instance.AddEffect(user, new ShieldEffect(Duration));
        }
    }
}
