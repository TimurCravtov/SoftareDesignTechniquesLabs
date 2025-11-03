using System;
using Laboratory.Characters;
using Laboratory.Game;
using Laboratory.Game.Effects;

namespace Laboratory.GameEntities.Items.Powerup
{
    public class Horse : IPowerup, IRenderableItem
    {
        public string Name => "Horse";
        public string[] Sprite => new[] { "🐎" };
        public int Duration => 70; 
        
        public void Use(GameEntity user)
        {
            // Apply speed boost via effect system
            StatusEffectManager.Instance.AddEffect(user, new SpeedBoostEffect(Duration, extraSteps: 1));
        }
    }
}