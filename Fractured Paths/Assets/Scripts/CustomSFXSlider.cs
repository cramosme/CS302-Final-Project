using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Audio;

public class CustomSFXSlider : MonoBehaviour
{

    private VisualElement root;
    private VisualElement slider;
    private VisualElement dragger;
    private VisualElement fillBar;

    //New Dragger
    private VisualElement newDragger;

    private Slider mySlider;

    private Label displayLabel;

    [SerializeField] private AudioMixer myMixer;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        slider = root.Q<Slider>("SFXSlider");
        mySlider = root.Q<Slider>("SFXSlider");
        dragger = mySlider.Q<VisualElement>("unity-dragger");

        displayLabel = root.Q<Label>("SFXVolumeDisplay");

        AddElements();

        slider.RegisterCallback<ChangeEvent<float>>(SliderValueChanged);

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetSFXVolume();
        }

        UpdateDisplayLabel(mySlider.value);

    }

    void AddElements()
    {
        fillBar = new VisualElement();
        dragger.Add(fillBar);
        fillBar.name = "sfxBar";
        fillBar.AddToClassList("sfxFillBar");

        newDragger = new VisualElement();
        slider.Add(newDragger);
        newDragger.name = "NewSfxDragger";
        newDragger.AddToClassList("newSfxDragger");
        newDragger.pickingMode = PickingMode.Ignore;

        slider.RegisterCallback<GeometryChangedEvent>(SliderInit);
    }

    void SliderValueChanged(ChangeEvent<float> value)
    {
        // Round the new slider value to the nearest integer
        int roundedValue = Mathf.RoundToInt(value.newValue);

        // Set the slider value to the rounded integer
        mySlider.value = roundedValue;

        UpdateDisplayLabel(roundedValue);
        CalculateDraggerPosition();

        SetSFXVolume();
    }

    private void CalculateDraggerPosition()
    {
        Vector2 offset = new Vector2((newDragger.layout.width - dragger.layout.width) / 2, (newDragger.layout.height - dragger.layout.height) / 2);
        Vector2 pos = dragger.parent.LocalToWorld(dragger.transform.position);
        newDragger.transform.position = newDragger.parent.WorldToLocal(pos - offset);
    }

    void SliderInit(GeometryChangedEvent evt)
    {
        Vector2 offset = new Vector2((newDragger.layout.width - dragger.layout.width) / 2, (newDragger.layout.height - dragger.layout.height) / 2);
        Vector2 pos = dragger.parent.LocalToWorld(dragger.transform.position);
        newDragger.transform.position = newDragger.parent.WorldToLocal(pos - offset);
    }

    private void SetSFXVolume()
    {
        float volume = mySlider.value;
        myMixer.SetFloat("sfx", volume);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void LoadVolume()
    {
        mySlider.value = PlayerPrefs.GetFloat("sfxVolume");
        slider.RegisterCallback<ChangeEvent<float>>(SliderValueChanged);

    }

    private void UpdateDisplayLabel(float audioValue)
    {
        //Convert audioValue (-80 to 0) to a display range (100 to 0)
        int displayValue = Mathf.RoundToInt((audioValue + 80) * (100 / 80f));
        displayLabel.text = displayValue.ToString();
    }
}