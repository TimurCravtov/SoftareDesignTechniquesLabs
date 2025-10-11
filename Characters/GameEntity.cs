using System.Drawing;
using Laboratory.Reports;

namespace Laboratory.Characters;

public abstract class GameEntity
{
    public CharacterType Type { get; }
    public Point Position { get; set; } 

    protected GameEntity(CharacterType type, Point position)
    {
        Type = type;
        Position = position;
    }

    public abstract void Update();
}


