using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Audio;

public class CustomSFXSlider : MonoBehaviour
{
    private VisualElement root; // Root visual element of the UI document
    private VisualElement slider; // Reference to the slider element
    private VisualElement dragger; // Dragger element for the slider
    private VisualElement fillBar; // Custom fill bar for the slider

    // New custom dragger element
    private VisualElement newDragger;

    private Slider mySlider; // The actual slider object

    private Label displayLabel; // Label to display the current slider value

    [SerializeField] private AudioMixer myMixer; // Audio mixer for controlling SFX volume

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement; // Access the root visual element
        slider = root.Q<Slider>("SFXSlider"); // Get the slider by its name
        mySlider = root.Q<Slider>("SFXSlider"); // Initialize the slider
        dragger = mySlider.Q<VisualElement>("unity-dragger"); // Get the slider's dragger

        displayLabel = root.Q<Label>("SFXVolumeDisplay"); // Get the label for displaying the volume

        AddElements(); // Add custom elements to the slider

        // Register a callback for slider value changes
        slider.RegisterCallback<ChangeEvent<float>>(SliderValueChanged);

        // Load saved volume settings or set a default volume
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetSFXVolume();
        }

        // Update the display label with the initial slider value
        UpdateDisplayLabel(mySlider.value);
    }

    // Add custom elements (fill bar and new dragger) to the slider
    void AddElements()
    {
        fillBar = new VisualElement();
        dragger.Add(fillBar); // Add the fill bar to the dragger
        fillBar.name = "sfxBar";
        fillBar.AddToClassList("sfxFillBar"); // Add a CSS class for styling

        newDragger = new VisualElement();
        slider.Add(newDragger); // Add the new dragger to the slider
        newDragger.name = "NewSfxDragger";
        newDragger.AddToClassList("newSfxDragger"); // Add a CSS class for styling
        newDragger.pickingMode = PickingMode.Ignore; // Prevent interaction with the new dragger

        slider.RegisterCallback<GeometryChangedEvent>(SliderInit); // Initialize the slider geometry
    }

    // Callback for when the slider value changes
    void SliderValueChanged(ChangeEvent<float> value)
    {
        int roundedValue = Mathf.RoundToInt(value.newValue); // Round the slider value to the nearest integer
        mySlider.value = roundedValue; // Set the slider value to the rounded integer

        UpdateDisplayLabel(roundedValue); // Update the display label
        CalculateDraggerPosition(); // Recalculate the custom dragger position

        SetSFXVolume(); // Update the audio mixer volume
    }

    // Calculate and set the position of the custom dragger
    private void CalculateDraggerPosition()
    {
        Vector2 offset = new Vector2(
            (newDragger.layout.width - dragger.layout.width) / 2,
            (newDragger.layout.height - dragger.layout.height) / 2
        );
        Vector2 pos = dragger.parent.LocalToWorld(dragger.transform.position);
        newDragger.transform.position = newDragger.parent.WorldToLocal(pos - offset);
    }

    // Initialize the slider geometry
    void SliderInit(GeometryChangedEvent evt)
    {
        Vector2 offset = new Vector2(
            (newDragger.layout.width - dragger.layout.width) / 2,
            (newDragger.layout.height - dragger.layout.height) / 2
        );
        Vector2 pos = dragger.parent.LocalToWorld(dragger.transform.position);
        newDragger.transform.position = newDragger.parent.WorldToLocal(pos - offset);
    }

    // Set the SFX volume in the audio mixer and save it to PlayerPrefs
    private void SetSFXVolume()
    {
        float volume = mySlider.value;
        myMixer.SetFloat("sfx", volume);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    // Load the saved SFX volume from PlayerPrefs
    private void LoadVolume()
    {
        mySlider.value = PlayerPrefs.GetFloat("sfxVolume"); // Load the saved value
        slider.RegisterCallback<ChangeEvent<float>>(SliderValueChanged); // Re-register the value change callback
    }

    // Update the display label with the current volume
    private void UpdateDisplayLabel(float audioValue)
    {
        // Convert the slider value (-80 to 0) to a percentage (100 to 0)
        int displayValue = Mathf.RoundToInt((audioValue + 80) * (100 / 80f));
        displayLabel.text = displayValue.ToString(); // Update the label text
    }
}
