using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMiniGame
{
    public int ActivationScore { get; }
    public float IntervalTime { get; }
    public float MinimumTerm { get; }
    public bool IsActivate { get; }
    public bool IsRunnig { get; }

    void Activate(float activateTime);

    void Excute();

    void Exit();

    float GetNextExcuteTime();
}
