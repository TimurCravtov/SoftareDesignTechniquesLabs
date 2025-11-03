using System;
using System.Drawing;
using Laboratory.Reports;

namespace Laboratory.Characters
{
    public class Player : GameEntity
    {
        private readonly int _width;
        private readonly int _height;
        public int Health { get; set; }

        public Player(CharacterType type, Point position)
            : base(type, position)
        {
            _width = 5;  // width of sprite
            _height = type.Sprite.Length;
            Health = type.BaseHealth;
        }

        public void Heal(int amount)
        {
            Health += amount;
        }

        // no per-frame update for player
        public override void Update()
        {
          
        }
    }
}