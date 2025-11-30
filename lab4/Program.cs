using System;
using lab4.Patterns.Chain;
using lab4.Patterns.Command;
using lab4.Patterns.Observer;
using lab4.Patterns.Strategy;
using lab4.Utils;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Netflix Service Simulation ===\n");

            // 1. Chain of Responsibility: Access Control
            Logger.LogSystem("--- SCENARIO 1: Access Control (Chain of Responsibility) ---");
            
            var authHandler = new AuthenticationHandler();
            var subHandler = new SubscriptionHandler();
            var regionHandler = new RegionHandler();

            // Build the chain: Auth -> Subscription -> Region
            authHandler.SetNext(subHandler).SetNext(regionHandler);

            var validUser = new User("JohnDoe", isLoggedIn: true, hasSubscription: true, region: "US");
            var invalidUser = new User("Hacker", isLoggedIn: false, hasSubscription: false, region: "Unknown");
            var poorUser = new User("BrokeStudent", isLoggedIn: true, hasSubscription: false, region: "US");

            Logger.LogInfo("Attempting access for Valid User:");
            if (authHandler.Handle(validUser, "US"))
                Logger.LogSuccess("Access Granted!");
            else
                Logger.LogError("Access Denied.");

            Console.WriteLine();
            Logger.LogInfo("Attempting access for Invalid User:");
            authHandler.Handle(invalidUser, "US");

            Console.WriteLine();
            Logger.LogInfo("Attempting access for User without Subscription:");
            authHandler.Handle(poorUser, "US");

            Console.WriteLine("\n");


            // 2. Strategy: Recommendation System
            Logger.LogSystem("--- SCENARIO 2: Recommendations (Strategy) ---");
            
            var recEngine = new RecommendationEngine();
            
            // Default to Trending
            recEngine.SetStrategy(new TrendingStrategy());
            recEngine.ShowRecommendations(validUser.Username);

            // Switch to Personalized
            recEngine.SetStrategy(new PersonalizedStrategy());
            recEngine.ShowRecommendations(validUser.Username);

            // Switch to Friends
            recEngine.SetStrategy(new FriendsLikesStrategy());
            recEngine.ShowRecommendations(validUser.Username);

            Console.WriteLine("\n");


            // 3. Observer: Notification System
            Logger.LogSystem("--- SCENARIO 3: New Episode Notifications (Observer) ---");

            var strangerThings = new Series("Stranger Things");
            var blackMirror = new Series("Black Mirror");

            var user1 = new NetflixUser("Alice");
            var user2 = new NetflixUser("Bob");
            var user3 = new NetflixUser("Charlie");

            strangerThings.Subscribe(user1);
            strangerThings.Subscribe(user2);
            
            blackMirror.Subscribe(user2);
            blackMirror.Subscribe(user3);

            Console.WriteLine();
            // Release episodes
            strangerThings.ReleaseNewEpisode("Chapter One: The Vanishing of Will Byers");
            Console.WriteLine();
            blackMirror.ReleaseNewEpisode("Joan Is Awful");

            Console.WriteLine("\n");


            // 4. Command: Video Player Control
            Logger.LogSystem("--- SCENARIO 4: Video Player Control (Command) ---");

            var videoPlayer = new VideoPlayer();
            videoPlayer.SetVideo("Stranger Things - S1E1");

            var remote = new RemoteControl();
            
            // Map commands to buttons
            remote.SetCommand("Play", new PlayCommand(videoPlayer));
            remote.SetCommand("Pause", new PauseCommand(videoPlayer));
            remote.SetCommand("Rewind", new RewindCommand(videoPlayer));
            remote.SetCommand("FastForward", new FastForwardCommand(videoPlayer));

            // Simulate user interaction
            remote.PressButton("Play");
            remote.PressButton("FastForward");
            remote.PressButton("FastForward");
            remote.PressButton("Pause");
            remote.PressButton("Rewind");
            remote.PressButton("Play");
            remote.PressButton("Stop"); // Not configured

            Console.WriteLine("\n=== Simulation Complete ===");
        }
    }
}
