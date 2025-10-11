using System;
using System.Drawing;
using Laboratory.Reports;

namespace Laboratory.Characters
{
    public class Player : GameEntity
    {
        private readonly int _width;
        private readonly int _height;

        public Player(CharacterType type, Point position)
            : base(type, position)
        {
            _width = 5;  // width of sprite
            _height = type.Sprite.Length;
        }

        public override void Update()
        {
          
        }

        public (Point position, char symbol) GetArrow(char direction)
        {
            int arrowX = Position.X;
            int arrowY = Position.Y;
            char arrow = ' ';

            switch (direction)
            {
                case 'H': arrow = '<'; arrowX--; break;
                case 'J': arrow = 'v'; arrowY += _height; break;
                case 'K': arrow = '^'; arrowY--; break;
                case 'U': arrow = '>'; arrowX += _width; break;
            }

            return (new Point(arrowX, arrowY), arrow);
        }
    }
}