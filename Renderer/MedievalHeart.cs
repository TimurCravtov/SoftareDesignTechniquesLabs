using System;

namespace Laboratory.Renderer
{
    public class MedievalHeart : IHeart
    {
        public void Display()
        {
            Console.Write("♥ ");
        }
        
        public void DisplayFilled()
        {
            // Medieval-style filled heart
            Console.Write("👑 ");
        }
        
        public void DisplayEmpty()
        {
            // Empty heart represented as a black box
            Console.Write("⬛ ");
        }
    }
}
