using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundBGM : MonoBehaviour
{
    AudioSource audioSource;

    // 0 : ���ΰ���
    // 1 : Ÿ��Ʋ
    public AudioClip[] bgmClips;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // �̺�Ʈ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        PlayBGM(scene.buildIndex);
    }

    private void PlayBGM(int index)
    {
        audioSource.clip = bgmClips[index];
        audioSource.Play();
    }
}
