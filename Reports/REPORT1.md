## Laboratory 0: Implementing 3 principles of SOLID

### Introduction

_SOLID_ is an acronym which stands for 5 principles of software development following which the system is less likely to be bad designed.

The five principles are:

- S — Single Responsibility Principle (SRP)
- O — Open/Closed Principle (OCP)
- L — Liskov Substitution Principle (LSP)
- I — Interface Segregation Principle (ISP)
- D — Dependency Inversion Principle (DIP)

Now I'll show how those principles are implemented in my code. 

## Implemented principles

I implemented Single Responsibility Principle (S), Liskov Substitution Principle (L) and Dependency Inversion Principle (D).

### 1) Single Responsibility Principle (SRP)

Files/classes that demonstrate SRP:

- `GameEntity` (in `Characters/GameEntity.cs`) — models the position/state of any game entity and exposes an abstract `Update()` method. It contains only entity state and the contract for per-frame behaviour.
- `PlayerController` (in `InputController/PlayerController.cs`) — parses keyboard input and converts it into player actions; it updates the player's position but does not render or manage other entities.
- `EntityRenderer` and `MapRenderer` (in `Renderer/`) — handle drawing and erasing responsibilities only; they don't manage game state or input.

Why SRP matters here: separating concerns (state, input, rendering) makes each component easier to understand, test and change. For example, rendering code can be rewritten or replaced without affecting input handling or entity logic.

### 2) Liskov Substitution Principle (LSP)

Examples:

- `GameEntity` is an abstract base class with `Position`, `PreviousPosition` and the abstract `Update()` method. Concrete types (`Player`, `RobotEntity`) extend `GameEntity` and can be used wherever `GameEntity` is expected.
- `GameLoop` operates on a `List<GameEntity>` and calls `Update()` on each element without checking concrete types. This demonstrates substitutability: replacing a `GameEntity` with any subclass preserves program correctness.

Evidence in code (simplified):

```csharp
foreach (var entity in _entities)
	entity.Update();
```

`RobotEntity` adds behavior (movement and removal) but respects the `GameEntity` contract — it doesn't throw unexpected exceptions or alter semantics expected by `GameLoop`.

### 3) Dependency Inversion Principle (DIP)

Examples and rationale:

- The rendering behaviour is expressed via the `IEntityRenderer` abstraction which `EntityRenderer` implements. High-level code (`GameLoop`) depends on that abstraction instead of a concrete implementation, e.g. `_renderer.Draw(entity)` and `_renderer.Erase(entity)`. This allows replacing the renderer with another implementation (for tests or a different output target) without changing the game loop.
- Constructor injection is used for key components (for example, `PlayerController` receives a `Player` instance, `GameLoop` is constructed with renderer/controller instances). This makes dependencies explicit and easier to replace in tests.

Benefit: depending on abstractions decouples high-level game logic from low-level details and improves testability and extensibility.

## Concrete code snippets

Short extracts already present in the repository that illustrate the above points:

- Game loop using polymorphism (LSP):

```csharp
foreach (var entity in _entities)
	entity.Update();
```

- EntityRenderer (DIP + SRP):

```csharp
public void Draw(GameEntity entity) { /* draws sprite at entity.Position */ }
public void Erase(GameEntity entity) { /* clears area at entity.PreviousPosition */ }
```

- PlayerController mapping input to actions (SRP):

```csharp
public void HandleInput(ConsoleKey key)
{
	PlayerAction action = key switch { /* mapping */ };
	ExecuteAction(action);
}
```

## Open/Closed Principle (OCP)

Although the original report focused on S, L and D, the project is also designed to be open for extension and closed for modification in practical ways:

- New character types can be added by extending `GameEntity` and implementing `Update()` as needed (see `Characters/Enemies/RobotEntity.cs`). The `GameLoop` treats all entities as `GameEntity` instances, so introducing new subclasses requires no change to the loop or renderer — only the new class needs to be added.

- This design follows OCP in that behaviour is extended via new subclasses rather than modifying existing core logic.

## Removable interface

The project defines an `IRemovable` interface (`Characters/IRemovable.cs`) used by entities that can be removed during the game (for example, `RobotEntity` implements `IRemovable`). The interface contains a `bool ShouldBeRemoved { get; }` property and a `Remove()` method. `GameLoop` checks for this interface and removes items from the entity list when `ShouldBeRemoved` is true:

```csharp
if (_entities[i] is IRemovable r && r.ShouldBeRemoved)
{
	_renderer.Erase(_entities[i]);
	_entities.RemoveAt(i);
}
```

Mentioning `IRemovable` in the report clarifies how entities indicate lifecycle/state changes without coupling the removal logic to concrete types.

## Conclusion

The implemented design separates responsibilities (SRP), relies on polymorphism so derived entities substitute for their base type (LSP), and depends on abstractions for key subsystems like rendering (DIP). These choices improve maintainability, testability and extensibility of the small console game.

## References

- Repository code: `Characters/GameEntity.cs`, `Characters/Player.cs`, `Characters/Enemies/RobotEntity.cs`, `InputController/PlayerController.cs`, `Renderer/EntityRenderer.cs`, `Game/GameLoop.cs`.
