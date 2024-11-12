using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Playables;

public class GraphicsMenu : MonoBehaviour
{
   [SerializeField] UIDocument graphicsMenuDocument;
   [SerializeField] UIDocument settingsMenuDocument;

   private Button backButton;
   private DropdownField frameRateDropdown;
   private DropdownField resolutionDropdown;
   private DropdownField windowModeDropdown;
   private Toggle vsyncToggle;
   
   private List<int> commonframeRates = new List<int>() { 60, 120, 144, 165, 240, 300 };
   private Resolution[] resolutions;

   private void Awake()
   {
      VisualElement root = graphicsMenuDocument.rootVisualElement;

      backButton = root.Q<Button>("BackButton");
      frameRateDropdown = root.Q<DropdownField>("framerateDropdown");
      resolutionDropdown = root.Q<DropdownField>("resolutionDropdown");
      windowModeDropdown = root.Q<DropdownField>("screenModeDropdown");
      vsyncToggle = root.Q<Toggle>("vsyncToggle");

      backButton.clickable.clicked += () => ExitMenu();

      PopulateFrameRateOptions();
      PopulateResolutionOptions();
      PopulateWindowModeOptions();

      frameRateDropdown.RegisterValueChangedCallback(evt => SetFrameRate(int.Parse(evt.newValue)));
      resolutionDropdown.RegisterValueChangedCallback(evt => SetResolution(evt.newValue));
      windowModeDropdown.RegisterValueChangedCallback(evt => SetWindowMode(evt.newValue));
      vsyncToggle.RegisterValueChangedCallback(evt => SetVSync(evt.newValue));

    }

    private void Start()
    {
        LoadWindowMode();
        LoadResolution();
        LoadFrameRate();

        //windowModeDropdown.value = PlayerPrefs.GetInt("WindowMode", (int)FullScreenMode.FullScreenWindow).ToString();
        //resolutionDropdown.value = $"{PlayerPrefs.GetInt("ResolutionWidth", Screen.currentResolution.width)}x{PlayerPrefs.GetInt("ResolutionHeight", Screen.currentResolution.height)}";
        //frameRateDropdown.value = PlayerPrefs.GetInt("FrameRate", (int)Mathf.Ceil((float)Screen.currentResolution.refreshRateRatio.numerator /                                                                  (float)Screen.currentResolution.refreshRateRatio.denominator)).ToString();
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
        int maxRefreshRate = (int)Mathf.Ceil((float)Screen.currentResolution.refreshRateRatio.numerator / (float) Screen.currentResolution.refreshRateRatio.denominator);
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
        frameRateDropdown.value = maxRefreshRate.ToString(); // Default value of the drop down is the max refresh rate of the current display
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
        resolutionDropdown.value = $"{Screen.currentResolution.width} x {Screen.currentResolution.height}";
    }

    private void PopulateWindowModeOptions()
    {
        List<string> windowModeOptions = new List<string> { "Fullscreen", "Windowed", "Borderless" };
        windowModeDropdown.choices = windowModeOptions;
        windowModeDropdown.value = "Fullscreen";
    }

    private void LoadWindowMode()
    {
        // If PlayerPrefs doesn't have the setting, set it to Fullscreen
        if (!PlayerPrefs.HasKey("WindowMode"))
        {
            PlayerPrefs.SetInt("WindowMode", (int)FullScreenMode.FullScreenWindow);
            PlayerPrefs.Save();
        }

        int mode = PlayerPrefs.GetInt("WindowMode");
        Screen.fullScreenMode = (FullScreenMode)mode;
    }

    private void LoadResolution()
    {
        // If PlayerPrefs doesn't have the resolution settings, set them to the current screen resolution
        if (!PlayerPrefs.HasKey("ResolutionWidth") || !PlayerPrefs.HasKey("ResolutionHeight"))
        {
            PlayerPrefs.SetInt("ResolutionWidth", Screen.currentResolution.width);
            PlayerPrefs.SetInt("ResolutionHeight", Screen.currentResolution.height);
            PlayerPrefs.Save();
        }

        int width = PlayerPrefs.GetInt("ResolutionWidth");
        int height = PlayerPrefs.GetInt("ResolutionHeight");
        Screen.SetResolution(width, height, Screen.fullScreenMode);
    }

    private void LoadFrameRate()
    {
        // If PlayerPrefs doesn't have the frame rate setting, set it to the maximum refresh rate of the current display
        if (!PlayerPrefs.HasKey("FrameRate"))
        {
            int maxRefreshRate = (int)Mathf.Ceil((float)Screen.currentResolution.refreshRateRatio.numerator /
                                                  (float)Screen.currentResolution.refreshRateRatio.denominator);
            PlayerPrefs.SetInt("FrameRate", maxRefreshRate);
            PlayerPrefs.Save();
        }

        int frameRate = PlayerPrefs.GetInt("FrameRate");
        Application.targetFrameRate = frameRate;
    }

    private void SetFrameRate(int frameRate)
    {
        Application.targetFrameRate = frameRate;
        PlayerPrefs.SetInt("FrameRate", frameRate);
        PlayerPrefs.Save();
    }

    private void SetResolution(string resolution)
    {
        foreach (Resolution res in resolutions)
        {
            string option = $"{res.width} x {res.height}";
            if (option == resolution)
            {
                Screen.SetResolution(res.width, res.height, Screen.fullScreenMode, res.refreshRateRatio);
                PlayerPrefs.SetInt("ResolutionWidth", Screen.currentResolution.width);
                PlayerPrefs.SetInt("ResolutionHeight", Screen.currentResolution.height);
                PlayerPrefs.Save();
                break;
            }
        }
    }

    private void SetWindowMode(string mode)
    {
        if (mode == "Fullscreen")
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else if (mode == "Windowed")
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
        }
    }

    private void SetVSync(bool isEnabled)
    {
        QualitySettings.vSyncCount = isEnabled ? 1 : 0;
    }
    private void ExitMenu()
   {
      graphicsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
      settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
   }

}
