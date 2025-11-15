using Laboratory.GameEntities.Items.Food;
using Laboratory.GameEntities.Items.Powerup;
using Laboratory.GameEntities.Items.Powerup.Decorators;

namespace Laboratory.GameEntities.Items.Factories;

public class MedievalItemFactory : InGameItemFactory
{
    public override IPowerup CreatePowerup()
    {
        return new PowerupPickUpAudioEffectDecorator(new Horse(), "horseffect.mp3");
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
