using System.Drawing;
using Laboratory.Audio;
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
    // orchestration moved to GameEngineFacade
    private GameEngineFacade? _facade;

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
        
        // create facade to encapsulate subsystems
        _facade = new GameEngineFacade(_entities, _renderer, _playerController, _menu, _menuRenderer);
        _facade.Initialize("Private/hearingdamage.mp3");

        while (true)
        {
            _facade.UpdateFrame();
            Thread.Sleep(50);
        }
    }


}