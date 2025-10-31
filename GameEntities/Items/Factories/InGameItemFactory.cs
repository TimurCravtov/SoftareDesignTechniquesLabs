namespace Laboratory.GameEntities.Items;

public abstract class InGameItemFactory
{
    public abstract IFood CreateFood();
    public abstract IPowerup CreatePowerup();

    // Create a themed menu renderer (e.g., hearts display) bound to the global GameState
    public abstract Laboratory.Renderer.MenuToRender CreateMenuToRender();
}

