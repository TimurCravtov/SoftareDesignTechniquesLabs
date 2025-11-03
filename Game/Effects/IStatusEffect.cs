using System;

namespace Laboratory.Game.Effects;

public interface IStatusEffect
{
    int RemainingTicks { get; }
    bool IsActive { get; }
    void Tick();
    // Optional overlay to render near the entity (null to skip)
    string? OverlayIcon { get; }
}

public interface ISpeedEffect : IStatusEffect
{
    // Additional steps per move (e.g., 1 => double speed)
    int ExtraSteps { get; }
}


