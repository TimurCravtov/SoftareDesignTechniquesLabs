using System.Drawing;
using Laboratory.Game;

namespace Laboratory.Renderer;

public class MapRenderer
{
    private GameMap map;
    private char[,] previousFrame;

    public MapRenderer(GameMap map)
    {
        this.map = map;
        previousFrame = new char[map.Height, map.Width];
    }

    public void Draw()
    {
        var current = map.GetMap();

        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                if (current[y, x] != previousFrame[y, x])
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(current[y, x]);
                    previousFrame[y, x] = current[y, x];
                }
            }
        }
    }
}

