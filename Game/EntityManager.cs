using System.Collections.Generic;
using Laboratory.Characters;

namespace Laboratory.Game;

public sealed class EntityManager
{
    private static readonly EntityManager _instance = new();
    public static EntityManager Instance => _instance;

    private List<GameEntity> _entities = new();

    private EntityManager() { }

    public IReadOnlyList<GameEntity> Entities => _entities;

    public void Initialize(List<GameEntity> entities)
    {
        _entities = entities;
    }

    public void Add(GameEntity entity)
    {
        _entities.Add(entity);
    }

    public bool Remove(GameEntity entity)
    {
        return _entities.Remove(entity);
    }
}


