using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class KeyBindMenu : MonoBehaviour
{
   [SerializeField] UIDocument keyBindsMenuDocument;
   [SerializeField] UIDocument settingsMenuDocument;

   private Button backButton;

   private void Awake()
   {
      VisualElement root = keyBindsMenuDocument.rootVisualElement;

      backButton = root.Q<Button>("BackButton");
      backButton.clickable.clicked += () => ExitMenu();
   }

   private void ExitMenu()
   {
      keyBindsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
      settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
   }

}
