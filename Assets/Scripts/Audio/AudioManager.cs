using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    //Sources to output the music from
    public AudioSource MusicSource;
    public AudioSource SfxSource;

    //Slider
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    //Clip of the Background music
    public AudioClip BackgroundMusic;

    //Audio Panel
    [SerializeField] private GameObject _panel;
    
    public bool StopFootSteps = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _panel.SetActive(false);
        if(_masterSlider.value == 0) { _masterSlider.value = 0.5f; }
        if(_musicSlider.value == 0) { _musicSlider.value = 0.5f; }
        if(_sfxSlider.value == 0) { _sfxSlider.value = 0.5f; }
        
        PlayMusic(BackgroundMusic);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ToggleAudioPanel();
        }
    }

    public void ToggleAudioPanel()
    {
        if (_panel.gameObject.activeSelf)
        {
            _panel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            _panel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ToggleAudioPanelInMainMenu()
    {
        if (_panel.gameObject.activeSelf)
        {
            _panel.SetActive(false);
            
        }
        else
        {
            _panel.SetActive(true);
            
        }
    }
    public void ChangeMasterVolume()
    {
        MusicSource.volume = _musicSlider.value * _masterSlider.value;
        SfxSource.volume = _sfxSlider.value * _masterSlider.value;
    }

    public void ChangeMusicVolume()
    {
        MusicSource.volume = _musicSlider.value * _masterSlider.value;
    }

    public void ChangeSfxVolume()
    {
        SfxSource.volume = _sfxSlider.value * _masterSlider.value;
    }
    public void PlayMusic(AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
    {
        MusicSource.loop = true;
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    public void PlaySFXOneShot(AudioClip clip, float volume = 1.0f)
    {
        SfxSource.clip = clip;

        SfxSource.PlayOneShot(clip, volume);
    }

    public void PlaySFXCustom(AudioClip clip, float volume = 1.0f, float randomPitch = 1.0f)
    {
        if (clip == null) return;
        if (StopFootSteps) return;
        if (randomPitch == 0.0f) return;
        if (!Application.isPlaying) return;
        GameObject sfxObj = new GameObject("SFX");

        AudioSource sfxAudioSource = sfxObj.AddComponent<AudioSource>();
        sfxAudioSource.clip = clip;
        sfxAudioSource.volume = SfxSource.volume;
        sfxAudioSource.pitch = randomPitch;

        randomPitch = Random.Range(1.0f, 3.0f);

        if (randomPitch < 0.0f)
            sfxAudioSource.time = sfxAudioSource.clip.length - 0.01f; 

        sfxAudioSource.Play();

        float adjustedDuration = clip.length / Mathf.Abs(randomPitch);
        Destroy(sfxObj, adjustedDuration);
    } // Play footsteps


}
