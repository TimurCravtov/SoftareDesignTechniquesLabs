using System;
using System.Collections.Generic;
using System.Drawing;
using Laboratory.Audio;
using Laboratory.Characters;
using Laboratory.Game.Effects;
using Laboratory.InputController;
using Laboratory.Renderer;

namespace Laboratory.Game
{
    // Facade that encapsulates the subsystems the GameLoop needs to orchestrate.
    public class GameEngineFacade
    {
        private readonly List<GameEntity> _entities;
        private readonly EntityRenderer _renderer;
        private readonly PlayerController _playerController;
        private readonly CollisionDetector _collisionDetector;
        private readonly StatusOverlayRenderer _statusOverlay;
        private readonly Laboratory.Renderer.MenuToRender? _menu;
        private readonly Laboratory.Renderer.IMenuRenderer? _menuRenderer;
        private readonly IAudioManager _audioManager;

        public GameEngineFacade(List<GameEntity> entities,
            EntityRenderer renderer,
            PlayerController playerController,
            Laboratory.Renderer.MenuToRender? menu = null,
            Laboratory.Renderer.IMenuRenderer? menuRenderer = null,
            IAudioManager? audioManager = null)
        {
            _entities = entities ?? throw new ArgumentNullException(nameof(entities));
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            _playerController = playerController ?? throw new ArgumentNullException(nameof(playerController));
            _collisionDetector = new CollisionDetector();
            _statusOverlay = new StatusOverlayRenderer();
            _menu = menu;
            _menuRenderer = menuRenderer;
            _audioManager = audioManager ?? AudioManager.Instance;
        }

        public void Initialize(string? backgroundMusic = null)
        {
            if (!string.IsNullOrEmpty(backgroundMusic))
            {
                _audioManager.PlayBackgroundMusic(backgroundMusic);
            }
        }

        public void UpdateFrame()
        {
            HandleInput();
            UpdateEntities();
            _collisionDetector.Tick();
            StatusEffectManager.Instance.TickAll();
            RenderEntities();
            _statusOverlay.DrawOverlays();
        }

        private void HandleInput()
        {
            if (!Console.KeyAvailable) return;

            var key = Console.ReadKey(true).Key;
            _playerController.HandleInput(key);

            while (Console.KeyAvailable) Console.ReadKey(true);
        }

        private void UpdateEntities()
        {
            var snapshot = _entities.ToArray();
            foreach (var entity in snapshot)
                entity.Update();

            RemoveEntities();
        }

        private void RemoveEntities()
        {
            for (int i = _entities.Count - 1; i >= 0; i--)
            {
                if (_entities[i] is IRemovable r && r.ShouldBeRemoved)
                {
                    _renderer.Erase(_entities[i]);
                    _entities.RemoveAt(i);
                }
            }
        }

        private void RenderEntities()
        {
            foreach (var entity in _entities)
            {
                _renderer.Erase(entity);
            }

            foreach (var entity in _entities)
            {
                _renderer.Draw(entity);
            }

            if (_menu != null && _menuRenderer != null)
            {
                _menuRenderer.Render(_menu);
            }
        }
    }
}
