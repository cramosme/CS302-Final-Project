using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public AudioClip mainMenuBGM;
    public AudioClip gameBGM;
    public AudioClip sfx;

    public static AudioManager instance;
    private void Awake()
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


    private void Start()
    {

        SetBGMForScene();
        musicSource.Play();

        if(IsInMainMenu())
        {
            StopSFX();
        }
        else
        {
            StartCoroutine(AddDelay());
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private IEnumerator AddDelay()
    {
        yield return new WaitForSeconds(1f);
        musicSource.Play();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetBGMForScene();
        if (IsInMainMenu())
        {
            StopSFX();
        }
        else
        {
            StartCoroutine(AddDelay());
        }
    }
        private void SetBGMForScene()
    {
        if (IsInMainMenu())
        {
            musicSource.clip = mainMenuBGM;
        }
        else
        {
            musicSource.clip = gameBGM;
        }
    }
    private bool IsInMainMenu()
    {
        return SceneManager.GetActiveScene().name == "MainMenuScene";
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }

    public void PlaySFX()
    {
        sfxSource.Play();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        musicSource.UnPause();
    }
}
