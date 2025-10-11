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

            for (int i = 0; i < spriteHeight; i++)
            {
                Console.SetCursorPosition(topLeftX, topLeftY + i);
                string line = sprite[i];
                int padding = (spriteWidth - line.Length) / 2;
                Console.Write(new string(' ', padding) + line);
            }
        }

        public void Erase(GameEntity entity)
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

            for (int i = 0; i < spriteHeight; i++)
            {
                Console.SetCursorPosition(topLeftX, topLeftY + i);
                Console.Write(new string(' ', spriteWidth));
            }
        }
    }
}