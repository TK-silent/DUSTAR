using UnityEngine;
using System.Collections;

public class MusicLooper : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    private AudioSource currentSource;

    void Start()
    {
        currentSource = audioSource1;
        currentSource.Play();
        StartCoroutine(PlayMusic());
    }

    private IEnumerator PlayMusic()
    {
        while (true) // 无限循环
        {
            yield return new WaitWhile(() => currentSource.isPlaying); // 等待当前音乐播放完毕

            // 切换到另一个 AudioSource
            currentSource = (currentSource == audioSource1) ? audioSource2 : audioSource1;
            currentSource.Play();
        }
    }
}