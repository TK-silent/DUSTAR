using UnityEngine;
using System.Collections;

public class CenterMusic : MonoBehaviour
{
    public AudioSource musicSource;
    private int playCount = 0;
    private const int MaxPlays = 2;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playCount < MaxPlays)
        {
            PlayMusic();
        }
    }

    private void PlayMusic()
    {
        musicSource.Play();
        playCount++;
        StartCoroutine(WaitForMusicToEnd());
    }

    private IEnumerator WaitForMusicToEnd()
    {
        while (musicSource.isPlaying)
        {
            yield return null;
        }

        if (playCount < MaxPlays)
        {
            PlayMusic();
        }
    }
}