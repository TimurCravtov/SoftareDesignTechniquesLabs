namespace Laboratory.InputController.Commands
{
    /// <summary>
    /// Command pattern interface for encapsulating player actions.
    /// Each command represents an atomic, executable player action.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Execute the command.
        /// </summary>
        void Execute();

        /// <summary>
        /// Undo the command (optional; not all commands support undo).
        /// </summary>
        void Undo();

        /// <summary>
        /// Get a human-readable description of the command.
        /// </summary>
        string GetDescription();
    }
}
