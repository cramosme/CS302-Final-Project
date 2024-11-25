using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] UIDocument settingsMenuDocument; // UI Document for the settings menu
    [SerializeField] UIDocument mainMenuDocument; // UI Document for the main menu
    [SerializeField] UIDocument graphicsMenuDocument; // UI Document for the graphics menu
    [SerializeField] UIDocument audioMenuDocument; // UI Document for the audio menu
    [SerializeField] UIDocument pauseMenuDocument; // UI Document for the pause menu

    private Button keyBindButton; // Button for key bindings
    private Button graphicsButton; // Button for graphics settings
    private Button audioButton; // Button for audio settings
    private Button backButton; // Button to return to the previous menu
    PauseMenu pauseMenu; // Reference to the PauseMenu script

    private void Awake()
    {
        pauseMenu = FindObjectOfType<PauseMenu>(); // Locate the PauseMenu instance

        VisualElement root = settingsMenuDocument.rootVisualElement; // Get root of settings menu UI

        // Assign buttons in the settings menu
        keyBindButton = root.Q<Button>("KeyBindingsButton");
        graphicsButton = root.Q<Button>("GraphicSettingsButton");
        audioButton = root.Q<Button>("AudioSettingsButton");
        backButton = root.Q<Button>("BackButton");

        // Set button click actions
        graphicsButton.clickable.clicked += () => ShowGraphicsMenu();
        audioButton.clickable.clicked += () => ShowAudioMenu();
        backButton.clickable.clicked += () => ExitMenu();
    }

    private void Update()
    {
        // Hide settings menu when pressing Escape during gameplay
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainMenuScene")
        {
            settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        }
    }

    private void ShowGraphicsMenu()
    {
        // Switch from settings menu to graphics menu
        settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        graphicsMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void ShowAudioMenu()
    {
        // Switch from settings menu to audio menu
        settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        audioMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void ExitMenu()
    {
        // Determine if we are in the main menu or in-game
        bool isMainMenuScene = SceneManager.GetActiveScene().name == "MainMenuScene";

        if (pauseMenu != null && pauseMenu.isPaused && !isMainMenuScene)
        {
            // In-game: Return to pause menu
            settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
            pauseMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        }
        else if (isMainMenuScene && mainMenuDocument != null)
        {
            // Main menu: Return to the main menu
            settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
            mainMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        }
        else
        {
            // Handle unexpected states
            Debug.LogWarning("mainMenuDocument is null or an unexpected scene state.");
            settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        }
    }
}
