using System;
using System.Drawing;
using Laboratory.Game;
using Laboratory.Game.Effects;

namespace Laboratory.Renderer
{
    public class StatusOverlayRenderer
    {
        private Point _lastShieldPos = new Point(-1, -1);
        private int _lastShieldLen = 0;

        public void DrawOverlays()
        {
            var player = GameState.Instance.Player;
            if (player == null) return;

            var icons = StatusEffectManager.Instance.GetOverlayIcons(player);

            if (icons.Count == 0)
            {
                if (_lastShieldPos.X >= 0 && _lastShieldPos.Y >= 0 && _lastShieldPos.Y < Console.BufferHeight)
                {
                    Console.SetCursorPosition(Math.Max(0, _lastShieldPos.X), _lastShieldPos.Y);
                    Console.Write(new string(' ', _lastShieldLen));
                }
                _lastShieldPos = new Point(-1, -1);
                _lastShieldLen = 0;
                return;
            }

            string line = string.Join(" ", icons);
            int shieldY = player.Position.Y - (player.Type.Sprite.Length / 2) - 1;
            int shieldX = player.Position.X - line.Length / 2;

            // Clear previous overlay if needed
            if (_lastShieldPos.X >= 0 && (_lastShieldPos.X != shieldX || _lastShieldPos.Y != shieldY))
            {
                if (_lastShieldPos.Y >= 0 && _lastShieldPos.Y < Console.BufferHeight)
                {
                    Console.SetCursorPosition(Math.Max(0, _lastShieldPos.X), _lastShieldPos.Y);
                    Console.Write(new string(' ', _lastShieldLen));
                }
                _lastShieldPos = new Point(-1, -1);
                _lastShieldLen = 0;
            }

            if (shieldY < 0 || shieldY >= Console.BufferHeight) return;
            if (shieldX < 0 || shieldX + line.Length > Console.BufferWidth) return;

            Console.SetCursorPosition(shieldX, shieldY);
            Console.Write(line);

            _lastShieldPos = new Point(shieldX, shieldY);
            _lastShieldLen = line.Length;
        }
    }
}


