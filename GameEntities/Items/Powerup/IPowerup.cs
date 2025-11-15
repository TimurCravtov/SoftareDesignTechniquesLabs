using Laboratory.Characters;

namespace Laboratory.GameEntities.Items.Powerup
{
    public interface IPowerup: IRenderableItem
    {
        int Duration { get; }
        void Use(GameEntity user);
    }
}
