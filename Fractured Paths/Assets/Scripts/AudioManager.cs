using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    // References to audio sources for background music and sound effects
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource puzzleSFXSource;

    // Background music clips for main menu and gameplay scenes
    public AudioClip mainMenuBGM;
    public AudioClip gameBGM;

    // Sound effects for puzzle win/lose scenarios
    public AudioClip winSFX;
    public AudioClip loseSFX;

    // Singleton instance of AudioManager
    public static AudioManager instance;

    private void Awake()
    {
        // Ensure a single instance of AudioManager persists across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevent destruction on scene load
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        // Set initial background music based on the scene
        SetBGMForScene();
        musicSource.Play();

        // Add a delay for non-main menu scenes
        if (!IsInMainMenu())
        {
            StartCoroutine(AddDelay());
        }

        // Subscribe to sceneLoaded event to detect when a new scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private IEnumerator AddDelay()
    {
        // Wait for 1 second before playing music (useful for smooth transitions)
        yield return new WaitForSeconds(1f);
        musicSource.Play();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Update the background music when a new scene is loaded
        SetBGMForScene();

        // Add delay for non-main menu scenes
        if (!IsInMainMenu())
        {
            StartCoroutine(AddDelay());
        }
    }

    private void SetBGMForScene()
    {
        // Set the appropriate background music based on the active scene
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
        // Check if the current scene is the main menu
        return SceneManager.GetActiveScene().name == "MainMenuScene";
    }

    public void PauseMusic()
    {
        // Pause the currently playing background music
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        // Resume the paused background music
        musicSource.UnPause();
    }

    public void PlaySound(AudioClip clip)
    {
        // Stop currently playing sound effect (if any)
        if (puzzleSFXSource.isPlaying)
        {
            puzzleSFXSource.Stop();
        }

        // Play the specified sound effect
        puzzleSFXSource.clip = clip;
        puzzleSFXSource.Play();
    }
}
