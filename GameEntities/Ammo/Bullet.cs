using System.Drawing;
using Laboratory.Game;

namespace Laboratory.GameEntities.Ammo;

public class Bullet: IPerTickActioner
{
    public bool IsActive { get; private set; }
    public Point Position { get; private set; }
    public Direction8 Direction { get; private set; }
    
    public void Update()
    {
        
    }
    
    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
