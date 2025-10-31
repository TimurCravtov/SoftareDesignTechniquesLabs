using Laboratory.Characters;

namespace Laboratory.Game
{
    public sealed class GameState
    {
        private static readonly GameState _instance = new();

        public static GameState Instance => _instance;

        private GameState() { }

        public Player? Player { get; private set; }

        public void SetPlayer(Player player)
        {
            Player = player;
        }
    }
}
