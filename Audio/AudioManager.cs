using NAudio.Wave;

namespace Laboratory.Audio;

public class AudioManager: IAudioManager
{
    private static AudioManager? _instance;
    private IWavePlayer? _outputDevice;
    private Mp3FileReader? _audioFile;

    private AudioManager() { }

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new AudioManager();
            return _instance;
        }
    }

    
    public void PlayBackgroundMusic(string filename)
    {
        // ... path construction code remains the same ...
        string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\.."));
        string path = Path.Combine(projectRoot, "Audio", "AudioAssets", filename);

        Stop(); // Stop and dispose of any previous playback

        _audioFile = new NAudio.Wave.Mp3FileReader(path); 
        _outputDevice = new WaveOutEvent();
    
        _outputDevice.Init(_audioFile);
        _outputDevice.Volume = 1.0f;
        _outputDevice.Play();
    
        if (true) 
        {
            // Now this uses the persistent fields
            _outputDevice.PlaybackStopped += (s, e) =>
            {
                // You must check if playback stopped due to a user call to Stop()
                // or if it reached the end of the file
                if (e.Exception == null) 
                {
                    _audioFile.Position = 0; // Rewind the file
                    _outputDevice.Play(); // Play again
                }
            };
        }
    }

    public void PlayAudioEffect(string filename)
    {
        string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\.."));
        string path = Path.Combine(projectRoot, "Audio", "AudioAssets", filename);
    
        var audio = new Mp3FileReader(path);
        var device = new WaveOutEvent();
    
        device.PlaybackStopped += (s, e) =>
        {
            device.Dispose();
            audio.Dispose();
        };
    
        device.Init(audio);
        device.Volume = 1.0f;
        device.Play();
    }
    public void Stop()
    {
        _outputDevice?.Stop();
        _outputDevice?.Dispose();
        _audioFile?.Dispose();
        _outputDevice = null;
        _audioFile = null;
    }
}