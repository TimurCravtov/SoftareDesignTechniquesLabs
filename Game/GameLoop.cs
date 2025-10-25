using Laboratory.Audio;
using Laboratory.InputController;

namespace Laboratory.Game;

using System;
using System.Collections.Generic;
using System.Threading;
using Laboratory.Characters;
using Laboratory.Renderer;

public class GameLoop
{
    private readonly List<GameEntity> _entities;
    private readonly EntityRenderer _renderer;
    private readonly PlayerController _playerController;

    public GameLoop(List<GameEntity> entities, EntityRenderer renderer, PlayerController playerController)
    {
        _entities = entities;
        _renderer = renderer;
        _playerController = playerController;
    }

    public void Run()
    {
        Console.CursorVisible = false;
        Console.Clear();
        
        AudioManager.Instance.PlayBackgroundMusic("my-universe-147152.mp3");
        
        while (true)
        {
            HandleInput();
            UpdateEntities();
            RenderEntities();
            Thread.Sleep(100);
            
            if (DateTime.Now.Second % 3 == 0) AudioManager.Instance.PlayAudioEffect("laser-45816.mp3");
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
        foreach (var entity in _entities)
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
    }

}