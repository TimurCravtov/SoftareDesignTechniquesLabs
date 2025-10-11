using System;
using System.Drawing;
using Laboratory.Reports;

namespace Laboratory.Characters
{
    public class RobotEntity : GameEntity
    {
        private int _moveCounter = 0;
        private readonly int _moveDelay;
        private readonly GameEntity _target;
        private readonly Random _rnd = new Random();

        public RobotEntity(CharacterType type, Point position, GameEntity target = null, int moveDelay = 4)
            : base(type, position)
        {
            _target = target;
            _moveDelay = moveDelay;
        }

        // Polymorphic update method for movement logic
        public override void Update()
        {
            _moveCounter++;
            if (_moveCounter < _moveDelay) return;
            _moveCounter = 0;

            if (_target != null)
            {
                // Move towards target
                int newX = Position.X;
                int newY = Position.Y;

                if (Position.X < _target.Position.X) newX++;
                else if (Position.X > _target.Position.X) newX--;

                if (Position.Y < _target.Position.Y) newY++;
                else if (Position.Y > _target.Position.Y) newY--;

                Position = new Point(newX, newY);
            }
            else
            {
                // Random movement if no target
                Position = new Point(
                    Position.X + _rnd.Next(-1, 2),
                    Position.Y + _rnd.Next(-1, 2)
                );
            }
        }
    }
}