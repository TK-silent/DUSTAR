using System.Collections.Generic;
using UnityEngine;
using System;

public class ConnectionManager : MonoBehaviour
{
    public PageTurner pageTurner1;
    public PageTurner pageTurner2;
    public Book book;
    public Sprite[] sprites = new Sprite[4];  // 保留四个sprite的数组
    public Page pageUI;  // 引用Page脚本
    public DragPen dragPen;
    public Animator animator; // 动画器
    public MissionPoint missionPoint;

    public AudioClip[] connectionSounds;  // 存储多个连接音效的数组
    public AudioSource audioSource;       // 用于播放音效的 AudioSource 组件
    public AudioSource starAudio;

    public GameObject s;
    public GameObject t;
    public GameObject a;
    public GameObject r;

    private HashSet<string> currentConnections = new HashSet<string>(); // 存储当前已经建立的连接

    private Dictionary<List<string>, Action> connectionPatterns = new Dictionary<List<string>, Action>();

    private void Start()
    {
        // 初始化连接模式
        connectionPatterns.Add(new List<string> { "s1s2", "s2s3", "s3s4" }, Pattern1Complete);
        connectionPatterns.Add(new List<string> { "t1t2", "t2t3", "t3t4", "t4t5" }, Pattern2Complete);
        connectionPatterns.Add(new List<string> { "a1a2", "a2a3", "a3a4", "a4a5", "a5a6" }, Pattern3Complete);
        connectionPatterns.Add(new List<string> { "r1r2", "r2r3", "r3r4", "r4r5" }, Pattern4Complete);
    }

    public void AddConnection(Transform first, Transform second)
    {
        string connectionKey = GetConnectionKey(first, second);

        if (!currentConnections.Contains(connectionKey))
        {
            currentConnections.Add(connectionKey);
            PlayRandomConnectionSound();
            CheckAllConnections();
        }
    }

    private void PlayRandomConnectionSound()
    {
        if (audioSource != null && connectionSounds.Length > 0)
        {
            int index = UnityEngine.Random.Range(0, connectionSounds.Length);  // 使用完全限定名
            AudioClip clip = connectionSounds[index];
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("AudioSource or connectionSounds array is not set or empty on the ConnectionManager.");
        }
    }

    private void CheckAllConnections()
    {
        foreach (var pattern in connectionPatterns)
        {
            if (currentConnections.SetEquals(new HashSet<string>(pattern.Key)))
            {
                pattern.Value.Invoke();
                break;
            }
        }
    }

    private string GetConnectionKey(Transform first, Transform second)
    {
        string firstKey = first.name.CompareTo(second.name) < 0 ? first.name : second.name;
        string secondKey = first.name.CompareTo(second.name) < 0 ? second.name : first.name;
        return firstKey + secondKey;
    }

    private void Pattern1Complete()
    {
        Debug.Log("连接模式 1 完成！");
        pageTurner1.pages[0] = sprites[0];
        book.CloseBook();
        dragPen.FinishConnect();
        s.SetActive(true);
        FinishConnectSound();
        animator.SetTrigger("S"); // 触发动画
    }

    private void Pattern2Complete()
    {
        Debug.Log("连接模式 2 完成！");
        pageTurner2.pages[0] = sprites[1];
        book.CloseBook();
        dragPen.FinishConnect();
        t.SetActive(true); 
        FinishConnectSound();
        animator.SetTrigger("T"); // 触发动画
    }

    private void Pattern3Complete()
    {
        Debug.Log("连接模式 3 完成！");
        pageTurner1.pages[1] = sprites[2];
        book.CloseBook();
        dragPen.FinishConnect();
        a.SetActive(true);
        FinishConnectSound();
        animator.SetTrigger("A"); // 触发动画
        missionPoint.TriggerNextTarget();
    }

    private void Pattern4Complete()
    {
        Debug.Log("连接模式 4 完成！");
        pageTurner2.pages[1] = sprites[3];
        book.CloseBook();
        dragPen.FinishConnect();
        r.SetActive(true); 
        FinishConnectSound();
        animator.SetTrigger("R"); // 触发动画
    }

    public void ClearAllConnections()
    {
        currentConnections.Clear();
    }

    public void FinishConnectSound()
    {
        starAudio.Play();
    }
}