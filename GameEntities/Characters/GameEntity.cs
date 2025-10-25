using System.Drawing;
using Laboratory.Game;
using Laboratory.Reports;

namespace Laboratory.Characters;

public abstract class GameEntity: IPerTickActioner
{
    public CharacterType Type { get; }
    
    private Point _position;
    public Point Position
    {
        get => _position;
        set
        {
            PreviousPosition = _position;
            _position = value; 
        }
    }
    
    public Point PreviousPosition { get; private set; } 

    protected GameEntity(CharacterType type, Point position)
    {
        Type = type;
        Position = position;
    }
    
    public abstract void Update();
}


