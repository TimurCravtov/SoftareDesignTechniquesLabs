using System;
using System.Collections.Generic;
using lab4.Utils;

namespace lab4.Patterns.Strategy
{
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

    public class PersonalizedStrategy : IRecommendationStrategy
    {
        public List<string> GetRecommendations(string userId)
        {
            Logger.LogSystem($"Generating 'For You' recommendations for user {userId}...");
            // Mock logic based on user history
            return new List<string> { "Black Mirror", "Love, Death & Robots", "Cyberpunk: Edgerunners" };
        }
    }

    public class FriendsLikesStrategy : IRecommendationStrategy
    {
        public List<string> GetRecommendations(string userId)
        {
            Logger.LogSystem($"Generating 'Friends are Watching' recommendations for user {userId}...");
            return new List<string> { "Friends", "The Office", "Brooklyn Nine-Nine" };
        }
    }

    public class RecommendationEngine
    {
        private IRecommendationStrategy _strategy;

        public void SetStrategy(IRecommendationStrategy strategy)
        {
            _strategy = strategy;
            Logger.LogInfo($"Switched recommendation strategy to {_strategy.GetType().Name}");
        }

        public void ShowRecommendations(string userId)
        {
            if (_strategy == null)
            {
                Logger.LogError("No recommendation strategy selected!");
                return;
            }

            var recommendations = _strategy.GetRecommendations(userId);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"--- Recommendations for {userId} ---");
            foreach (var rec in recommendations)
            {
                Console.WriteLine($"- {rec}");
            }
            Console.WriteLine("-----------------------------------");
            Console.ResetColor();
        }
    }
}
