using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AudioService : MonoBehaviour
{
    [SerializeField] private List<Sound> _sounds = new List<Sound>();
    private Dictionary<Sound, AudioSource> _soundDictionary = new Dictionary<Sound, AudioSource>();

    public void Init()
    {
        InitSounds();
    }

    public void Play(Sound sound)
    {
        if (_soundDictionary.ContainsKey(sound))
        {
            _soundDictionary[sound].Play();
        }
        else
        {
            AddNewSound(sound);
            _soundDictionary[sound].Play();
        }
    }

    private void InitSounds()
    {
        for (int i = 0; i < _sounds.Count; i++)
        {
            _soundDictionary.Add(_sounds[i], gameObject.AddComponent<AudioSource>());
        }

        foreach (var sound in _soundDictionary)
        {
            InitSoundByIndex(sound);
        }
    }

    private void AddNewSound(Sound sound)
    {
        _soundDictionary.Add(sound, gameObject.AddComponent<AudioSource>());
        InitSoundByIndex(_soundDictionary.SingleOrDefault(p => p.Key == sound));
    }

    private void InitSoundByIndex(KeyValuePair<Sound,AudioSource> pair)
    {
        pair.Value.clip = pair.Key.Clip;
        pair.Value.outputAudioMixerGroup = pair.Key.Mixer;
        pair.Value.playOnAwake = pair.Key.PlayOnAwake;
        pair.Value.loop = pair.Key.PlayOnAwake;
        if (pair.Value.playOnAwake)
            pair.Value.Play();
    }
}
