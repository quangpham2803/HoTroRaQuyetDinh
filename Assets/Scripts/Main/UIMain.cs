using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    [SerializeField]
    private List<Button> _listMainButtons;
    [SerializeField]
    private GameObject[] _listMainPanels;
    [SerializeField]
    private int _idMainButton;
    [SerializeField]
    private Button _profileButton;
    [SerializeField]
    private GameObject _profilePanel;


    private void Awake()
    {
     
        for (int i = 0; i < _listMainButtons.Count; i++)
        {
            var temp = i;
            _listMainButtons[temp].onClick.AddListener(() => OnSelectMainButton(temp));
        }

        _profileButton.onClick.AddListener(() => { _profilePanel.SetActive(true);});
        OnSelectMainButton(0);
    }


    void OnSelectMainButton(int id)
    {
        _idMainButton = id;
        Debug.Log(id);
        for (int i = 0; i < _listMainPanels.Length; i++)
        {
            var temp = i;
            if (i == _idMainButton)
            {
                _listMainPanels[i].SetActive(true);
                _listMainButtons[i].Select();
            }
            else
            {
                _listMainPanels[i].SetActive(false);
            }
        }
    }
}   
