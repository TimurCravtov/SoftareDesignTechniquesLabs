using System;
using Laboratory.Characters;

namespace Laboratory.GameEntities.Items.Food
{
    public class Toothpaste : IFood, IRenderableItem
    {
        public string Name => "Toothpaste";
        public string[] Sprite => new[] { "🪥" };
        public int Nutrition => 1;

        public void Use(GameEntity user)
        {
            // Simple example: restore 1 health if the target has a Health property.
            // We'll try to apply by reflection to keep the item non-invasive.
            var prop = user.GetType().GetProperty("Health");
            if (prop != null && prop.CanWrite)
            {
                var current = (int)prop.GetValue(user)!;
                prop.SetValue(user, current + Nutrition);
            }
        }
    }
}
