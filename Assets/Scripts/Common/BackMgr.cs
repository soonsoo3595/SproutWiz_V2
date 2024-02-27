using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMgr : MonoBehaviour
{
    public static BackMgr instance;

    private Stack<PopupBtn> st;
    
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            st = new Stack<PopupBtn>();
        }
    }

    public int GetCount()
    {
        return st.Count;
    }

    public void Push(PopupBtn popup)
    {
        st.Push(popup);
    }

    public void Pop()
    {
        if (st.Count > 0)
        {
            PopupBtn popup = st.Peek();
            st.Pop();
            
            popup.BackClick();
        }
    }

    public void Clear()
    {
        st.Clear();
    }
}
