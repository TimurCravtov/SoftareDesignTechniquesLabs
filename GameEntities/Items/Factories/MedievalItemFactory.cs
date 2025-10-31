using Laboratory.GameEntities.Items.Food;
using Laboratory.GameEntities.Items.Powerup;

namespace Laboratory.GameEntities.Items.Factories;

public class MedievalItemFactory : InGameItemFactory
{
    public override IPowerup CreatePowerup()
    {
        // Medieval theme: keep ElectricGuitar as a quirky medieval lute replacement
        return new ElectricGuitar();
    }

    public override IFood CreateFood()
    {
        // Medieval themed food - omelette
        return new Omelette();
    }

    public override Laboratory.Renderer.MenuToRender CreateMenuToRender()
    {
        // Medieval hearts
        var heart = new Laboratory.Renderer.MedievalHeart();
        return new Laboratory.Renderer.MenuToRender(Laboratory.Game.GameState.Instance, heart);
    }
}
