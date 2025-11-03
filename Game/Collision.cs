using System.Collections.Generic;
using System.Drawing;
using Laboratory.Characters;

namespace Laboratory.Game;

public static class Collision
{
    public static IReadOnlyList<GameEntity> QueryOverlapsAt(Point position, GameEntity? exclude = null)
    {
        var entities = EntityManager.Instance.Entities;
        var result = new List<GameEntity>();
        for (int i = 0; i < entities.Count; i++)
        {
            var e = entities[i];
            if (!ReferenceEquals(e, exclude) && e.Position.Equals(position))
            {
                result.Add(e);
            }
        }
        return result;
    }
}


