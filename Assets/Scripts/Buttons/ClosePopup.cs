using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePopup : MonoBehaviour
{
    public Button closeButton;
    // Start is called before the first frame update
    void Start()
    {
       closeButton.onClick.AddListener(() => { this.gameObject.SetActive(false);});
    }
  
}
