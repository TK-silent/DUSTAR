using UnityEngine;

public class RandomNoisePlayer : MonoBehaviour
{
    public AudioClip[] noises;  // 存放所有噪音的数组
    public AudioSource audioSource;  // AudioSource 组件

    private float timeToNextPlay = 0;  // 下次播放的倒计时

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        ScheduleNextPlay();
    }

    void Update()
    {
        // 更新倒计时
        timeToNextPlay -= Time.deltaTime;
        
        if (timeToNextPlay <= 0)
        {
            PlayRandomNoise();
            ScheduleNextPlay();
        }
    }

    void PlayRandomNoise()
    {
        if (noises.Length > 0)
        {
            int index = Random.Range(0, noises.Length);
            audioSource.clip = noises[index];
            audioSource.Play();
        }
    }

    void ScheduleNextPlay()
    {
        // 设置下次播放时间为 2 到 3 分钟之间的随机秒数
        timeToNextPlay = Random.Range(100f, 180f);
    }
}