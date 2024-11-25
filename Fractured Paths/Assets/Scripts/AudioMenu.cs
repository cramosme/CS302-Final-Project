using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AudioMenu : MonoBehaviour
{
    [SerializeField] UIDocument audioMenuDocument; // UI Document for the audio menu
    [SerializeField] UIDocument settingsMenuDocument; // UI Document for the settings menu

    private Button backButton; // Button to return to the settings menu

    private void Awake()
    {
        // Get the root visual element of the audio menu UI
        VisualElement root = audioMenuDocument.rootVisualElement;

        // Find the back button in the UI and set its click action
        backButton = root.Q<Button>("BackButton");
        backButton.clickable.clicked += () => ExitMenu();
    }

    private void Update()
    {
        // Close the audio menu when Escape is pressed during gameplay
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainMenuScene")
        {
            audioMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        }
    }

    private void ExitMenu()
    {
        // Hide the audio menu and show the settings menu
        audioMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
    }
}
