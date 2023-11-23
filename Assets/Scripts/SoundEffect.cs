using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    AudioSource audioSource;

    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    [Header("MainGame")]
    public AudioClip countdownStartClip;
    public AudioClip countdownEndClip;
    public AudioClip dragClip;
    public AudioClip dropClip;
    public AudioClip harvestClip;
    public AudioClip feverClip;
    public AudioClip rerollClip;
    public AudioClip clearGoalClip;
    public AudioClip doublecomboClip;
    public AudioClip triplecomboClip;
    public AudioClip quadrupleClip;
    public AudioClip gameoverClip;
    public AudioClip clickClip;

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
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("No clip");
        }
    }

    private void Allocate()
    {
        // MainGame
        audioClips.Add("countdownStart", countdownStartClip);
        audioClips.Add("countdownEnd", countdownEndClip);
        audioClips.Add("drag", dragClip);
        audioClips.Add("drop", dropClip);
        audioClips.Add("harvest", harvestClip);
        audioClips.Add("fever", feverClip);
        audioClips.Add("reroll", rerollClip);
        audioClips.Add("clearGoal", clearGoalClip);
        audioClips.Add("doublecombo", doublecomboClip);
        audioClips.Add("triplecombo", triplecomboClip);
        audioClips.Add("quadruple", quadrupleClip);
        audioClips.Add("gameover", gameoverClip);
        audioClips.Add("click", clickClip);

    }
}
