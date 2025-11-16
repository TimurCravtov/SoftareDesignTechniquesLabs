**## Laboratory 3: Structural & Behavioral Patterns — Facade, Decorator, Flyweight

### Introduction

This report describes three design patterns relevant to the project: the Facade (structural/architectural), the Decorator (structural/behavioral), and the Flyweight (structural). 

## Implemented & Proposed Patterns

The repository now contains or benefits from the following patterns:

- Flyweight — implemented: `GameEntities/Characters/CharacterFactory.cs` and `Program.cs` now use it to reuse `CharacterType` instances.
- Facade — proposed and partially wired: `Game/GameLoop.cs` has been simplified to use a single `GameEngineFacade` orchestration; a concrete `GameEngineFacade` class is the recommended consolidation point for input, audio, entity lifecycle, collision, status effects, rendering and UI rendering.
- Decorator — recommended: use decorators to compose runtime abilities and visual/audio augmentations for `GameEntity` instances (status effects / powerups). The project already contains `IStatusEffect`, `StatusEffectManager`, `SpeedBoostEffect`, and `ShieldEffect`; these are a good basis for a decorator-like approach.

Below each pattern is explained with concrete references and short code sketches showing how to integrate the pattern with the existing codebase.

---

### 1) Flyweight — share intrinsic state for characters

Motivation
- In this project `CharacterType` holds display/sprite data and a small amount of intrinsic state (base HP, name, sprite lines). These values are identical across many instances of the same logical character type (e.g., many `Robot` instances). Storing a copy per entity wastes memory and makes it harder to change shared attributes at runtime.

What was done
- Added `GameEntities/Characters/CharacterFactory.cs` which stores and returns shared `CharacterType` records by name. `Program.cs` now acquires types from the factory instead of using `new CharacterType(...)` directly.

Files changed / added
- `GameEntities/Characters/CharacterFactory.cs` — new flyweight factory that holds `Dictionary<string, CharacterType>` and `GetOrCreate(...)`.
- `Program.cs` — replaced direct `new CharacterType(...)` invocations with `characterFactory.GetOrCreate(...)` calls.

Why Flyweight fits
- Many enemies or projectiles can use the same visual representation and base stats. Sharing `CharacterType` reduces memory duplication and centralizes type metadata.

Code excerpt (factory usage already in `Program.cs`):

```csharp
var characterFactory = new Characters.CharacterFactory();
var robotType = characterFactory.GetOrCreate("Robot", 10, new[] { ".\\", "\\_/" });
var playerType = characterFactory.GetOrCreate("Player", 5, new[] { "(o-o)", " /П\\", "  л" });
```

---

### 2) Facade — simplify GameLoop orchestration

Motivation
- `GameLoop` coordinates many subsystems every frame: input handling, audio, entity updates and lifecycle, collision detection and resolution, status effect ticking, rendering and UI rendering.
- Repeatedly coordinating these subsystems scatters orchestration code and makes unit-testing the per-frame behaviour harder.

Proposed / partial integration
- Introduce a `GameEngineFacade` class that exposes a small surface:

- `void Initialize(string audioIntro)`
- `void UpdateFrame()`
- `void Shutdown()`

Internally `GameEngineFacade` will orchestrate the seven subsystems the `GameLoop` currently handles manually:

- Input controller (`PlayerController` / input handling)
- Audio (e.g., `AudioManager`)
- Entity lifecycle & manager (`EntityManager`)
- Collision system (`Collision` / `CollisionDetector`)
- Status effects (`StatusEffectManager`)
- Rendering (`EntityRenderer`)
- UI rendering (`ConsoleMenuRenderer` / `MenuToRender`)

Repository-level integration
- `Game/GameLoop.cs` has been simplified to construct and call a `GameEngineFacade` each frame. This reduces `GameLoop` to a loop which calls `_facade.UpdateFrame()` and sleeps; initialization is delegated to `_facade.Initialize(...)`.

Recommended implementation sketch (new file `Game/GameEngineFacade.cs`):

