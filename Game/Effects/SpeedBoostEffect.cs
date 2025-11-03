namespace Laboratory.Game.Effects;

public class SpeedBoostEffect : ISpeedEffect
{
    public int RemainingTicks { get; private set; }
    public bool IsActive => RemainingTicks > 0;
    public string? OverlayIcon => null; // no icon by default
    public int ExtraSteps { get; }

    public SpeedBoostEffect(int durationTicks, int extraSteps = 1)
    {
        RemainingTicks = durationTicks;
        ExtraSteps = extraSteps;
    }

    public void Tick()
    {
        if (RemainingTicks > 0) RemainingTicks--;
    }
}


