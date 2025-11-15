using Laboratory.Audio;
using Laboratory.Characters;

namespace Laboratory.GameEntities.Items.Powerup.Decorators;

public class PowerupPickUpAudioEffectDecorator: IPowerup
{
    public int Duration { get; }
    public string Name { get;  }
    public string[] Sprite { get; }
    private IPowerup _powerup;
    private string _audiofile;

    public PowerupPickUpAudioEffectDecorator(IPowerup powerup, string audiofile)
    {
        this.Duration = powerup.Duration;
        this._powerup = powerup;
        this._audiofile = audiofile;
        this.Sprite = powerup.Sprite;
        this.Name = powerup.Name;
    }
    
    public void Use(GameEntity user)
    {
        AudioManager.Instance.PlayAudioEffect(_audiofile);
        _powerup.Use(user);
    }
}
