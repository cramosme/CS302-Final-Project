using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Security.Cryptography;

public class GraphicsMenu : MonoBehaviour
{
    [SerializeField] UIDocument graphicsMenuDocument; // Graphics menu UI document
    [SerializeField] UIDocument settingsMenuDocument; // Settings menu UI document

    private Button backButton; // Back button in the graphics menu
    private DropdownField frameRateDropdown; // Dropdown for frame rate selection
    private DropdownField resolutionDropdown; // Dropdown for resolution selection
    private DropdownField windowModeDropdown; // Dropdown for screen mode selection

    private List<int> commonframeRates = new List<int>() { 60, 120, 144, 165, 240, 300 }; // Common frame rates for displays
    private Resolution[] resolutions; // Array of supported resolutions

    private void Awake()
    {
        // Access root UI elements from the graphics menu
        VisualElement root = graphicsMenuDocument.rootVisualElement;

        // Assign references to UI elements
        backButton = root.Q<Button>("BackButton");
        frameRateDropdown = root.Q<DropdownField>("framerateDropdown");
        resolutionDropdown = root.Q<DropdownField>("resolutionDropdown");
        windowModeDropdown = root.Q<DropdownField>("screenModeDropdown");

        // Assign functionality to the back button
        backButton.clickable.clicked += () => ExitMenu();

        // Populate dropdowns with relevant options
        PopulateFrameRateOptions();
        PopulateResolutionOptions();
        PopulateWindowModeOptions();

        // Register callbacks for dropdown value changes
        windowModeDropdown.RegisterValueChangedCallback(evt => SetWindowMode(evt.newValue));
        frameRateDropdown.RegisterValueChangedCallback(evt => SetFrameRate(int.Parse(evt.newValue)));
        resolutionDropdown.RegisterValueChangedCallback(evt => SetResolution(evt.newValue));
    }

    private void Update()
    {
        // Hide graphics menu when Escape is pressed (if not in main menu)
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainMenuScene")
        {
            graphicsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        }
    }

    private void PopulateFrameRateOptions()
    {
        // Get the maximum refresh rate of the current display
        int maxRefreshRate = (int)Mathf.Ceil((float)Screen.currentResolution.refreshRateRatio.numerator / Screen.currentResolution.refreshRateRatio.denominator);

        List<string> frameRateOptions = new List<string>(); // List to hold frame rate options for the dropdown

        // Add frame rates from the predefined list if they are below or equal to the max refresh rate
        foreach (int frameRate in commonframeRates)
        {
            if (frameRate <= maxRefreshRate)
            {
                frameRateOptions.Add(frameRate.ToString());
            }
        }

        // Include the max refresh rate if it's not already in the list
        if (!frameRateOptions.Contains(maxRefreshRate.ToString()))
        {
            frameRateOptions.Add(maxRefreshRate.ToString());
        }

        // Populate the dropdown and set the default value
        frameRateDropdown.choices = frameRateOptions;
        if (!PlayerPrefs.HasKey("FrameRate")) // Check if frame rate is stored in PlayerPrefs
        {
            frameRateDropdown.value = maxRefreshRate.ToString(); // Default to max refresh rate
            PlayerPrefs.SetInt("FrameRate", maxRefreshRate);
        }
        else
        {
            frameRateDropdown.value = PlayerPrefs.GetInt("FrameRate").ToString(); // Use saved frame rate
        }
    }

    private void PopulateResolutionOptions()
    {
        resolutions = Screen.resolutions; // Fetch all supported resolutions
        List<string> resolutionOptions = new List<string>(); // List for resolution options

        // Add resolutions with a minimum refresh rate of 60 Hz
        foreach (Resolution res in resolutions)
        {
            if (res.refreshRateRatio.numerator >= 60)
            {
                string option = $"{res.width} x {res.height}"; // Format resolution as "Width x Height"
                resolutionOptions.Add(option);
            }
        }

        // Populate the dropdown and set the default value
        resolutionDropdown.choices = resolutionOptions;
        if (!PlayerPrefs.HasKey("Resolution")) // Check if resolution is stored in PlayerPrefs
        {
            resolutionDropdown.value = $"{Screen.currentResolution.width} x {Screen.currentResolution.height}"; // Default to current resolution
            PlayerPrefs.SetString("Resolution", $"{Screen.currentResolution.width} x {Screen.currentResolution.height}");
        }
        else
        {
            resolutionDropdown.value = PlayerPrefs.GetString("Resolution"); // Use saved resolution
        }
    }

    private void PopulateWindowModeOptions()
    {
        // Define the screen mode options
        List<string> windowModeOptions = new List<string> { "Fullscreen", "Windowed", "Borderless" };
        windowModeDropdown.choices = windowModeOptions;

        // Set the default or load the saved screen mode
        if (!PlayerPrefs.HasKey("WindowMode"))
        {
            windowModeDropdown.value = "Fullscreen"; // Default to fullscreen mode
            PlayerPrefs.SetInt("WindowMode", (int)FullScreenMode.ExclusiveFullScreen);
        }
        else
        {
            // Load and set the saved screen mode
            if (PlayerPrefs.GetInt("WindowMode") == (int)FullScreenMode.ExclusiveFullScreen)
            {
                windowModeDropdown.value = "Fullscreen";
            }
            else if (PlayerPrefs.GetInt("WindowMode") == (int)FullScreenMode.Windowed)
            {
                windowModeDropdown.value = "Windowed";
            }
            else
            {
                windowModeDropdown.value = "Borderless";
            }
        }
    }

    private void SetFrameRate(int frameRate)
    {
        // Apply the selected frame rate and save it in PlayerPrefs
        Application.targetFrameRate = frameRate;
        PlayerPrefs.SetInt("FrameRate", frameRate);
    }

    private void SetResolution(string resolution)
    {
        // Loop through supported resolutions to match the selected option
        foreach (Resolution res in resolutions)
        {
            string option = $"{res.width} x {res.height}";
            if (option == resolution)
            {
                // Apply resolution and screen mode settings
                FullScreenMode mode = (FullScreenMode)PlayerPrefs.GetInt("WindowMode", (int)FullScreenMode.ExclusiveFullScreen);
                Screen.SetResolution(res.width, res.height, mode, res.refreshRateRatio);
                PlayerPrefs.SetString("Resolution", option); // Save resolution
                break;
            }
        }
    }

    private void SetWindowMode(string mode)
    {
        // Map the selected mode to FullScreenMode and apply it
        if (mode == "Fullscreen")
        {
            PlayerPrefs.SetInt("WindowMode", (int)FullScreenMode.ExclusiveFullScreen);
            Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.ExclusiveFullScreen);
        }
        else if (mode == "Windowed")
        {
            PlayerPrefs.SetInt("WindowMode", (int)FullScreenMode.Windowed);
            Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.Windowed);
        }
        else
        {
            PlayerPrefs.SetInt("WindowMode", (int)FullScreenMode.FullScreenWindow);
            Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.FullScreenWindow);
        }
    }

    private void ExitMenu()
    {
        // Hide graphics menu and display settings menu
        graphicsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
    }
}
