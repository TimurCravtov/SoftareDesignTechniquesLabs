using System.Collections.Generic;
using Laboratory.Characters;

namespace Laboratory.Game.Effects;

public sealed class StatusEffectManager
{
    private static readonly StatusEffectManager _instance = new();
    public static StatusEffectManager Instance => _instance;

    private readonly Dictionary<GameEntity, List<IStatusEffect>> _effects = new();

    private StatusEffectManager() { }

    public void AddEffect(GameEntity target, IStatusEffect effect)
    {
        if (!_effects.TryGetValue(target, out var list))
        {
            list = new List<IStatusEffect>();
            _effects[target] = list;
        }
        list.Add(effect);
    }

    public void TickAll()
    {
        foreach (var kv in _effects)
        {
            var list = kv.Value;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                list[i].Tick();
                if (!list[i].IsActive)
                {
                    list.RemoveAt(i);
                }
            }
        }
    }

    public int GetExtraSteps(GameEntity target)
    {
        int extra = 0;
        if (_effects.TryGetValue(target, out var list))
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] is ISpeedEffect se)
                    extra += se.ExtraSteps;
            }
        }
        return extra;
    }

    public List<string> GetOverlayIcons(GameEntity target)
    {
        var icons = new List<string>();
        if (_effects.TryGetValue(target, out var list))
        {
            for (int i = 0; i < list.Count; i++)
            {
                var icon = list[i].OverlayIcon;
                if (!string.IsNullOrEmpty(icon)) icons.Add(icon!);
            }
        }
        return icons;
    }
}


