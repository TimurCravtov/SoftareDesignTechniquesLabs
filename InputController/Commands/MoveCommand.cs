using System;
using System.Drawing;
using Laboratory.Characters;
using Laboratory.Renderer;
using Laboratory.Game.Effects;

namespace Laboratory.InputController.Commands
{
    /// <summary>
    /// Base class for movement commands that encapsulate direction and logic.
    /// </summary>
    public abstract class MoveCommand : ICommand
    {
        protected readonly Player Player;
        protected readonly EntityRenderer Renderer;
        private Point _previousPosition;

        protected MoveCommand(Player player, EntityRenderer renderer)
        {
            Player = player;
            Renderer = renderer;
        }

        public void Execute()
        {
            _previousPosition = Player.Position;
            int steps = 1 + StatusEffectManager.Instance.GetExtraSteps(Player);
            
            for (int i = 0; i < steps; i++)
            {
                Player.Position = new Point(Player.Position.X + GetDeltaX(), Player.Position.Y + GetDeltaY());
                Renderer.Erase(Player);
            }
        }

        public void Undo()
        {
            Player.Position = _previousPosition;
            Renderer.Erase(Player);
        }

        protected abstract int GetDeltaX();
        protected abstract int GetDeltaY();

        public abstract string GetDescription();
    }

    /// <summary>
    /// Command to move the player up.
    /// </summary>
    public class MoveUpCommand : MoveCommand
    {
        public MoveUpCommand(Player player, EntityRenderer renderer) : base(player, renderer) { }

        protected override int GetDeltaX() => 0;
        protected override int GetDeltaY() => -1;
        public override string GetDescription() => "Move Up";
    }

    /// <summary>
    /// Command to move the player down.
    /// </summary>
    public class MoveDownCommand : MoveCommand
    {
        public MoveDownCommand(Player player, EntityRenderer renderer) : base(player, renderer) { }

        protected override int GetDeltaX() => 0;
        protected override int GetDeltaY() => 1;
        public override string GetDescription() => "Move Down";
    }

    /// <summary>
    /// Command to move the player left.
    /// </summary>
    public class MoveLeftCommand : MoveCommand
    {
        public MoveLeftCommand(Player player, EntityRenderer renderer) : base(player, renderer) { }

        protected override int GetDeltaX() => -1;
        protected override int GetDeltaY() => 0;
        public override string GetDescription() => "Move Left";
    }

    /// <summary>
    /// Command to move the player right.
    /// </summary>
    public class MoveRightCommand : MoveCommand
    {
        public MoveRightCommand(Player player, EntityRenderer renderer) : base(player, renderer) { }

        protected override int GetDeltaX() => 1;
        protected override int GetDeltaY() => 0;
        public override string GetDescription() => "Move Right";
    }
}
