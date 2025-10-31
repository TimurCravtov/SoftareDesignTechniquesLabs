using System;
using Laboratory.Game;

namespace Laboratory.Renderer
{
    public class MenuToRender
    {
        public GameState GameState { get; }
        public IHeart HeartPrototype { get; }
        public int? FixedX { get; }
        public int? FixedY { get; }

        public MenuToRender(GameState gameState, IHeart heartPrototype, int? fixedX = null, int? fixedY = null)
        {
            GameState = gameState;
            HeartPrototype = heartPrototype;
            FixedX = fixedX;
            FixedY = fixedY;
        }

        // Current (filled) hearts to render
        public int CurrentHearts => Math.Max(0, GameState.Player?.Health ?? 0);

        // Max hearts to render (based on player's base health)
        public int MaxHearts => Math.Max(0, GameState.Player?.Type.BaseHealth ?? 0);
    }
}
