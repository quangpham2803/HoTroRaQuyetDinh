using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class LoginManager : MonoBehaviour
{
    [SerializeField] private GameObject _loginPanel;
    [SerializeField] private GameObject _logupPanel;
    [SerializeField] private GameObject _forgetPasswordPanel;
    [SerializeField] private Button _switchLogButton;
    [SerializeField] private Button _switchForgetPasswordButton;


    [SerializeField] private TMP_InputField _inputUserName;
    [SerializeField] private TMP_InputField _inputPassword;

    [SerializeField] private Button _loginButton;
    [SerializeField] private Button _signupButton;
    [SerializeField] private Button _HasAccountButton;
    [SerializeField] private Button _ReturnforgetPasswordButton;
    [SerializeField] private Button _getNewPasswordButton;


    [SerializeField] private TMP_InputField _inputUserNameSignUp;
    [SerializeField] private TMP_InputField _inputPasswordSignUp;
    [SerializeField] private TMP_InputField _inputPhoneNumberSignUp;

    [SerializeField] private TMP_InputField _inputForgotUserNameSignUp;
    [SerializeField] private TMP_InputField _inputForgotPhoneNumberSignUp;

    private SQL _SQL = new SQL();

    private void Awake()
    {
        _switchLogButton.onClick.AddListener(OnSwitchLogButton);
        _switchForgetPasswordButton.onClick.AddListener(OnSwitchForgetButton);
        _loginButton.onClick.AddListener(OnLogin);
        _signupButton.onClick.AddListener(SignUp);
        _HasAccountButton.onClick.AddListener(HasAccount);
        _ReturnforgetPasswordButton.onClick.AddListener(ReturnFromForgetPanel);
        _getNewPasswordButton.onClick.AddListener(GetNewPassword);
    }
    void SignUp()
    {
        if (_inputUserNameSignUp.text != "" && _inputPasswordSignUp.text != "" && _inputPhoneNumberSignUp.text != "")
        {
            _SQL.SignUp(_inputUserNameSignUp.text, _inputPasswordSignUp.text, _inputPhoneNumberSignUp.text);
            HasAccount();
        }
    }
    void GetNewPassword()
    {
      
        if (_inputForgotUserNameSignUp.text != "" && _inputForgotPhoneNumberSignUp.text != "")
        {
            Debug.Log("okokClickGetNewPassword");
        }    
    }    
    void OnSwitchForgetButton()
    {
        _logupPanel.SetActive(false);
        _loginPanel.SetActive(false);
        _forgetPasswordPanel.SetActive(true);
    }    
    void HasAccount()
    {
        _logupPanel.SetActive(false);
        _forgetPasswordPanel.SetActive(false);
        _loginPanel.SetActive(true);
        _inputUserNameSignUp.text = "";
        _inputPasswordSignUp.text = "";
        _inputPhoneNumberSignUp.text = "";

    }    
    void ReturnFromForgetPanel()
    {
        _logupPanel.SetActive(false);
        _loginPanel.SetActive(true);
        _forgetPasswordPanel.SetActive(false);
    }    
    void OnLogin()
    {
        if (_inputUserName.text != "" && _inputPassword.text != "")
        {
            bool islogin = _SQL.CheckLogin(_inputUserName.text, _inputPassword.text);
            if (islogin)
            {               
                SceneManager.LoadScene("Main");              
            }
            else
            {
                Debug.Log("Sai TK or MK");
            }    
        }

    }
    void OnSwitchLogButton()
    {
        _logupPanel.SetActive(true);
        _loginPanel.SetActive(false);
        _forgetPasswordPanel.SetActive(false);
    }
}
