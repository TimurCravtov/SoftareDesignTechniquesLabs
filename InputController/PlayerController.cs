using System;
using System.Drawing;
using Laboratory.Characters;
using Laboratory.InputController;

namespace Laboratory.InputController
{
    public class PlayerController
    {
        private readonly Player _player;

        public PlayerController(Player player)
        {
            _player = player;
        }

        public void HandleInput(ConsoleKey key)
        {
            PlayerAction action = key switch
            {
                ConsoleKey.UpArrow => PlayerAction.MoveUp,
                ConsoleKey.DownArrow => PlayerAction.MoveDown,
                ConsoleKey.LeftArrow => PlayerAction.MoveLeft,
                ConsoleKey.RightArrow => PlayerAction.MoveRight,
                _ => PlayerAction.None
            };

            ExecuteAction(action);
        }

        private void ExecuteAction(PlayerAction action)
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
        }
    }
}
