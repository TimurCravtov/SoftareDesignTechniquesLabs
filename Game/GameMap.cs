namespace Laboratory.Game;

public class GameMap
{
    public int Width { get; }
    public int Height { get; }
    private char[,] map;

    public GameMap(int width, int height)
    {
        Width = width;
        Height = height;
        // Console.SetBufferSize(width, height);
        Initialize();
    }
    
    private void Initialize()
    {
        map = new char[Height, Width];
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                map[y, x] = (y == 0 || y == Height - 1 || x == 0 || x == Width - 1) ? '#' : ' ';
            }
        }
    }

    public char[,] GetMap() => map;
}
