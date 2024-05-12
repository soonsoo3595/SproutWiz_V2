using UnityEngine;
using UnityEngine.UI;

public class PopupBtn : MonoBehaviour
{
    [SerializeField] private ButtonType type;
    private Button btn;

    public GameObject popup;

    public GameObject back;
    
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(Click);
    }

    public void Click()
    {
        if (type == ButtonType.Popup)
        {
            popup.SetActive(true);
            back.SetActive(true);
            
            BackMgr.instance.Push(this);
        }
        else if(type == ButtonType.Back)
        {
            popup.SetActive(false);
            back.SetActive(false);

            if (BackMgr.instance.GetCount() > 0)
            {
                BackMgr.instance.Pop();
            }
        }
        
    }

    public void BackClick()
    {
        popup.SetActive(false);
    }

    public void Register(GameObject popup, GameObject back)
    {
        this.popup = popup;
        this.back = back;
    }
}
