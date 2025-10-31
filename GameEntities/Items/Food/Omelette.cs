using System;
using Laboratory.Characters;

namespace Laboratory.GameEntities.Items.Food
{
    public class Omelette : IFood, IRenderableItem
    {
        public string Name => "Omelette";
        public string[] Sprite => new[] { "🍳" };
        public int Nutrition => 3;

        public void Use(GameEntity user)
        {
            var prop = user.GetType().GetProperty("Health");
            if (prop != null && prop.CanWrite)
            {
                var current = (int)prop.GetValue(user)!;
                prop.SetValue(user, current + Nutrition);
            }
        }
    }
}
