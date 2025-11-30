using System;
using lab4.Utils;

namespace lab4.Patterns.Chain
{
    public class User
    {
        public string Username { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool HasSubscription { get; set; }
        public string Region { get; set; }

        public User(string username, bool isLoggedIn, bool hasSubscription, string region)
        {
            Username = username;
            IsLoggedIn = isLoggedIn;
            HasSubscription = hasSubscription;
            Region = region;
        }
    }

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

    public class AuthenticationHandler : AccessHandler
    {
        public override bool Handle(User user, string contentRegion)
        {
            Logger.LogSystem("Checking authentication...");
            if (!user.IsLoggedIn)
            {
                Logger.LogError($"User {user.Username} is not logged in.");
                return false;
            }
            Logger.LogSuccess("Authentication successful.");
            return base.Handle(user, contentRegion);
        }
    }

    public class SubscriptionHandler : AccessHandler
    {
        public override bool Handle(User user, string contentRegion)
        {
            Logger.LogSystem("Checking subscription status...");
            if (!user.HasSubscription)
            {
                Logger.LogError($"User {user.Username} does not have an active subscription.");
                return false;
            }
            Logger.LogSuccess("Subscription active.");
            return base.Handle(user, contentRegion);
        }
    }

    public class RegionHandler : AccessHandler
    {
        public override bool Handle(User user, string contentRegion)
        {
            Logger.LogSystem($"Checking region availability (User: {user.Region}, Content: {contentRegion})...");
            if (user.Region != contentRegion && contentRegion != "Global")
            {
                Logger.LogWarning($"Content is not available in user's region ({user.Region}).");
                return false;
            }
            Logger.LogSuccess("Region check passed.");
            return base.Handle(user, contentRegion);
        }
    }
}
