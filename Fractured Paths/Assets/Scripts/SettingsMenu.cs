using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
   [SerializeField] UIDocument settingsMenuDocument;
   [SerializeField] UIDocument mainMenuDocument;
   [SerializeField] UIDocument graphicsMenuDocument;
   [SerializeField] UIDocument audioMenuDocument;
   [SerializeField] UIDocument pauseMenuDocument;

   private Button keyBindButton;
   private Button graphicsButton;
   private Button audioButton;
   private Button backButton;
   PauseMenu pauseMenu;

   private void Awake()
   {
        pauseMenu = FindObjectOfType<PauseMenu>();

        VisualElement root = settingsMenuDocument.rootVisualElement;

      keyBindButton = root.Q<Button>("KeyBindingsButton");
      graphicsButton = root.Q<Button>("GraphicSettingsButton");
      audioButton = root.Q<Button>("AudioSettingsButton");
      backButton = root.Q<Button>("BackButton");

      graphicsButton.clickable.clicked += () => ShowGraphicsMenu();
      audioButton.clickable.clicked += () => ShowAudioMenu();
      backButton.clickable.clicked += () => ExitMenu();

   }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainMenuScene")
        {
            settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        }
    }

   private void ShowGraphicsMenu()
   {
      settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
      graphicsMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
   }

   private void ShowAudioMenu()
   {
      settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
      audioMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
   }
    private void ExitMenu()
    {
        // Check if we are in the main menu scene
        bool isMainMenuScene = SceneManager.GetActiveScene().name == "MainMenuScene";

        if (pauseMenu != null && pauseMenu.isPaused && !isMainMenuScene)
        {
            // In-game scenario
            settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
            pauseMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        }
        else if (isMainMenuScene && mainMenuDocument != null)
        {
            // Main menu scenario
            settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
            mainMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        }
        else
        {
            // Fallback in case mainMenuDocument is null or an unexpected state occurs
            Debug.LogWarning("mainMenuDocument is null or an unexpected scene state.");
            settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        }
    }

}
