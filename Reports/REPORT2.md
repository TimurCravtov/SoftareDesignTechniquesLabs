## Laboratory 1: Creational Design Patterns

### Introduction

Creational design patterns provide standard ways to create objects in a system while keeping the creation logic separate from the business logic. These patterns improve flexibility, testability and decouple concrete types from the code that uses them.

Common creational patterns include:

- Singleton
- Factory Method
- Abstract Factory
- Builder
- Prototype
- Object Pool (commonly used in games as a performance-minded creational variation)

This report explains which creational patterns are present in the project and shows concrete examples from the repository.

## Implemented patterns

In this project I used the following creational patterns (observed in the codebase):

- Abstract Factory — `InGameItemFactory` and its concrete factories (`MedievalItemFactory`, `SciFiItemFactory`).
- Singleton — `GameState.Instance` provides a single shared game state.
- Object Pool — `BulletPoolManager` implements a pool for `Bullet` objects to avoid frequent allocations.

### 1) Abstract Factory

Files/classes that demonstrate Abstract Factory:

- `GameEntities/Items/Factories/InGameItemFactory.cs` — abstract factory interface with methods to create themed items (`CreateFood`, `CreatePowerup`) and a themed menu (`CreateMenuToRender`).
- `GameEntities/Items/Factories/MedievalItemFactory.cs` — medieval-themed factory that returns `Omelette` and `ElectricGuitar` and a `MedievalHeart`-backed menu renderer.
- `GameEntities/Items/Factories/SciFiItemFactory.cs` — sci-fi themed counterpart that returns `Toothpaste`, `ElectricGuitar` and a `SciFiHeart` menu renderer.

Why this is Abstract Factory: the code groups related creators (food, powerup, menu) in a single factory interface. The concrete factories provide families of related objects for a given theme without exposing concrete classes to the calling code.

Evidence in code (abstract factory declaration):

```csharp
public abstract class InGameItemFactory
{
	public abstract IFood CreateFood();
	public abstract IPowerup CreatePowerup();
	public abstract Laboratory.Renderer.MenuToRender CreateMenuToRender();
}
```

And usage in `Program.cs` where a concrete factory is chosen and used to create a menu:

```csharp
var itemFactory = new SciFiItemFactory();
var menu = itemFactory.CreateMenuToRender();
var menuRenderer = new ConsoleMenuRenderer();
```

Benefit: the game can switch themes by swapping the factory instance (for example between `MedievalItemFactory` and `SciFiItemFactory`) with no other changes to game initialization or rendering code.

### 2) Singleton

Files/classes that demonstrate Singleton:

- `Game/GameState.cs` — a single, globally accessible `GameState.Instance` used to hold and share the `Player` object and other game-wide state.

Singleton implementation (simplified):

```csharp
public sealed class GameState
{
	private static readonly GameState _instance = new();
	public static GameState Instance => _instance;
	private GameState() { }

	public Player? Player { get; private set; }
	public void SetPlayer(Player player) { Player = player; }
}
```

Example usage in `Program.cs`:

```csharp
GameState.Instance.SetPlayer(player);
```

Rationale: a singleton is appropriate here to provide a simple, globally-accessible point for UI and menu renderers to read player state (HP etc.) without needing to thread the player reference through many constructors. This keeps the renderer APIs simple. Note: singletons have trade-offs (global state, harder to test) — constructor injection is used elsewhere in the project for better testability where appropriate.

### 3) Object Pool

Files/classes that demonstrate an object pool:

- `GameEntities/Ammo/BulletPoolManager.cs` — preallocates a queue of `Bullet` objects, provides `GetBullet()` and `ReturnBullet()` methods and reuses bullet instances instead of creating/destroying them frequently.

Excerpt from `BulletPoolManager` showing initialization and reuse logic:

```csharp
public class BulletPoolManager: IBulletPoolManager
{
	private int POOL_SIZE = 50;
	private readonly Queue<Bullet> _bulletPool = new Queue<Bullet>();

	public BulletPoolManager()
	{
		foreach (int i in Enumerable.Range(0, POOL_SIZE))
		{
			Bullet bullet = new Bullet();
			bullet.Deactivate();
			bullet.ReturnToPool = ReturnBullet;
			_bulletPool.Enqueue(bullet);
		}
	}

	public Bullet GetBullet() { /* dequeue or create */ }
	public void ReturnBullet(Bullet bullet) { /* reset and enqueue */ }
}
```

Why use a pool: in a real-time game loop, creating and GC-ing many small objects each frame can cause performance jitter. A pool reduces allocations and keeps memory churn predictable.

## Other creational patterns (not used / partial usage)

- Factory Method: The project does not contain a classic Factory Method hierarchy; object creation of most game entities is done directly where needed or via the Abstract Factory for themed items.
- Builder / Prototype: Not present in the repository; these patterns would be useful if game entities required complex step-wise construction or cloning of pre-configured prototypes.

## Conclusion

The project demonstrates useful creational patterns used in many games. The Abstract Factory groups related item/menu creation so themes can be switched easily. The Singleton `GameState` provides a simple global state holder (used judiciously). The `BulletPoolManager` is a pragmatic object-pool implementation that improves runtime performance by reusing `Bullet` instances. These choices make the game modular, theme-switchable, and efficient.

## References

- Repository code: `GameEntities/Items/Factories/InGameItemFactory.cs`, `GameEntities/Items/Factories/MedievalItemFactory.cs`, `GameEntities/Items/Factories/SciFiItemFactory.cs`, `Game/GameState.cs`, `GameEntities/Ammo/BulletPoolManager.cs`, `Program.cs`.

````