```csharp
using System.Collections.Generic;
using Laboratory.Characters;
using Laboratory.Renderer;
using Laboratory.Audio;
using Laboratory.Game.Effects;

public class GameEngineFacade
{
    private readonly List<GameEntity> _entities;
    private readonly EntityRenderer _renderer;
    private readonly PlayerController _input;
    private readonly Renderer.MenuToRender? _menu;
    private readonly Renderer.IMenuRenderer? _menuRenderer;

    public GameEngineFacade(List<GameEntity> entities, EntityRenderer renderer, PlayerController input, Renderer.MenuToRender? menu = null, Renderer.IMenuRenderer? menuRenderer = null)
    {
        _entities = entities;
        _renderer = renderer;
        _input = input;
        _menu = menu;
        _menuRenderer = menuRenderer;
    }

    public void Initialize(string startupAudio)
    {
        // audio init, preload important assets, play intro sound
        Audio.AudioManager.Instance?.Play(startupAudio);
    }

    public void UpdateFrame()
    {
        // 1. Input
        _input?.HandleInput();

        // 2. Entities update (movement, AI, bullets)
        for (int i = 0; i < _entities.Count; i++) _entities[i].Update();

        // 3. Collision resolution
        CollisionDetector.ResolveAll(_entities);

        // 4. Effects tick
        StatusEffectManager.Instance.TickAll();

        // 5. Entity lifecycle (removals/additions)
        EntityManager.Instance.Cleanup();

        // 6. Rendering
        _renderer.Render(_entities);

        // 7. UI / Menus
        if (_menu != null && _menuRenderer != null) _menuRenderer.Render(_menu);
    }
}
```

Notes
- The facade keeps code in `GameLoop` small and testable: unit tests can replace the facade with a fake and assert sequencing or skip subsystems entirely.
- The facade can expose coarse-grained test hooks (for example `Pause()`, `SingleStep()` or `InjectFrameTime`) to allow deterministic unit tests.

---

### 3) Decorator — compose runtime behavior like status effects and powerups

Motivation
- Some behaviours (temporary shields, speed boosts, audio cues, visual overlays) are orthogonal to the entity's core responsibilities and may be added/removed at runtime.
- The Decorator pattern allows adding responsibilities to objects dynamically by wrapping them with decorator objects that implement the same interface.

```csharp
using Laboratory.Audio;
using Laboratory.Characters;

namespace Laboratory.GameEntities.Items.Powerup.Decorators;

public class PowerupPickUpAudioEffectDecorator: IPowerup
{
    public int Duration { get; }
    public string Name { get;  }
    public string[] Sprite { get; }
    private IPowerup _powerup;
    private string _audiofile;

    public PowerupPickUpAudioEffectDecorator(IPowerup powerup, string audiofile)
    {
        this.Duration = powerup.Duration;
        this._powerup = powerup;
        this._audiofile = audiofile;
        this.Sprite = powerup.Sprite;
        this.Name = powerup.Name;
    }
    
    public void Use(GameEntity user)
    {
        AudioManager.Instance.PlayAudioEffect(_audiofile);
        _powerup.Use(user);
    }
}

```

## Summary: what was implemented in the repo and what remains

- Implemented: Flyweight — `GameEntities/Characters/CharacterFactory.cs` was added and `Program.cs` updated to use it. This reduces duplicated `CharacterType` allocations and centralizes type creation.
- Partially integrated: Facade — `Game/GameLoop.cs` has been simplified to call a `GameEngineFacade` but the concrete `GameEngineFacade` class is recommended to be added (a sketch is shown above). Implementing it will consolidate per-frame orchestration into a single class and make `GameLoop` trivial and testable.
- Designer guidance: Decorator — the project already contains `StatusEffect` implementations and `StatusEffectManager` which realize behavior composition. If stronger structural decoration is required (wrapping entities in decorator objects), the `GameEntityDecorator` pattern is included above as a suggested extension.

## References

- Repository files referenced in this report:
  - `GameEntities/Characters/CharacterFactory.cs` (new)
  - `Program.cs` (uses factory)
  - `Game/GameLoop.cs` (now delegates orchestration to `GameEngineFacade`)
  - `Game/Effects/IStatusEffect.cs`, `Game/Effects/StatusEffectManager.cs`, `Game/Effects/SpeedBoostEffect.cs`, `Game/Effects/ShieldEffect.cs`
  - `GameEntities/Items/Factories/*`
  - `PowerupPickUpAudioEffectDecorator`

---
