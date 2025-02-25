using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource soundSource;
    public static AudioManager instance;
    public static bool isMusicSounds;

    [Header("Audio Clips")]
    public AudioClip theme;
    public AudioClip poweUpTheme;
    public AudioClip goal;
    public AudioClip goalpost;
    public AudioClip refereeWhistle;
    public AudioClip somosGuapos;
    public AudioClip siuRonaldo;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CambiarVolumen();
    }
    void Update()
    {
    }
    
    public void PlaySound(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }
    public void PlayTheme()
    {
        musicSource.clip = theme;
        musicSource.Play();
    }
    public void StopTheme()
    {
        musicSource.Stop();
    }

    public void PlayEasterEggSomosGuapos()
    {
        soundSource.volume = Mathf.Clamp(1f, 0f, 1f);
        soundSource.PlayOneShot(somosGuapos);
    }

    public void CambiarVolumen()
    {
        float volumen = PlayerPrefs.GetFloat("Volumen", 10);
        soundSource.volume = volumen;
        float musica = PlayerPrefs.GetFloat("Musica", 10);
        musicSource.volume = musica;
    }
}