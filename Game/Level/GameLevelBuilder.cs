using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Laboratory.Characters;
using Laboratory.Characters.Enemies;
using Laboratory.GameEntities.Items;
using Laboratory.GameEntities.Items.Factories;
using Laboratory.Reports;

namespace Laboratory.Game
{
    // Concrete builder implementing ILevelBuilder. It uses an InGameItemFactory
    // to create themed items and places enemies and items on the provided map.
    public class GameLevelBuilder : ILevelBuilder
    {
        private readonly List<GameEntity> _entities = new();
        private GameMap? _map;
        private Player? _player;
        private InGameItemFactory? _factory;
        private readonly Random _rand = new();

        public ILevelBuilder SetFactory(InGameItemFactory factory)
        {
            _factory = factory;
            return this;
        }

        public ILevelBuilder SetMap(GameMap map)
        {
            _map = map;
            return this;
        }

        public ILevelBuilder SetPlayer(Player player)
        {
            _player = player;
            // ensure player is the first entity
            _entities.Insert(0, player);
            return this;
        }

        public ILevelBuilder AddEnemy(Laboratory.Characters.Enemies.IEnemy enemy)
        {
            if (_map == null)
                throw new InvalidOperationException("Map must be set before placing enemies.");

            // If the provided enemy is a GameEntity, we can position it and add to the list
            if (enemy is GameEntity ge)
            {
                var pos = GetFreePosition();
                ge.Position = pos;
                _entities.Add(ge);
            }
            else
            {
                // If it's not a GameEntity (unlikely given current code), ignore or throw
                throw new InvalidOperationException("Enemy must be a GameEntity in this implementation.");
            }

            return this;
        }

        public ILevelBuilder PlaceItems(int foodCount, int powerupCount)
        {
            if (_map == null)
                throw new InvalidOperationException("Map must be set before placing items.");
            if (_factory == null)
                throw new InvalidOperationException("Factory must be provided before placing items.");

            // Place food items
            for (int i = 0; i < foodCount; i++)
            {
                var food = _factory.CreateFood();
                var renderable = food as IRenderableItem ?? new AnonymousRenderableItem(food.GetType().Name);
                var (sprite, name) = ExtractSpriteAndName(renderable);
                var type = new CharacterType(0, sprite, name);
                var pos = GetFreePosition();
                var itemEntity = new ItemEntity(type, pos, renderable);
                _entities.Add(itemEntity);
            }

            // Place powerups
            for (int i = 0; i < powerupCount; i++)
            {
                var powerup = _factory.CreatePowerup();
                var renderable = powerup as IRenderableItem ?? new AnonymousRenderableItem(powerup.GetType().Name);
                var (sprite, name) = ExtractSpriteAndName(renderable);
                var type = new CharacterType(0, sprite, name);
                var pos = GetFreePosition();
                var itemEntity = new ItemEntity(type, pos, renderable);
                _entities.Add(itemEntity);
            }

            return this;
        }

        public List<GameEntity> GetResult() => _entities;

        // Helper: pick a random free position inside the map bounds (not on border)
        private Point GetFreePosition()
        {
            if (_map == null)
                throw new InvalidOperationException("Map not set");

            int x, y;
            int attempts = 0;
            do
            {
                x = _rand.Next(1, _map.Width - 1);
                y = _rand.Next(1, _map.Height - 1);
                attempts++;
                if (attempts > 500)
                    break;
            } while (_entities.Any(e => e.Position.X == x && e.Position.Y == y));

            return new Point(x, y);
        }

        // Use the IRenderableItem abstraction when available to extract display data.
        private static (string[] sprite, string name) ExtractSpriteAndName(object item)
        {
            if (item is Laboratory.GameEntities.Items.IRenderableItem ri)
            {
                return (ri.Sprite, ri.Name);
            }

            // Fallback for unknown types: use type name and a placeholder sprite
            return (new[] { "?" }, item.GetType().Name);
        }

        // Simple wrapper for items that do not implement IRenderableItem.
        private class AnonymousRenderableItem : Laboratory.GameEntities.Items.IRenderableItem
        {
            public string Name { get; }
            public string[] Sprite { get; }

            public AnonymousRenderableItem(string name)
            {
                Name = name;
                Sprite = new[] { "?" };
            }
        }
    }
}
