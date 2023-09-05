using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public delegate void ApplyTetris(TetrisObject tetrisObject);

    public ApplyTetris applyTetris;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("EventManager already exist");
            Destroy(gameObject);
            return;
        }
    }
}
