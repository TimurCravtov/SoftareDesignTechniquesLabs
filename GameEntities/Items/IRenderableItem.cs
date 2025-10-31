namespace Laboratory.GameEntities.Items
{
    // Abstraction for items that can be displayed in the world (provide a sprite and a name).
    public interface IRenderableItem
    {
        string Name { get; }
        string[] Sprite { get; }
    }
}
