using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    virtual public void Initialize(object data)
    {
        if(closeButton)
            closeButton.onClick.AddListener(Close);
    }

    virtual public void Close()
    {
        Destroy(transform.parent.gameObject);
    }
}
