using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRecord : MonoBehaviour
{
    [SerializeField] private MainGame mainGame;

    public List<string> nameList;
    public Dictionary<string, int> dict = new Dictionary<string, int>();

    void Start()
    {
        Assign();
    }

    public List<int> GetRecord()
    {
        List<int> list = new List<int>();

        foreach (var name in nameList)
        {
            list.Add(dict[name]);
        }

        return list;
    }

    private void Init()
    {
        foreach (var name in nameList)
        {
            dict[name] = 0;
        }
    }

    private void Assign()
    {
        foreach(var name in nameList)
        {
            dict.Add(name, 0);
        }

        EventManager.resetMainGame += Init;
    }
}
