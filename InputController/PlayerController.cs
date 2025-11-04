using System;
using System.Drawing;
using System.Collections.Generic;
using Laboratory.Audio;
using Laboratory.Characters;
using Laboratory.GameEntities;
using Laboratory.GameEntities.Ammo;
using Laboratory.InputController;
using Laboratory.Game.Effects;

namespace Laboratory.InputController
{
    public class PlayerController
    {
        private readonly Player _player;
        private readonly List<GameEntity> _entities;
        private readonly IBulletPoolManager _bulletPool;
        private readonly Laboratory.Renderer.EntityRenderer _renderer;

        public PlayerController(Player player, List<GameEntity> entities, IBulletPoolManager bulletPool, Laboratory.Renderer.EntityRenderer renderer)
        {
            _player = player;
            _entities = entities;
            _bulletPool = bulletPool;
            _renderer = renderer;
        }

        public void HandleInput(ConsoleKey key)
        {
            // Movement: WASD
            PlayerAction action = key switch
            {
                ConsoleKey.W => PlayerAction.MoveUp,
                ConsoleKey.S => PlayerAction.MoveDown,
                ConsoleKey.A => PlayerAction.MoveLeft,
                ConsoleKey.D => PlayerAction.MoveRight,
                _ => PlayerAction.None
            };

            if (action != PlayerAction.None)
            {
                ExecuteAction(action);
                return;
            }

            Direction8? dir = key switch
            {
                ConsoleKey.UpArrow    => Direction8.Up,
                ConsoleKey.DownArrow  => Direction8.Down,
                ConsoleKey.LeftArrow  => Direction8.Left,
                ConsoleKey.RightArrow => Direction8.Right,
                _                     => null
            };

            if (dir != null)
            {
                Shoot(dir.Value);
                AudioManager.Instance.PlayLazerShooting();
            }
        }

        private void ExecuteAction(PlayerAction action)
        {
            int steps = 1 + StatusEffectManager.Instance.GetExtraSteps(_player);
            for (int i = 0; i < steps; i++)
            {
                switch (action)
                {
                    case PlayerAction.MoveUp:
                        _player.Position = new Point(_player.Position.X, _player.Position.Y - 1);
                        break;
                    case PlayerAction.MoveDown:
                        _player.Position = new Point(_player.Position.X, _player.Position.Y + 1);
                        break;
                    case PlayerAction.MoveLeft:
                        _player.Position = new Point(_player.Position.X - 1, _player.Position.Y);
                        break;
                    case PlayerAction.MoveRight:
                        _player.Position = new Point(_player.Position.X + 1, _player.Position.Y);
                        break;
                }
                // Erase the tile we just left to avoid trails during speed boost multi-steps
                _renderer.Erase(_player);
            }
        }

        private void Shoot(Direction8 dir)
        {
            var bullet = _bulletPool.GetBullet();
            if (bullet == null) return;

            // Launch bullet from player's position
            var start = new Point(_player.Position.X, _player.Position.Y);
            bullet.Launch(start, dir);

            // Add to entities so it gets updated and rendered
            _entities.Add(bullet);
        }
    }
}
