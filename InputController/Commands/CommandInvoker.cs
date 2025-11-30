using System;
using System.Collections.Generic;

namespace Laboratory.InputController.Commands
{
    /// <summary>
    /// Command invoker that encapsulates command execution and history management.
    /// </summary>
    public class CommandInvoker
    {
        private readonly Stack<ICommand> _history = new();
        private readonly int _maxHistorySize;

        public CommandInvoker(int maxHistorySize = 100)
        {
            _maxHistorySize = maxHistorySize;
        }

        /// <summary>
        /// Execute a command and add it to history.
        /// </summary>
        public void Execute(ICommand command)
        {
            command.Execute();
            _history.Push(command);

            // Trim history if it exceeds max size
            if (_history.Count > _maxHistorySize)
            {
                var temp = new Stack<ICommand>(_maxHistorySize);
                int count = 0;
                foreach (var cmd in _history)
                {
                    if (count < _maxHistorySize)
                    {
                        temp.Push(cmd);
                        count++;
                    }
                }
                _history.Clear();
                foreach (var cmd in temp) _history.Push(cmd);
            }
        }

        /// <summary>
        /// Undo the last executed command.
        /// </summary>
        public void Undo()
        {
            if (_history.Count == 0) return;
            var command = _history.Pop();
            command.Undo();
        }

        /// <summary>
        /// Get the number of commands in history.
        /// </summary>
        public int HistoryCount => _history.Count;

        /// <summary>
        /// Clear all command history.
        /// </summary>
        public void ClearHistory()
        {
            _history.Clear();
        }
    }
}
