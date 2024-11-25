using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] UIDocument mainMenuDocument; // UI Document for the main menu
    [SerializeField] UIDocument settingsMenuDocument; // UI Document for the settings menu
    [SerializeField] UIDocument graphicsMenuDocument; // UI Document for the graphics menu
    [SerializeField] UIDocument audioMenuDocument; // UI Document for the audio menu

    private Button playButton; // Button to start the game
    private Button settingsButton; // Button to open the settings menu
    private Button exitButton; // Button to exit the game

    public static MainMenu instance; // Singleton instance of MainMenu

    private void Awake()
    {
        // Access root UI elements of the main menu
        VisualElement mainRoot = mainMenuDocument.rootVisualElement;

        // Assign references to buttons in the main menu
        playButton = mainRoot.Q<Button>("PlayButton");
        settingsButton = mainRoot.Q<Button>("SettingsButton");
        exitButton = mainRoot.Q<Button>("ExitButton");

        // Set button click functionalities
        playButton.clickable.clicked += () => PlayGame();
        settingsButton.clickable.clicked += () => ShowSettingsMenu();
        exitButton.clickable.clicked += () => ExitGame();

        // Ensure other menus are hidden initially
        settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        graphicsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        audioMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void PlayGame()
    {
        // Load the game scene
        SceneManager.LoadScene("GameScene");
    }

    private void ShowSettingsMenu()
    {
        // Hide the main menu and display the settings menu
        mainMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void ExitGame()
    {
        // Exit the application
        Application.Quit();
    }
}
