using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _loginPanel;
    [SerializeField]
    private GameObject _logupPanel;
    [SerializeField]
    private Button _switchLogButton;

    private void Awake()
    {
        _switchLogButton.onClick.AddListener(OnSwitchLogButton);
    }

    void OnSwitchLogButton()
    {
        _logupPanel.SetActive(true);
        _loginPanel.SetActive(false);
    }
}
