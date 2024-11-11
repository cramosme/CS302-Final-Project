using UnityEngine;
using UnityEngine.SceneManagement;
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainMenuScene")
        {
            audioMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        }
    }

    private void ExitMenu()
   {
      audioMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
      settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
   }
}