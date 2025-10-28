using System;
using System.Drawing;
using System.Collections.Generic;
using Laboratory.Reports;
using Laboratory.Game;

namespace Laboratory.Characters.Enemies
{
    public class UngaBunga : GameEntity, IRemovable, IEnemy
    {
        private readonly Random _rnd = new();
        private int _moveCounter = 0;
        private readonly int _moveDelay;
    private int _shootCounter = 0;
    private readonly int _minShootDelay;
    private readonly int _maxShootDelay;
    private readonly List<GameEntity> _entities;

        private bool _shouldBeRemoved = false;

    private static readonly char[] _projectileSymbols = new[] {'@', '#', '$', '%', '&'};

        public UngaBunga(CharacterType type, Point position, List<GameEntity> entities, int moveDelay = 10, int minShootDelay = 6, int maxShootDelay = 20)
            : base(type, position)
        {
            _moveDelay = Math.Max(1, moveDelay);
            _minShootDelay = Math.Max(1, minShootDelay);
            _maxShootDelay = Math.Max(_minShootDelay, maxShootDelay);
            _entities = entities ?? throw new ArgumentNullException(nameof(entities));

            // randomize initial shoot counter so enemies don't all shoot in sync
            _shootCounter = _rnd.Next(_minShootDelay, _maxShootDelay + 1);
        }

        public bool ShouldBeRemoved => _shouldBeRemoved;

        public void Remove()
        {
            _shouldBeRemoved = true;
        }

        public override void Update()
        {
            // Movement
            _moveCounter++;
            if (_moveCounter % _moveDelay == 0)
            {
                Position = new Point(
                    Position.X + _rnd.Next(-1, 2),
                    Position.Y + _rnd.Next(-1, 2)
                );
            }

            // Shooting
            _shootCounter--;
            if (_shootCounter <= 0)
            {
                TryShoot();
                _shootCounter = _rnd.Next(_minShootDelay, _maxShootDelay + 1);
            }
        }

        private void TryShoot()
        {
            // choose a random symbol
            char symbol = _projectileSymbols[_rnd.Next(0, _projectileSymbols.Length)];

            // choose a random direction to shoot (dx,dy in -1..1 except 0,0)
            int dx, dy;
            do
            {
                dx = _rnd.Next(-1, 2);
                dy = _rnd.Next(-1, 2);
            } while (dx == 0 && dy == 0);

            // spawn projectile just adjacent to the enemy and add it to the main entities list
            var start = new Point(Position.X + dx, Position.Y + dy);
            var projectile = new Projectile(new string(new[] { symbol }), start, dx, dy);

            _entities.Add(projectile);
        }

        /// <summary>
        /// Simple projectile GameEntity that moves with a fixed (dx,dy) per tick
        /// and removes itself when out of console bounds.
        /// </summary>
        private class Projectile : GameEntity, IRemovable
        {
            private readonly int _dx;
            private readonly int _dy;
            private bool _shouldBeRemoved = false;

            public Projectile(string symbol, Point start, int dx, int dy)
                : base(new CharacterType(1, new[] { symbol }, "Projectile"), start)
            {
                _dx = dx;
                _dy = dy;
            }

            public bool ShouldBeRemoved => _shouldBeRemoved;

            public void Remove()
            {
                _shouldBeRemoved = true;
            }

            public override void Update()
            {
                Position = new Point(Position.X + _dx, Position.Y + _dy);

                // mark for removal if outside console buffer
                if (Position.X < 0 || Position.Y < 0 || Position.X >= Console.BufferWidth || Position.Y >= Console.BufferHeight)
                {
                    _shouldBeRemoved = true;
                }
            }
        }
    }
}
