using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GraphicsMenu : MonoBehaviour
{
   [SerializeField] UIDocument graphicsMenuDocument;
   [SerializeField] UIDocument settingsMenuDocument;

   private Button backButton;

   private void Awake()
   {
      VisualElement root = graphicsMenuDocument.rootVisualElement;

      backButton = root.Q<Button>("BackButton");

      backButton.clickable.clicked += () => ExitMenu();

   }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainMenuScene")
        {
            graphicsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        }
    }
    private void ExitMenu()
   {
      graphicsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
      settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
   }

}
