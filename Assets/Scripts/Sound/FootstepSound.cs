using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource leftFootAudioSource;
    public AudioSource rightFootAudioSource;
    public AudioSource fellAudioSource;

    public void PlayLeftFootSound()
    {
        leftFootAudioSource.Play();
    }

    public void PlayRightFootSound()
    {
        rightFootAudioSource.Play();
    }

    public void PlayFellSound()
    {
        fellAudioSource.Play();
    }
}