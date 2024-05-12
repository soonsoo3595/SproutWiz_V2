using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSpriteChange : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button button;

    public Sprite beforeClick;
    public Sprite afterClick;
    public Image skillpad;
    public GameObject bar, icon;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(button.interactable == false) return;

        bar.SetActive(false); icon.SetActive(false);
        skillpad.sprite = afterClick;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (button.interactable == false) return;

        skillpad.sprite = beforeClick;
        bar.SetActive(true); icon.SetActive(true);
    }
}
