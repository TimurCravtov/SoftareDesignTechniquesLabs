namespace Laboratory.Game;

using System.Collections.Generic;
using Laboratory.Characters;

public class LevelDirector
{
    // Build a level using a provided builder and optional predefined enemies; returns the created entities
    public List<GameEntity> BuildLevel(ILevelBuilder builder, IEnumerable<Laboratory.Characters.Enemies.IEnemy>? enemies = null)
    {
        
        return builder.GetResult();
    }
}

