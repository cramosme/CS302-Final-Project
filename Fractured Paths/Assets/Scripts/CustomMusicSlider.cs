using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Audio;

public class CustomMusicSlider : MonoBehaviour
{
    // Root VisualElement for the UI Document
    private VisualElement root;
    // VisualElement representing the slider and its components
    private VisualElement slider;
    private VisualElement dragger;
    private VisualElement fillBar;

    // New draggable element added for custom styling
    private VisualElement newDragger;

    // Reference to the actual Slider UI component
    private Slider mySlider;

    // Label to display the current volume as a percentage
    private Label displayLabel;

    // Reference to the AudioMixer for controlling the music volume
    [SerializeField] private AudioMixer myMixer;

    // Start is called before the first frame update
    void Start()
    {
        // Get the root element of the UI Document
        root = GetComponent<UIDocument>().rootVisualElement;

        // Find the slider and its components in the UI
        slider = root.Q<Slider>("MusicSlider");
        mySlider = root.Q<Slider>("MusicSlider");
        dragger = mySlider.Q<VisualElement>("unity-dragger");

        // Find the label for displaying the volume
        displayLabel = root.Q<Label>("MusicVolumeDisplay");

        // Add custom elements to the slider for styling
        AddElements();

        // Register an event callback for when the slider value changes
        slider.RegisterCallback<ChangeEvent<float>>(SliderValueChanged);

        // Load the saved volume if it exists; otherwise, set a default value
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }

        // Update the label to reflect the current slider value
        UpdateDisplayLabel(mySlider.value);
    }

    // Adds custom visual elements to the slider for better visuals
    void AddElements()
    {
        fillBar = new VisualElement();
        dragger.Add(fillBar);
        fillBar.name = "musicBar";
        fillBar.AddToClassList("musicFillBar");

        newDragger = new VisualElement();
        slider.Add(newDragger);
        newDragger.name = "NewMusicDragger";
        newDragger.AddToClassList("newMusicDragger");
        newDragger.pickingMode = PickingMode.Ignore;

        // Ensure the new dragger's position is properly initialized
        slider.RegisterCallback<GeometryChangedEvent>(SliderInit);
    }

    // Called when the slider value changes
    void SliderValueChanged(ChangeEvent<float> value)
    {
        // Round the new slider value to the nearest integer
        int roundedValue = Mathf.RoundToInt(value.newValue);

        // Update the slider's value to the rounded integer
        mySlider.value = roundedValue;

        // Update the label and dragger position
        UpdateDisplayLabel(roundedValue);
        CalculateDraggerPosition();

        // Apply the new music volume
        SetMusicVolume();
    }

    // Calculates the new position for the custom dragger
    private void CalculateDraggerPosition()
    {
        Vector2 offset = new Vector2(
            (newDragger.layout.width - dragger.layout.width) / 2,
            (newDragger.layout.height - dragger.layout.height) / 2
        );
        Vector2 pos = dragger.parent.LocalToWorld(dragger.transform.position);
        newDragger.transform.position = newDragger.parent.WorldToLocal(pos - offset);
    }

    // Ensures the slider and custom dragger are initialized properly
    void SliderInit(GeometryChangedEvent evt)
    {
        Vector2 offset = new Vector2(
            (newDragger.layout.width - dragger.layout.width) / 2,
            (newDragger.layout.height - dragger.layout.height) / 2
        );
        Vector2 pos = dragger.parent.LocalToWorld(dragger.transform.position);
        newDragger.transform.position = newDragger.parent.WorldToLocal(pos - offset);
    }

    // Sets the music volume on the AudioMixer and saves it
    private void SetMusicVolume()
    {
        float volume = mySlider.value;
        myMixer.SetFloat("music", volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    // Loads the saved music volume and applies it to the slider
    private void LoadVolume()
    {
        mySlider.value = PlayerPrefs.GetFloat("musicVolume");
        slider.RegisterCallback<ChangeEvent<float>>(SliderValueChanged);
    }

    // Updates the label to display the volume as a percentage (100 to 0)
    private void UpdateDisplayLabel(float audioValue)
    {
        // Convert audio value (-80 to 0) to a range of 100 to 0
        int displayValue = Mathf.RoundToInt((audioValue + 80) * (100 / 80f));
        displayLabel.text = displayValue.ToString();
    }
}
