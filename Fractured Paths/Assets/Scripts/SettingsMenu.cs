using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
   [SerializeField] UIDocument settingsMenuDocument;
   [SerializeField] UIDocument mainMenuDocument;
   [SerializeField] UIDocument keyBindsMenuDocument;
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

      keyBindButton.clickable.clicked += () => ShowKeyBindingMenu();
      graphicsButton.clickable.clicked += () => ShowGraphicsMenu();
      audioButton.clickable.clicked += () => ShowAudioMenu();
      backButton.clickable.clicked += () => ExitMenu();

   }

   private void ShowKeyBindingMenu()
   {
      settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
      keyBindsMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
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
      if( pauseMenu != null && pauseMenu.isPaused)
        {
            settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
            pauseMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;

        }
        else
        {
            settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
            mainMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        }
    }
}
