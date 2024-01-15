using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Category : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private List<GameObject> upgradeElements;

    // Start is called before the first frame update
    void Start()
    {
        toggle.onValueChanged.AddListener((value) =>
        {
            for(int i = 0; i < upgradeElements.Count; i++)
            {
                upgradeElements[i].SetActive(value);
            }
        });
    }

}
