using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{

    public Button closeButton;

    void Update()
    {
        closeButton.onClick.AddListener(() => {
            GameObject.Destroy(this.gameObject);
        });
    }
}
