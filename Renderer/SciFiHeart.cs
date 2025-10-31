using System;

namespace Laboratory.Renderer
{
    public class SciFiHeart : IHeart
    {
        public void DisplayFilled()
        {
            // Sci-fi style filled heart (battery glyph)
            Console.Write("🔋 ");
        }

        public void DisplayEmpty()
        {
            // Empty heart represented as a black box
            Console.Write("⬛ ");
        }
    }
}
