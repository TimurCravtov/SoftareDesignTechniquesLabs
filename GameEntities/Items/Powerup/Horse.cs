using System;
using Laboratory.Characters;

namespace Laboratory.GameEntities.Items.Powerup
{
    public class Horse : IPowerup, IRenderableItem
    {
        public string Name => "Horse";
        public string[] Sprite => new[] { "🐎" };
        public int Duration => 30; 
        
        public void Use(GameEntity user)
        {
            // speed++
            // no direct console output; effects should be reflected in game state and drawn by renderer
        }
    }
}