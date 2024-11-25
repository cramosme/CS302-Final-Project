using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // References to various UI Documents for different menus (Pause, Settings, Key Bind, Graphics, Audio)
    [SerializeField] UIDocument pauseMenuDocument;
    [SerializeField] UIDocument settingsMenuDocument;
    [SerializeField] UIDocument keyBindMenuDocument;
    [SerializeField] UIDocument graphicsMenuDocument;
    [SerializeField] UIDocument audioMenuDocument;

    // Buttons for actions in the pause menu
    private Button resumeButton;
    private Button settingsButton;
    private Button mainMenuButton;

    // Boolean to track if the game is paused
    public bool isPaused;

    // Singleton instance for the PauseMenu
    public static PauseMenu instance;

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize the paused state as false (game is not paused initially)
        isPaused = false;

        // Get references to the buttons in the pause menu UI
        VisualElement mainRoot = pauseMenuDocument.rootVisualElement;
        resumeButton = mainRoot.Q<Button>("ResumeButton");
        settingsButton = mainRoot.Q<Button>("SettingsButton");
        mainMenuButton = mainRoot.Q<Button>("ExitToMainButton");

        // Assign click events to the buttons
        resumeButton.clickable.clicked += ResumeGame;
        settingsButton.clickable.clicked += ShowSettingsMenu;
        mainMenuButton.clickable.clicked += ShowMainMenu;

        // Initially hide all menus (Pause, Settings, Key Bind, Graphics, Audio)
        pauseMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        keyBindMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        graphicsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        audioMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    // Update is called once per frame
    private void Update()
    {
        // Toggle the pause state when the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Resumes the game, hides the pause menu, and restores the time scale
    private void ResumeGame()
    {
        isPaused = false; // Set the game as unpaused
        Time.timeScale = 1; // Resume game time
        UnityEngine.Cursor.visible = false; // Hide the cursor
        UnityEngine.Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        pauseMenuDocument.rootVisualElement.style.display = DisplayStyle.None; // Hide the pause menu

        // Resume music if the AudioManager instance exists
        if (AudioManager.instance != null)
        {
            AudioManager.instance.ResumeMusic();
        }
    }

    // Pauses the game, shows the pause menu, and stops the game time
    private void PauseGame()
    {
        isPaused = true; // Set the game as paused
        pauseMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex; // Show the pause menu
        Time.timeScale = 0; // Stop game time
        UnityEngine.Cursor.visible = true; // Show the cursor
        UnityEngine.Cursor.lockState = CursorLockMode.None; // Unlock the cursor

        // Pause music if the AudioManager instance exists
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PauseMusic();
        }
    }

    // Switches from the pause menu to the settings menu
    private void ShowSettingsMenu()
    {
        pauseMenuDocument.rootVisualElement.style.display = DisplayStyle.None; // Hide the pause menu
        settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex; // Show the settings menu
    }

    // Loads the main menu scene when the "Exit to Main Menu" button is clicked
    private void ShowMainMenu()
    {
        Time.timeScale = 1; // Ensure game time is resumed when leaving
        SceneManager.LoadScene("MainMenuScene"); // Load the main menu scene
    }
}
