using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] UIDocument pauseMenuDocument;
    [SerializeField] UIDocument settingsMenuDocument;
    [SerializeField] UIDocument keyBindMenuDocument;
    [SerializeField] UIDocument graphicsMenuDocument;
    [SerializeField] UIDocument audioMenuDocument;

    private Button resumeButton;
    private Button settingsButton;
    private Button mainMenuButton;

    public bool isPaused;

    private void Start()
    {
        isPaused = false;

        VisualElement mainRoot = pauseMenuDocument.rootVisualElement;
        resumeButton = mainRoot.Q<Button>("ResumeButton");
        settingsButton = mainRoot.Q<Button>("SettingsButton");
        mainMenuButton = mainRoot.Q<Button>("ExitToMainButton");

        resumeButton.clickable.clicked += ResumeGame;
        settingsButton.clickable.clicked += ShowSettingsMenu;
        mainMenuButton.clickable.clicked += ShowMainMenu;

        pauseMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        keyBindMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        graphicsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        audioMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void Update()
    {
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

    private void ResumeGame()
    {
        isPaused = false;
        pauseMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        Time.timeScale = 1;
    }

    private void PauseGame()
    {
        isPaused = true;
        pauseMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        Time.timeScale = 0;
    }

    private void ShowSettingsMenu()
    {
        pauseMenuDocument.rootVisualElement.style.display = DisplayStyle.None; // Hide the pause menu
        settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex; // Show the settings menu
    }

    private void ShowMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }
}