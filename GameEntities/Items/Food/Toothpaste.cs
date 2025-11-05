using System;
using Laboratory.Characters;

namespace Laboratory.GameEntities.Items.Food
{
    public class Toothpaste : IFood, IRenderableItem
    {
        public string Name => "Toothpaste";
        public string[] Sprite => new[] { "🪥" };
        public int Nutrition => 1;
        
    }
}
