using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class soundManager : MonoBehaviour
{
    public static soundManager instance;

    [SerializeField] private AudioSource fxObject;
    [SerializeField] private AudioMixerGroup sfxGroup;
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void playSound(AudioClip clip, Transform spawnTransform, float volume, float pitch, bool loop, string mixerGroup = "SFX")
    {        
        AudioSource audioSource = Instantiate(fxObject , spawnTransform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.loop = loop;
        audioSource.Play();
        AudioMixerGroup[] groups = audioMixer.FindMatchingGroups(mixerGroup);
        audioSource.outputAudioMixerGroup = groups.Length > 0 ? groups[0] : sfxGroup;
        if (!loop) { Destroy(audioSource.gameObject, clip.length); }
        
    }

    public void playSoundRandPitch(AudioClip clip, Transform spawnTransform, float volume, float pitchMin, float pitchMax, bool loop)
    {
        AudioSource audioSource = Instantiate(fxObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = Random.Range(pitchMin, pitchMax);
        audioSource.loop = loop;
        audioSource.outputAudioMixerGroup = sfxGroup;
        audioSource.Play();
        if (!loop) { Destroy(audioSource.gameObject, clip.length); }

    }
}
