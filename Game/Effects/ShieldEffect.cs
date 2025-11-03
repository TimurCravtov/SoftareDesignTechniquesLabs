namespace Laboratory.Game.Effects;

public class ShieldEffect : IStatusEffect
{
    public int RemainingTicks { get; private set; }
    public bool IsActive => RemainingTicks > 0;
    public string? OverlayIcon => "🎧";

    public ShieldEffect(int durationTicks)
    {
        RemainingTicks = durationTicks;
    }

    public void Tick()
    {
        if (RemainingTicks > 0) RemainingTicks--;
    }
}


