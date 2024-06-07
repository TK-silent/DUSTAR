using UnityEngine;

public class BookSound : MonoBehaviour
{
    public AudioSource openAudioSource;
    public AudioSource pageAudioSource;
    public AudioSource closeAudioSource;

    public void OpenAudio()
    {
        openAudioSource.Play();
    }

    public void PageAudio()
    {
        pageAudioSource.Play();
    }

    public void CloseAudio()
    {
        closeAudioSource.Play();
    }
}