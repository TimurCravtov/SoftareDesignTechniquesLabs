using Laboratory.Characters;

namespace Laboratory.GameEntities.Items.Powerup
{
    public interface IPowerup
    {
        int Duration { get; }
        void Use(GameEntity user);
    }
}
