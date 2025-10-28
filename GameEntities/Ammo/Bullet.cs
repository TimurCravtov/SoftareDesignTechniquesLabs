using System;
using System.Drawing;
using Laboratory.Game;
using Laboratory.Characters;
using Laboratory.Reports;

namespace Laboratory.GameEntities.Ammo;

public class Bullet : GameEntity, IRemovable
{
    public bool IsActive { get; private set; }
    public Direction8 Direction { get; private set; }

    private bool _shouldBeRemoved;
    public bool ShouldBeRemoved => _shouldBeRemoved;

    // Callback set by pool so bullet can return itself
    public Action<Bullet>? ReturnToPool { get; set; }

    public Bullet() : base(new CharacterType(1, new[] {"*"}, "Bullet"), new Point(-1, -1))
    {
        IsActive = false;
        _shouldBeRemoved = false;
    }

    public void Launch(Point start, Direction8 dir)
    {
        Position = start;
        Direction = dir;
        _shouldBeRemoved = false;
        Activate();
    }

    public override void Update()
    {
        if (!IsActive) return;

        var p = Position;
        var (dx, dy) = Direction switch
        {
            Direction8.Up => (0, -1),
            Direction8.Down => (0, 1),
            Direction8.Left => (-1, 0),
            Direction8.Right => (1, 0),
            Direction8.UpLeft => (-1, -1),
            Direction8.UpRight => (1, -1),
            Direction8.DownLeft => (-1, 1),
            Direction8.DownRight => (1, 1),
            _ => (0, 0)
        };

        Position = new Point(p.X + dx, p.Y + dy);

        // Bounds check — if outside console buffer, deactivate and return to pool
        if (Position.X < 0 || Position.Y < 0 || Position.X >= Console.BufferWidth || Position.Y >= Console.BufferHeight)
        {
            Deactivate();
        }
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
        _shouldBeRemoved = true;
        // return to pool if available
        ReturnToPool?.Invoke(this);
    }

    public void Remove()
    {
        Deactivate();
    }
}
