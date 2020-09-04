using UnityEngine.Audio;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound", menuName = "Sounds")]
public class Sound : ScriptableObject
{
    public AudioClip Clip;
    public AudioMixerGroup Mixer;
    public bool PlayOnAwake;
    public bool Loop;
}
