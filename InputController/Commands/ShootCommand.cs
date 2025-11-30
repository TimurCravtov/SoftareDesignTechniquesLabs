using System;
using System.Collections.Generic;
using System.Drawing;
using Laboratory.Audio;
using Laboratory.Characters;
using Laboratory.GameEntities;
using Laboratory.GameEntities.Ammo;

namespace Laboratory.InputController.Commands
{
    /// <summary>
    /// Command to shoot a projectile in a given direction.
    /// </summary>
    public class ShootCommand : ICommand
    {
        private readonly Player _player;
        private readonly List<GameEntity> _entities;
        private readonly IBulletPoolManager _bulletPool;
        private readonly Direction8 _direction;
        private Bullet? _firedBullet;

        public ShootCommand(Player player, List<GameEntity> entities, IBulletPoolManager bulletPool, Direction8 direction)
        {
            _player = player;
            _entities = entities;
            _bulletPool = bulletPool;
            _direction = direction;
        }

        public void Execute()
        {
            _firedBullet = _bulletPool.GetBullet();
            if (_firedBullet == null) return;

            var start = new Point(_player.Position.X, _player.Position.Y);
            _firedBullet.Launch(start, _direction);
            _entities.Add(_firedBullet);
            
            AudioManager.Instance.PlayLazerShooting();
        }

        public void Undo()
        {
            if (_firedBullet != null)
            {
                _entities.Remove(_firedBullet);
                _bulletPool.ReturnBullet(_firedBullet);
            }
        }

        public string GetDescription() => $"Shoot {_direction}";
    }
}
