namespace Laboratory.Characters;

public interface IRemovable
{
    bool ShouldBeRemoved { get; }  
    void Remove();
}
