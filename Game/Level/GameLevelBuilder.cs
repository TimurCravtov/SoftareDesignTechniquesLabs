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
            _entities.Insert(0, player);
            return this;
        }

        public ILevelBuilder AddEnemy(IEnemy enemy)
        {
            if (_map == null) throw new InvalidOperationException("Map must be set.");

            if (enemy is not GameEntity ge)
                throw new InvalidOperationException("Enemy must be GameEntity.");

            ge.Position = GetFreePosition();
            _entities.Add(ge);
            return this;
        }

        public ILevelBuilder AddFood(int foodCount)
        {
            EnsureFactoryAndMap();

            for (int i = 0; i < foodCount; i++)
            {
                var food = _factory!.CreateFood();
                var renderable = (IRenderableItem)food;
                var (sprite, name) = ExtractSpriteAndName(renderable);
                var itemEntity = new ItemEntity(new CharacterType(0, sprite, name), GetFreePosition(), renderable);
                _entities.Add(itemEntity);
            }

            return this;
        }

        public ILevelBuilder AddPowerup(int powerupCount)
        {
            EnsureFactoryAndMap();

            for (int i = 0; i < powerupCount; i++)
            {
                var powerup = _factory!.CreatePowerup();
                var renderable = (IRenderableItem)powerup;
                var (sprite, name) = ExtractSpriteAndName(renderable);
                var itemEntity = new ItemEntity(new CharacterType(0, sprite, name), GetFreePosition(), renderable);
                _entities.Add(itemEntity);
            }

            return this;
        }

        public ILevelBuilder PlaceItems(int foodCount, int powerupCount)
        {
            AddFood(foodCount);
            AddPowerup(powerupCount);
            return this;
        }

        public List<GameEntity> GetResult() => _entities;

        private Point GetFreePosition()
        {
            if (_map == null) throw new InvalidOperationException("Map not set");

            int x, y;
            int attempts = 0;
            do
            {
                x = _rand.Next(1, _map.Width - 1);
                y = _rand.Next(1, _map.Height - 1);
                attempts++;
                if (attempts > 500) break;
            } while (_entities.Any(e => e.Position.X == x && e.Position.Y == y));

            return new Point(x, y);
        }

        private static (string[] sprite, string name) ExtractSpriteAndName(IRenderableItem item)
            => (item.Sprite, item.Name);

        private void EnsureFactoryAndMap()
        {
            if (_factory == null) throw new InvalidOperationException("Factory must be provided before placing items.");
            if (_map == null) throw new InvalidOperationException("Map must be set before placing items.");
        }
    }
}
