using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAssis : MonoBehaviour
{
    public Button findButton;
    public GameObject popUpResutl;
    void Start()
    {
        findButton.onClick.AddListener(OnFindButton);
    }

    void OnFindButton()
    {
        popUpResutl.SetActive(true);
    }
  
}
