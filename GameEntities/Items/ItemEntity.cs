using System.Drawing;
using Laboratory.Characters;
using Laboratory.Reports;

namespace Laboratory.GameEntities.Items
{
    // A simple GameEntity wrapper for in-world items (food / powerups)
    public class ItemEntity : GameEntity
    {
        // The actual item instance (prefer IRenderableItem abstraction)
            public IRenderableItem Item { get; }

            public ItemEntity(CharacterType type, Point position, IRenderableItem item) : base(type, position)
            {
                Item = item;
            }

        // Items are static in the world — no per-tick behaviour by default
        public override void Update()
        {
            // noop
        }
    }
}
