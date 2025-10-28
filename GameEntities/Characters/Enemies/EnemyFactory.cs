using System.Collections.Generic;
using System.Drawing;
using Laboratory.Characters;
using Laboratory.Reports;

namespace Laboratory.Characters.Enemies
{
    /// <summary>
    /// Centralized factory for creating enemy instances. Keeps creation logic
    /// in one place and simplifies `Program.cs`.
    /// </summary>
    public static class EnemyFactory
    {
        public static GameEntity CreateMrBob(CharacterType type, Point position, GameEntity? target = null, int moveDelay = 4)
        {
            return new MrBob(type, position, target, moveDelay);
        }

        public static GameEntity CreateUngaBunga(CharacterType type, Point position, List<GameEntity> entities, int moveDelay = 4, int minShootDelay = 6, int maxShootDelay = 20)
        {
            return new UngaBunga(type, position, entities, moveDelay, minShootDelay, maxShootDelay);
        }
    }
}
