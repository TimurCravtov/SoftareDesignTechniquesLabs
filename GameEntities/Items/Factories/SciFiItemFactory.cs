using Laboratory.GameEntities.Items.Food;
using Laboratory.GameEntities.Items.Powerup;
using Laboratory.GameEntities.Items.Powerup.Decorators;

namespace Laboratory.GameEntities.Items.Factories;

public class SciFiItemFactory : InGameItemFactory
{
    public override IPowerup CreatePowerup()
    {
        return new PowerupPickUpAudioEffectDecorator(new ElectricGuitar(),"guitareffect.mp3");
    }

    public override IFood CreateFood()
    {
        return new Toothpaste();
    }
    public override Laboratory.Renderer.MenuToRender CreateMenuToRender()
    {
        var heart = new Laboratory.Renderer.SciFiHeart();
        return new Laboratory.Renderer.MenuToRender(Laboratory.Game.GameState.Instance, heart);
    }
}