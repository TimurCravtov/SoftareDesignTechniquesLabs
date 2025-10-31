namespace Laboratory.Renderer
{
    public interface IHeart
    {
        // Render a filled heart (player HP present)
        void DisplayFilled();

        // Render an empty heart (HP missing)
        void DisplayEmpty();
    }
}
