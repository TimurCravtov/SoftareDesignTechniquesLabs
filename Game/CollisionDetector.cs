using System;
using System.Collections.Generic;
using System.Linq;
using Laboratory.Characters;
using Laboratory.GameEntities.Items;

namespace Laboratory.Game;

public class CollisionDetector
{
    public void Tick()
    {
        var entities = EntityManager.Instance.Entities;
        var player = GameState.Instance.Player;

        IReadOnlyList<GameEntity> overlaps = Array.Empty<GameEntity>();

        if (player != null)
        {
            overlaps = GetOverlapsFor(player);

            for (int i = 0; i < overlaps.Count; i++)
            {
                if (overlaps[i] is Laboratory.GameEntities.Items.ItemEntity item)
                {
                    if (item.Item is Laboratory.GameEntities.Items.Powerup.IPowerup p)
                    {
                        p.Use(player);
                        EntityManager.Instance.Remove(item);
                    }
                    else if (item.Item is IFood food)
                    {
                        player.Heal(food.Nutrition);
                        EntityManager.Instance.Remove(item);
                    }
                }
            }
        }

        // Single-line HUD at the bottom
        Console.SetCursorPosition(0, Console.BufferHeight - 1);
        var overlapList = overlaps.Count > 0 ? string.Join(", ", overlaps.Select(e => e.Type.Name)) : "none";
        Console.Write($"Entities: {entities.Count} | Overlaps: {overlaps.Count} [{overlapList}]           ");
    }

    public IReadOnlyList<GameEntity> GetOverlapsFor(GameEntity subject)
    {
        var entities = EntityManager.Instance.Entities;
        var result = new List<GameEntity>();
        for (int i = 0; i < entities.Count; i++)
        {
            var e = entities[i];
            if (!ReferenceEquals(e, subject) && e.Position.Equals(subject.Position))
            {
                result.Add(e);
            }
        }
        return result;
    }
}

