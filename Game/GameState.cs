using Laboratory.Characters;

namespace Laboratory.Game
{
    public sealed class GameState
    {
        private static readonly GameState _instance = new();
        
        public static GameState Instance => _instance;

        private GameState() { }
        
        public Player? Player { get; private set; }

        public int SpeedBoostTicks { get; private set; }
        public int ShieldTicks { get; private set; }

        public void SetPlayer(Player player)
        {
            Player = player;
        }

        public void AddSpeedBoost(int ticks)
        {
            if (ticks > 0)
                SpeedBoostTicks += ticks;
        }

        public void AddShield(int ticks)
        {
            if (ticks > 0)
                ShieldTicks += ticks;
        }

        public void TickDownBoosts()
        {
            if (SpeedBoostTicks > 0) SpeedBoostTicks--;
            if (ShieldTicks > 0) ShieldTicks--;
        }
    }
}
