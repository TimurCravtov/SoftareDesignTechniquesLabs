using System;
using System.Collections.Generic;
using lab4.Utils;

namespace lab4.Patterns.Observer
{
    public interface ISubscriber
    {
        void Update(string seriesName, string episodeTitle);
        string Name { get; }
    }

    public class NetflixUser : ISubscriber
    {
        public string Name { get; private set; }

        public NetflixUser(string name)
        {
            Name = name;
        }

        public void Update(string seriesName, string episodeTitle)
        {
            Logger.LogInfo($"Notification for {Name}: New episode of '{seriesName}' is out! - '{episodeTitle}'");
        }
    }

    public class Series
    {
        private readonly List<ISubscriber> _subscribers = new List<ISubscriber>();
        public string Title { get; private set; }

        public Series(string title)
        {
            Title = title;
        }

        public void Subscribe(ISubscriber subscriber)
        {
            _subscribers.Add(subscriber);
            Logger.LogSystem($"{subscriber.Name} subscribed to {Title}.");
        }

        public void Unsubscribe(ISubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
            Logger.LogSystem($"{subscriber.Name} unsubscribed from {Title}.");
        }

        public void ReleaseNewEpisode(string episodeTitle)
        {
            Logger.LogSuccess($"Releasing new episode for {Title}: {episodeTitle}");
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
}
