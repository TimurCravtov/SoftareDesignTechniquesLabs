## Laboratory 4: Behavioral Design Patterns

### Introduction

Behavioral design patterns are concerned with algorithms and the assignment of responsibilities between objects. They describe not just patterns of objects or classes but also the patterns of communication between them. These patterns characterize complex control flow that's difficult to follow at run-time. They shift your focus away from flow of control to let you concentrate just on the way objects are interconnected.

Common behavioral patterns include:

- Chain of Responsibility
- Command
- Interpreter
- Iterator
- Mediator
- Memento
- Observer
- State
- Strategy
- Template Method
- Visitor

This report explains which behavioral patterns are implemented in the `lab4` project (Netflix Service Simulation) and shows concrete examples.

## Implemented patterns

In this project, I implemented the following behavioral patterns:

- **Chain of Responsibility** — Used for the user access control pipeline (Authentication -> Subscription -> Region checks).
- **Strategy** — Used for the recommendation engine to switch between different recommendation algorithms (Trending, Personalized, Friends).
- **Observer** — Used for the notification system where users subscribe to series updates.
- **Command** — Used for the video player remote control to encapsulate actions as objects.

### 1) Chain of Responsibility

Files/classes that demonstrate Chain of Responsibility:
- `lab4/Patterns/Chain/AccessHandlers.cs` — Contains the `AccessHandler` abstract base and concrete handlers: `AuthenticationHandler`, `SubscriptionHandler`, `RegionHandler`.

**Why this is Chain of Responsibility:**
The request (user access) is passed along a chain of handlers. Each handler decides either to process the request (and pass it to the next handler) or to stop it. This decouples the sender of the request from its receivers and allows dynamic composition of the validation logic.

**Evidence in code:**

The abstract handler defining the link to the next handler:
```csharp
public abstract class AccessHandler
{
    protected AccessHandler _nextHandler;

    public AccessHandler SetNext(AccessHandler nextHandler)
    {
        _nextHandler = nextHandler;
        return nextHandler;
    }

    public virtual bool Handle(User user, string contentRegion)
    {
        if (_nextHandler != null)
        {
            return _nextHandler.Handle(user, contentRegion);
        }
        return true;
    }
}
```

Setting up the chain in `Program.cs`:
```csharp
var authHandler = new AuthenticationHandler();
var subHandler = new SubscriptionHandler();
var regionHandler = new RegionHandler();

// Build the chain: Auth -> Subscription -> Region
authHandler.SetNext(subHandler).SetNext(regionHandler);

// Execute
authHandler.Handle(validUser, "US");
```

### 2) Strategy

Files/classes that demonstrate Strategy:
- `lab4/Patterns/Strategy/RecommendationSystem.cs` — Defines `IRecommendationStrategy` and concrete strategies (`TrendingStrategy`, `PersonalizedStrategy`, `FriendsLikesStrategy`) and the context `RecommendationEngine`.

**Why this is Strategy:**
The recommendation algorithm can be selected and swapped at runtime. The `RecommendationEngine` (Context) delegates the work to a linked `IRecommendationStrategy` object. This allows the client to change the behavior of the recommendation system without modifying the engine itself.

**Evidence in code:**

The strategy interface and a concrete implementation:
```csharp
public interface IRecommendationStrategy
{
    List<string> GetRecommendations(string userId);
}

public class TrendingStrategy : IRecommendationStrategy
{
    public List<string> GetRecommendations(string userId)
    {
        Logger.LogSystem("Generating 'Trending Now' recommendations...");
        return new List<string> { "Stranger Things", "The Crown", "Squid Game", "Wednesday" };
    }
}
```

The context allowing strategy switching:
```csharp
public class RecommendationEngine
{
    private IRecommendationStrategy _strategy;

    public void SetStrategy(IRecommendationStrategy strategy)
    {
        _strategy = strategy;
    }

    public void ShowRecommendations(string userId)
    {
        var recommendations = _strategy.GetRecommendations(userId);
        // ... display logic
    }
}
```

### 3) Observer

Files/classes that demonstrate Observer:
- `lab4/Patterns/Observer/NotificationSystem.cs` — Defines `ISubscriber` (Observer) and `Series` (Subject).

**Why this is Observer:**
The `Series` object maintains a list of dependents (`ISubscriber`s) and notifies them automatically of any state changes (new episodes). This implements a publish-subscribe mechanism where the subject doesn't need to know the concrete class of the observers.

**Evidence in code:**

The Subject (`Series`) managing subscribers:
```csharp
public class Series
{
    private readonly List<ISubscriber> _subscribers = new List<ISubscriber>();

    public void Subscribe(ISubscriber subscriber)
    {
        _subscribers.Add(subscriber);
    }

    public void ReleaseNewEpisode(string episodeTitle)
    {
        NotifySubscribers(episodeTitle);
    }

    private void NotifySubscribers(string episodeTitle)
    {
        foreach (var subscriber in _subscribers)
        {
            subscriber.Update(Title, episodeTitle);
        }
    }
}
```

The Observer (`NetflixUser`) receiving updates:
```csharp
public class NetflixUser : ISubscriber
{
    public void Update(string seriesName, string episodeTitle)
    {
        Logger.LogInfo($"Notification for {Name}: New episode of '{seriesName}' is out! - '{episodeTitle}'");
    }
}
```

### 4) Command

Files/classes that demonstrate Command:
- `lab4/Patterns/Command/VideoCommands.cs` — Defines `IVideoCommand`, concrete commands (`PlayCommand`, `PauseCommand`, etc.), the Invoker (`RemoteControl`), and the Receiver (`VideoPlayer`).

**Why this is Command:**
Requests to control the video player are encapsulated as objects (`PlayCommand`, `PauseCommand`). This allows parameterizing the `RemoteControl` with different requests and decoupling the object that invokes the operation from the one that knows how to perform it.

**Evidence in code:**

The Command interface and a concrete command:
```csharp
public interface IVideoCommand
{
    void Execute();
}

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
```

The Invoker (`RemoteControl`) executing commands:
```csharp
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
            _commands[button].Execute();
        }
    }
}
```
