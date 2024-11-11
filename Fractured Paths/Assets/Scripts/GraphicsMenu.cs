using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class GraphicsMenu : MonoBehaviour
{
   [SerializeField] UIDocument graphicsMenuDocument;
   [SerializeField] UIDocument settingsMenuDocument;

   private Button backButton;
   private DropdownField frameRateDropdown;
   private DropdownField resolutionDropdown;
   private DropdownField windowModeDropdown;
   private Toggle vsyncToggle;
   
   private List<int> frameRates = new List<int>() { 60, 120, 144, 165, 240, 300 };
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainMenuScene")
        {
            graphicsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        }
    }
    private void PopulateFrameRateOptions()
    {
        int maxRefreshRate = (int)Screen.currentResolution.refreshRateRatio.numerator;
        List<string> frameRateOptions = new List<string>();

        foreach (int frameRate in frameRates)
        {
            if (frameRate >= 60 && frameRate <= maxRefreshRate)
            {
                frameRateOptions.Add(frameRate.ToString());
            }
        }

        if (!frameRateOptions.Contains(maxRefreshRate.ToString()))
        {
            frameRateOptions.Add(maxRefreshRate.ToString());
        }

        frameRateDropdown.choices = frameRateOptions;
        frameRateDropdown.value = Application.targetFrameRate == -1 ? "60" : Application.targetFrameRate.ToString();
    }

    private void PopulateResolutionOptions()
    {
        resolutions = Screen.resolutions;
        List<string> resolutionOptions = new List<string>();

        foreach (Resolution res in resolutions)
        {
            if (res.refreshRateRatio.numerator >= 60)
            {
                string option = $"{res.width} x {res.height}";
                resolutionOptions.Add(option);
            }
        }

        resolutionDropdown.choices = resolutionOptions;
        resolutionDropdown.value = $"{Screen.currentResolution.width} x {Screen.currentResolution.height} @ {Screen.currentResolution.refreshRateRatio.numerator}Hz";
    }

    private void PopulateWindowModeOptions()
    {
        List<string> windowModeOptions = new List<string> { "Fullscreen", "Windowed", "Borderless" };
        windowModeDropdown.choices = windowModeOptions;

        if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
        {
            windowModeDropdown.value = "Fullscreen";
        }
        else if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            windowModeDropdown.value = "Windowed";
        }
        else
        {
            windowModeDropdown.value = "Borderless";
        }
    }

    private void SetFrameRate(int frameRate)
    {
        Application.targetFrameRate = frameRate;
        Debug.Log($"Frame rate set to: {frameRate}");
    }

    private void SetResolution(string resolution)
    {
        foreach (Resolution res in resolutions)
        {
            string option = $"{res.width} x {res.height} @ {res.refreshRateRatio.numerator}Hz";
            if (option == resolution)
            {
                Screen.SetResolution(res.width, res.height, Screen.fullScreenMode, res.refreshRateRatio);
                Debug.Log($"Resolution set to: {option}");
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

        Debug.Log($"Window mode set to: {mode}");
    }

    private void SetVSync(bool isEnabled)
    {
        QualitySettings.vSyncCount = isEnabled ? 1 : 0;
        Debug.Log($"VSync set to: {(isEnabled ? "Enabled" : "Disabled")}");
    }
    private void ExitMenu()
   {
      graphicsMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
      settingsMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
   }

}
