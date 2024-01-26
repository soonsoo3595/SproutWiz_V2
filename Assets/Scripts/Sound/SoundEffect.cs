using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    AudioSource audioSource;

    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    [Header("MainGame")]
    public AudioClip countdownStartClip;
    public AudioClip dropClip;
    public AudioClip harvestClip;
    public AudioClip rerollClip;
    public AudioClip clearGoalClip;
    public AudioClip doublecomboClip;
    public AudioClip triplecomboClip;
    public AudioClip quadrupleClip;
    public AudioClip gameoverClip;
    public AudioClip lockClip;
    public AudioClip clickClip;

    [Header("CraftShop")]
    public AudioClip griffinClip;

    [Header("CraftShop")]
    public AudioClip upgradeClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    private void Start()
    {
        Allocate();
    }

    public void PlayOneShotSoundEffect(string str)
    {
        AudioClip clip;
        bool isContain = audioClips.TryGetValue(str, out clip);

        if(isContain)
        {
            if (clip != null)
                audioSource.PlayOneShot(clip);
            else
                Debug.Log(str + " 소리가 없습니다");
        }
        else
        {
            Debug.Log("No clip");
        }
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void Pause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    public void Resume()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
    }

    private void Allocate()
    {
        // MainGame
        audioClips.Add("countdownStart", countdownStartClip);
        audioClips.Add("drop", dropClip);
        audioClips.Add("harvest", harvestClip);
        audioClips.Add("reroll", rerollClip);
        audioClips.Add("clearGoal", clearGoalClip);
        audioClips.Add("double", doublecomboClip);
        audioClips.Add("triple", triplecomboClip);
        audioClips.Add("quadruple", quadrupleClip);
        audioClips.Add("gameover", gameoverClip);
        audioClips.Add("lock", lockClip);
        audioClips.Add("click", clickClip);

        // MiniGame
        audioClips.Add("griffin", griffinClip);

        // CraftShop
        audioClips.Add("upgrade", upgradeClip);
    }
}
