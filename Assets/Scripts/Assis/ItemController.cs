using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ItemController : MonoBehaviour
{
    public TMP_Text Nametxt;
    public TMP_Text Giatxt;
    public TMP_Text Ratetxt;
    public TMP_Text Recipetxt;
    public TMP_Text Unittxt;
    public Image ImageBG;

    public void LayMon()
    {
        SelectController sct = GameObject.FindGameObjectWithTag("Assis").GetComponent<SelectController>();
        Item it = this.gameObject.GetComponent<Item>();
        sct.AddItemToMenu(it);
    }    
}
