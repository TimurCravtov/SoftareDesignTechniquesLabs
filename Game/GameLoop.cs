using System.Drawing;
using Laboratory.InputController;

namespace Laboratory.Game;

using System;
using System.Collections.Generic;
using System.Threading;
using Laboratory.Characters;
using Laboratory.Renderer;
using Laboratory.Game.Effects;

public class GameLoop
{
    private readonly List<GameEntity> _entities;
    private readonly EntityRenderer _renderer;
    private readonly PlayerController _playerController;
    private readonly Laboratory.Renderer.MenuToRender? _menu;
    private readonly Laboratory.Renderer.IMenuRenderer? _menuRenderer;
    private readonly CollisionDetector _collisionDetector = new();
    private readonly Laboratory.Renderer.StatusOverlayRenderer _statusOverlay = new();

    public GameLoop(List<GameEntity> entities, EntityRenderer renderer, PlayerController playerController, Laboratory.Renderer.MenuToRender? menu = null, Laboratory.Renderer.IMenuRenderer? menuRenderer = null)
    {
        _entities = entities;
        _renderer = renderer;
        _playerController = playerController;
        _menu = menu;
        _menuRenderer = menuRenderer;
    }

    public void Run()
    {
        Console.CursorVisible = false;
        Console.Clear();

        while (true)
        {
            HandleInput();
            UpdateEntities();
            _collisionDetector.Tick();
            StatusEffectManager.Instance.TickAll();
            RenderEntities();
            _statusOverlay.DrawOverlays();
            Thread.Sleep(50);
        }
    }

    private void HandleInput()
    {
        if (!Console.KeyAvailable) return;

        var key = Console.ReadKey(true).Key;
        _playerController.HandleInput(key);

        // Flush remaining keys
        while (Console.KeyAvailable) Console.ReadKey(true);
    }

    private void UpdateEntities()
    {
        // Iterate over a snapshot to allow entities to add/remove items from the
        // main list during their Update() without causing an InvalidOperationException.
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
        // Erase all entities at their previous positions
        foreach (var entity in _entities)
        {
            _renderer.Erase(entity); // use PreviousPosition
        }

        // Draw all entities at their current positions
        foreach (var entity in _entities)
        {
            _renderer.Draw(entity);
        }

        // Draw UI overlay (hearts/menu) on top of entities
        if (_menu != null && _menuRenderer != null)
        {
            _menuRenderer.Render(_menu);
        }
    }

}