using UnityEngine;

public class MounmentSound : MonoBehaviour
{
    public AudioSource touchAudio;
    public AudioSource riseAudio;

    public void PlayTouchSound()
    {
        touchAudio.Play();
    }

    public void PlayRiseSound()
    {
        riseAudio.Play();
    }
}