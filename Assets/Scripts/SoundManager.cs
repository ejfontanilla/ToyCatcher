using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip daycareMusic;
    public AudioClip playgroundMusic;
    public AudioClip catchSound;
    public AudioClip gameOverSound;
    public AudioClip buttonClickSound;
    public AudioClip hitSound;
    public AudioClip laneSwitchSound;
    public AudioClip grumpyEnterSound;
    public AudioClip toySpawnSound;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "MainMenu":
            case "StageSelect":
                PlayMusic(backgroundMusic); 
                break;

            case "DayCareStage":
                PlayMusic(daycareMusic);
                break;

            case "PlaygroundStage":
                PlayMusic(playgroundMusic);
                break;
        }
    }


    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }


    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;

        sfxSource.pitch = Random.Range(0.95f, 1.05f);
        sfxSource.PlayOneShot(clip, volume);
        sfxSource.pitch = 1f;
    }

}
