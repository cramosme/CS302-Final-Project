using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

   [SerializeField] UIDocument mainMenuDocument;
   [SerializeField] UIDocument settingsMenuDocument;
   [SerializeField] UIDocument keyBindMenuDocument;
   [SerializeField] UIDocument graphicsMenuDocument;
   [SerializeField] UIDocument audioMenuDocument;

   private Button playButton;
   private Button settingsButton;
   private Button exitButton;

   private void Awake()
   {
      VisualElement mainRoot = mainMenuDocument.rootVisualElement;

      playButton = mainRoot.Q<Button>("PlayButton");
      settingsButton = mainRoot.Q<Button>("SettingsButton");
      exitButton = mainRoot.Q<Button>("ExitButton");

      playButton.clickable.clicked += () => PlayGame();
      settingsButton.clickable.clicked += () => ShowSettingsMenu();
      exitButton.clickable.clicked += () => ExitGame();

      settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
      keyBindMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
      graphicsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
      audioMenuDocument.rootVisualElement.style.display = DisplayStyle.None;

   }

    private void PlayGame()
   {
      SceneManager.LoadScene("GameScene");
   }

   private void ShowSettingsMenu()
   {
      mainMenuDocument.rootVisualElement.style.display = DisplayStyle.None; // Hide the main menu
      settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex; // Show the settings menu
   }

   private void ExitGame()
   {
      Application.Quit();
   }
}
