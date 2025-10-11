using Laboratory.Characters;

namespace Laboratory.Renderer
{
    public interface IEntityRenderer
    {
        void Draw(GameEntity entity);
        void Erase(GameEntity entity);
    }
}