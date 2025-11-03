using System;
using System.Drawing;
using Laboratory.Characters;

namespace Laboratory.Renderer
{
    public class EntityRenderer : IEntityRenderer
    {
        public void Draw(GameEntity entity)
        {
            Point center = entity.Position;
            string[] sprite = entity.Type.Sprite;

            int spriteHeight = sprite.Length;
            int spriteWidth = 0;
            foreach (var line in sprite)
                if (line.Length > spriteWidth)
                    spriteWidth = line.Length;

            int topLeftX = center.X - spriteWidth / 2;
            int topLeftY = center.Y - spriteHeight / 2;

            // Bounds checking
            if (topLeftX < 0 || topLeftY < 0 || 
                topLeftX + spriteWidth > Console.BufferWidth || 
                topLeftY + spriteHeight > Console.BufferHeight)
            {
                return; // Skip drawing if out of bounds
            }

            for (int i = 0; i < spriteHeight; i++)
            {
                Console.SetCursorPosition(topLeftX, topLeftY + i);
                string line = sprite[i];
                int padding = (spriteWidth - line.Length) / 2;
                Console.Write(new string(' ', padding) + line + new string(' ', spriteWidth - line.Length - padding));
            }

        }

        public void Erase(GameEntity entity)
        {
            Point center = entity.PreviousPosition;
            string[] sprite = entity.Type.Sprite;

            int spriteHeight = sprite.Length;
            int spriteWidth = 0;
            foreach (var line in sprite)
                if (line.Length > spriteWidth)
                    spriteWidth = line.Length;

            // Expand the area to clear by adding a border (e.g., 2 extra characters in each direction)
            int extraBorder = 2;
            int clearWidth = spriteWidth + 2 * extraBorder;
            int clearHeight = spriteHeight + 2 * extraBorder;

            int topLeftX = center.X - spriteWidth / 2 - extraBorder;
            int topLeftY = center.Y - spriteHeight / 2 - extraBorder;

            // Bounds checking to ensure we don't write outside the console buffer
            if (topLeftX < 0) 
            {
                clearWidth += topLeftX; // Adjust width if shifted
                topLeftX = 0;
            }
            if (topLeftY < 0)
            {
                clearHeight += topLeftY; // Adjust height if shifted
                topLeftY = 0;
            }
            if (topLeftX + clearWidth > Console.BufferWidth)
                clearWidth = Console.BufferWidth - topLeftX;
            if (topLeftY + clearHeight > Console.BufferHeight)
                clearHeight = Console.BufferHeight - topLeftY;

            // Skip erasing if the area is entirely out of bounds
            if (clearWidth <= 0 || clearHeight <= 0)
                return;

            // Clear the larger rectangular area with spaces
            for (int i = 0; i < clearHeight; i++)
            {
                if (topLeftY + i >= 0 && topLeftY + i < Console.BufferHeight)
                {
                    Console.SetCursorPosition(topLeftX, topLeftY + i);
                    Console.Write(new string(' ', clearWidth));
                }
            }
        }
        

    }
}