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
   private Button applyButton; // Applies changed settings
   private Button cancelButton; // Cancels changed settings
   private Button resetButton; // Resets settings to default

   private void Awake()
   {
      VisualElement root = keyBindsMenuDocument.rootVisualElement;

      backButton = root.Q<Button>("BackButton");
      applyButton = root.Q<Button>("ApplyButton");
      cancelButton = root.Q<Button>("CancelButton");
      resetButton = root.Q<Button>("ResetButton");

      backButton.clickable.clicked += () => ExitMenu();
      applyButton.clickable.clicked += () => ApplySettings();
      cancelButton.clickable.clicked += () => CancelSettings();
      resetButton.clickable.clicked += () => ResetSettings();

      // settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;

   }

   private void ExitMenu()
   {
      keyBindsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
      settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
   }
   
   private void ApplySettings()
   {
      print("Applying Settings");
   }

   private void CancelSettings()
   {
      print("Cancelling Settings");
   }

   private void ResetSettings()
   {
      print("Resetting Settings");
   }
}
