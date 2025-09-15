using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    //Sources to output the music from
    public AudioSource MusicSource;
    public AudioSource SfxSource;

    //Clip of the Background music
    public AudioClip BackgroundMusic;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        PlayMusic(BackgroundMusic,0.3f);
    }

    public void PlayMusic(AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
    {
        MusicSource.loop = true;
        MusicSource.volume = volume;
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    public void PlaySFXOneShot(AudioClip clip, float volume = 1.0f)
    {
        SfxSource.clip = clip;
        SfxSource.volume = volume;

        SfxSource.PlayOneShot(clip, volume);
    }

    public void PlaySFXCustom(AudioClip clip, float volume = 1.0f, float randomPitch = 1.0f)
    {
        if (clip == null) return;

        if (randomPitch == 0.0f) return;

        GameObject sfxObj = new GameObject("SFX");

        AudioSource sfxAudioSource = sfxObj.AddComponent<AudioSource>();
        sfxAudioSource.clip = clip;
        sfxAudioSource.volume = volume;
        sfxAudioSource.pitch = randomPitch;

        randomPitch = Random.Range(1.0f, 3.0f);

        if (randomPitch < 0.0f)
            sfxAudioSource.time = sfxAudioSource.clip.length - 0.01f; 

        sfxAudioSource.Play();

        float adjustedDuration = clip.length / Mathf.Abs(randomPitch);
        Destroy(sfxObj, adjustedDuration);
    } // Play footsteps


}
