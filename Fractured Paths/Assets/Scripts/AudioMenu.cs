using UnityEngine;
using UnityEngine.UIElements;

public class AudioMenu : MonoBehaviour
{
   [SerializeField] UIDocument audioMenuDocument;
   [SerializeField] UIDocument settingsMenuDocument;

   private Button backButton;

   private void Awake()
   {
      VisualElement root = audioMenuDocument.rootVisualElement;

      backButton = root.Q<Button>("BackButton");
      backButton.clickable.clicked += () => ExitMenu();

   }

   private void ExitMenu()
   {
      audioMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
      settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
   }
}