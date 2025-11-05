using System;
using Laboratory.Characters;

namespace Laboratory.GameEntities.Items.Food
{
    public class Omelette : IFood, IRenderableItem
    {
        public string Name => "Omelette";
        public string[] Sprite => new[] { "🍳" };
        public int Nutrition => 2;
        
    }
}
