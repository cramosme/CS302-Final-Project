using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Audio;
// using UnityEngine.UI;

public class CustomSlider : MonoBehaviour
{
   
   private VisualElement root;
   private VisualElement slider;
   private VisualElement dragger;
   private VisualElement fillBar;
   
   //New Dragger
   private VisualElement newDragger;

   private Slider musicSlider;

   private Label displayLabel;

   [SerializeField] private AudioMixer myMixer;

   // Start is called before the first frame update
   void Start()
   {
      root = GetComponent<UIDocument>().rootVisualElement;
      slider = root.Q<Slider>("MusicSlider");
      dragger = root.Q<VisualElement>("unity-dragger");
      musicSlider = root.Q<Slider>("MusicSlider");

      displayLabel = root.Q<Label>("VolumeDisplay");

      AddElements();

      slider.RegisterCallback<ChangeEvent<float>>(SliderValueChanged);

      if( PlayerPrefs.HasKey("musicVolume"))
      {
         LoadVolume();
      }
      else
      {
         SetMusicVolume();
      }

      UpdateDisplayLabel(musicSlider.value);

   }

   void AddElements()
   {
      fillBar = new VisualElement();
      dragger.Add(fillBar);
      fillBar.name = "Bar";
      fillBar.AddToClassList("fillBar");

      newDragger = new VisualElement();
      slider.Add(newDragger);
      newDragger.name = "NewDragger";
      newDragger.AddToClassList("newDragger");
      newDragger.pickingMode = PickingMode.Ignore;

      slider.RegisterCallback<GeometryChangedEvent>(SliderInit);
   }

   void SliderValueChanged(ChangeEvent<float> value)
   {      
      // Round the new slider value to the nearest integer
      int roundedValue = Mathf.RoundToInt(value.newValue);

      // Set the slider value to the rounded integer
      musicSlider.value = roundedValue;

      UpdateDisplayLabel(roundedValue);
      CalculateDraggerPosition();

      SetMusicVolume();
   }

   private void CalculateDraggerPosition()
   {
      Vector2 offset = new Vector2( (newDragger.layout.width - dragger.layout.width) / 2, (newDragger.layout.height - dragger.layout.height) / 2);
      Vector2 pos = dragger.parent.LocalToWorld(dragger.transform.position);
      newDragger.transform.position = newDragger.parent.WorldToLocal(pos-offset);
   }

   void SliderInit(GeometryChangedEvent evt)
   {
      Vector2 offset = new Vector2( (newDragger.layout.width - dragger.layout.width) / 2, (newDragger.layout.height - dragger.layout.height) / 2);
      Vector2 pos = dragger.parent.LocalToWorld(dragger.transform.position);
      newDragger.transform.position = newDragger.parent.WorldToLocal(pos-offset);
   }

   private void SetMusicVolume()
   {
      float volume = musicSlider.value;
      myMixer.SetFloat("music", volume);
      PlayerPrefs.SetFloat("musicVolume", volume);
   }

   private void LoadVolume()
   {
      musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
      slider.RegisterCallback<ChangeEvent<float>>(SliderValueChanged);

   }

   private void UpdateDisplayLabel(float audioValue)
   {
      //Convert audioValue (-80 to 0) to a display range (100 to 0)
      int displayValue = Mathf.RoundToInt((audioValue + 80) * (100 / 80f));
      displayLabel.text = displayValue.ToString();
   }
}
