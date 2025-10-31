using System;

namespace Laboratory.Renderer
{
    public class ConsoleMenuRenderer : IMenuRenderer
    {
        public void Render(MenuToRender menu)
        {
            if (menu == null) return;

            int filled = menu.CurrentHearts;
            int max = Math.Max(1, menu.MaxHearts);
            int heartWidth = 2; // width per heart in chars (e.g. "♥ ")
            int totalWidth = max * heartWidth;

            int startY = menu.FixedY ?? 0;
            int startX;
            if (menu.FixedX.HasValue)
                startX = menu.FixedX.Value;
            else
            {
                try
                {
                    startX = Math.Max(0, Console.BufferWidth - totalWidth - 10);
                }
                catch
                {
                    startX = 0;
                }
            }

            try
            {
                // Clear previous hearts area first to avoid duplicate glyphs left behind
                Console.SetCursorPosition(startX, startY);
                Console.Write(new string(' ', Math.Max(0, totalWidth)));

                // Move back and draw hearts
                Console.SetCursorPosition(startX, startY);
            }
            catch
            {
            }

            // Draw max hearts: filled for current HP, empty for the rest
            for (int i = 0; i < max; i++)
            {
                if (i < filled)
                {
                    menu.HeartPrototype.DisplayFilled();
                }
                else
                {
                    menu.HeartPrototype.DisplayEmpty();
                }
            }
        }
    }
}
