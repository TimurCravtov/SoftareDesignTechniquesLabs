using System;
using System.Collections.Generic;
using lab4.Utils;

namespace lab4.Patterns.Command
{
    // Receiver
    public class VideoPlayer
    {
        private string _currentVideo;
        private bool _isPlaying;
        private int _currentTime;

        public void SetVideo(string video)
        {
            _currentVideo = video;
            _currentTime = 0;
            _isPlaying = false;
            Logger.LogInfo($"Video loaded: {_currentVideo}");
        }

        public void Play()
        {
            if (!_isPlaying)
            {
                _isPlaying = true;
                Logger.LogSuccess($"Playing {_currentVideo} at {_currentTime}s");
            }
            else
            {
                Logger.LogWarning("Video is already playing.");
            }
        }

        public void Pause()
        {
            if (_isPlaying)
            {
                _isPlaying = false;
                Logger.LogInfo($"Video paused at {_currentTime}s");
            }
            else
            {
                Logger.LogWarning("Video is already paused.");
            }
        }

        public void Rewind()
        {
            _currentTime = Math.Max(0, _currentTime - 10);
            Logger.LogInfo($"Rewound to {_currentTime}s");
        }

        public void FastForward()
        {
            _currentTime += 10;
            Logger.LogInfo($"Fast forwarded to {_currentTime}s");
        }
    }

    // Command Interface
    public interface IVideoCommand
    {
        void Execute();
    }

    // Concrete Commands
    public class PlayCommand : IVideoCommand
    {
        private readonly VideoPlayer _player;

        public PlayCommand(VideoPlayer player)
        {
            _player = player;
        }

        public void Execute()
        {
            _player.Play();
        }
    }

    public class PauseCommand : IVideoCommand
    {
        private readonly VideoPlayer _player;

        public PauseCommand(VideoPlayer player)
        {
            _player = player;
        }

        public void Execute()
        {
            _player.Pause();
        }
    }

    public class RewindCommand : IVideoCommand
    {
        private readonly VideoPlayer _player;

        public RewindCommand(VideoPlayer player)
        {
            _player = player;
        }

        public void Execute()
        {
            _player.Rewind();
        }
    }

    public class FastForwardCommand : IVideoCommand
    {
        private readonly VideoPlayer _player;

        public FastForwardCommand(VideoPlayer player)
        {
            _player = player;
        }

        public void Execute()
        {
            _player.FastForward();
        }
    }

    // Invoker
    public class RemoteControl
    {
        private readonly Dictionary<string, IVideoCommand> _commands = new Dictionary<string, IVideoCommand>();

        public void SetCommand(string button, IVideoCommand command)
        {
            _commands[button] = command;
        }

        public void PressButton(string button)
        {
            if (_commands.ContainsKey(button))
            {
                Logger.LogSystem($"Remote: Pressing {button}...");
                _commands[button].Execute();
            }
            else
            {
                Logger.LogError($"Remote: Button {button} not configured.");
            }
        }
    }
}
