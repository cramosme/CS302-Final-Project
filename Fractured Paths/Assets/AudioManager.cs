using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    public AudioClip bgm;

   private void Start()
   {
      musicSource.clip = bgm;
      musicSource.Play();
   }

}
