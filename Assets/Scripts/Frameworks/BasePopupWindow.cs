using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasePopupWindow : MonoBehaviour
{
    public bool IsOpen { get; set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Open(UnityAction opened) {

        if (IsOpen == true)
            return;
        IsOpen = true;

        opened?.Invoke();
    }

    public virtual void Close(UnityAction closed) {

        if (IsOpen == false)
            return;
        IsOpen = false;

        closed?.Invoke();
    }


}
