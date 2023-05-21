using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListUIButtonItem : MonoBehaviour
{
    public List<UIButtonItem> buttons;

    public List<GameObject> listMenu;
 
    private void Start()
    {
        foreach(var button in buttons)
        {
            button.GetComponent<Button>().onClick.AddListener( () => { SelectType(button);});
        }
        for(int i = 0; i < buttons.Count; i++)
        {
            buttons[i].idButton = i;
        }    
        SelectType(buttons[0]);
    }

    void SelectType(UIButtonItem uiButton)
    {
      foreach(var button in buttons)
        {
            if (button == uiButton)
            {
                button.GetComponent<Image>().enabled = true;
                
                listMenu[button.idButton].SetActive(true);
                SelectController.instance.CategoryHandleButton = button;
                SelectController.instance.CategoryHandleGameObject = listMenu[button.idButton];
                // button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
            }
            else
            {
                listMenu[button.idButton].SetActive(false);
                button.GetComponent<Image>().enabled = false;
               // button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
            }
        }
    }
}
