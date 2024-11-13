using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Security.Cryptography;

public class GraphicsMenu : MonoBehaviour
{
   [SerializeField] UIDocument graphicsMenuDocument;
   [SerializeField] UIDocument settingsMenuDocument;

   private Button backButton;
   private DropdownField frameRateDropdown;
   private DropdownField resolutionDropdown;
   private DropdownField windowModeDropdown;
   
   private List<int> commonframeRates = new List<int>() { 60, 120, 144, 165, 240, 300 };
   private Resolution[] resolutions;

    private void Awake()
   {
      VisualElement root = graphicsMenuDocument.rootVisualElement;

      backButton = root.Q<Button>("BackButton");
      frameRateDropdown = root.Q<DropdownField>("framerateDropdown");
      resolutionDropdown = root.Q<DropdownField>("resolutionDropdown");
      windowModeDropdown = root.Q<DropdownField>("screenModeDropdown");

      backButton.clickable.clicked += () => ExitMenu();

      PopulateFrameRateOptions();
      PopulateResolutionOptions();
      PopulateWindowModeOptions();

      windowModeDropdown.RegisterValueChangedCallback(evt => SetWindowMode(evt.newValue));
      frameRateDropdown.RegisterValueChangedCallback(evt => SetFrameRate(int.Parse(evt.newValue)));
      resolutionDropdown.RegisterValueChangedCallback(evt => SetResolution(evt.newValue));

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainMenuScene")
        {
            graphicsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        }
    }
    private void PopulateFrameRateOptions()
    {
        // Gets the max refresh rate of the current display
        int maxRefreshRate = (int)Mathf.Ceil((float)Screen.currentResolution.refreshRateRatio.numerator / (float)Screen.currentResolution.refreshRateRatio.denominator);

        List<string> frameRateOptions = new List<string>(); // These are the frame rates that will get added to the drop down menu

        foreach (int frameRate in commonframeRates) // loops through the predefined list with common frame rates
        {
            // if the current frameRate in the predetermined list is less than the max refresh rate than include it as an option
            if (frameRate <= maxRefreshRate)
            {
                frameRateOptions.Add(frameRate.ToString());
            }
        }
        // If the current frameRate is not included in the list (might not be common) then add it to the list
        if (!frameRateOptions.Contains(maxRefreshRate.ToString()))
        {
            frameRateOptions.Add(maxRefreshRate.ToString());
        }

        frameRateDropdown.choices = frameRateOptions; // this will actually populate the drop down values
        if (!PlayerPrefs.HasKey("FrameRate"))
        {
            frameRateDropdown.value = maxRefreshRate.ToString(); // Default value of the drop down is the max refresh rate of the current display
            PlayerPrefs.SetInt("FrameRate", maxRefreshRate);
        }
        else
        {
            frameRateDropdown.value = PlayerPrefs.GetInt("FrameRate").ToString();
        }
        
    }

    private void PopulateResolutionOptions()
    {

        resolutions = Screen.resolutions; // Fills the Resolution array with all resolutions the current display supports
        List<string> resolutionOptions = new List<string>(); // creates a new list of options to be displayed

        foreach (Resolution res in resolutions)
        {
            if (res.refreshRateRatio.numerator >= 60)
            {
                string option = $"{res.width} x {res.height}";
                resolutionOptions.Add(option);
            }
        }

        resolutionDropdown.choices = resolutionOptions;
        if (!PlayerPrefs.HasKey("Resolution"))
        {
            resolutionDropdown.value = $"{Screen.currentResolution.width} x {Screen.currentResolution.height}";
            PlayerPrefs.SetString("Resolution", $"{Screen.currentResolution.width} x {Screen.currentResolution.height}");
        }
        else
        {
            resolutionDropdown.value = PlayerPrefs.GetString("Resolution");
        }
    }

    private void PopulateWindowModeOptions()
    {
        List<string> windowModeOptions = new List<string> { "Fullscreen", "Windowed", "Borderless" };
        windowModeDropdown.choices = windowModeOptions;
        if (!PlayerPrefs.HasKey("WindowMode"))
        {
            windowModeDropdown.value = "Fullscreen";
            PlayerPrefs.SetInt("WindowMode", (int)FullScreenMode.ExclusiveFullScreen);
        }
        else
        {
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
        Application.targetFrameRate = frameRate;
        PlayerPrefs.SetInt("FrameRate", frameRate);
    }

    private void SetResolution(string resolution)
    {
        foreach (Resolution res in resolutions)
        {
            string option = $"{res.width} x {res.height}";
            if (option == resolution)
            {
                if (!PlayerPrefs.HasKey("WindowMode"))
                {
                    Screen.SetResolution(res.width, res.height, FullScreenMode.ExclusiveFullScreen, res.refreshRateRatio);
                }
                else
                {
                    if (PlayerPrefs.GetInt("WindowMode") == (int)FullScreenMode.ExclusiveFullScreen)
                    {
                        Screen.SetResolution(res.width, res.height, FullScreenMode.ExclusiveFullScreen, res.refreshRateRatio);
                    }
                    else if (PlayerPrefs.GetInt("WindowMode") == (int)FullScreenMode.Windowed)
                    {
                        Screen.SetResolution(res.width, res.height, FullScreenMode.Windowed, res.refreshRateRatio);
                    }
                    else
                    {
                        Screen.SetResolution(res.width, res.height, FullScreenMode.FullScreenWindow, res.refreshRateRatio);
                    }
                }
                PlayerPrefs.SetString("Resolution", $"{res.width} x {res.height}");
                break;
            }
        }
    }

    private void SetWindowMode(string mode)
    {
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
      graphicsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
      settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
   }

}
