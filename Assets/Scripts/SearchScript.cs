using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SearchScript : MonoBehaviour
{
    private SQL _SQL = new SQL();
    public List<Item> listItems;
    public Transform ContentPanel;
    public GameObject ItemObject;
    string ValueGet;
    public TMP_InputField ValueResearch;
    public Button ValueResearchBtn;

    private void Start()
    {
        ValueResearchBtn.onClick.AddListener(Init);
    }
 
    public void Init()
    {
        ValueGet = ValueResearch.text;
        if (!ValueGet.Equals(""))
        {
            if (listItems.Count > 0)
            {
                listItems.Clear();
            }
            listItems = _SQL.SearchFood(ValueGet);
            if (listItems.Count > 0)
            {
                foreach (Transform child in ContentPanel)
                {
                    GameObject.Destroy(child.gameObject);
                }


                foreach (Item it in listItems)
                {
                    GameObject newitem = Instantiate(ItemObject, ContentPanel);
                    Item newit = newitem.GetComponent<Item>();
                    newit = it;
                    ItemController itct = newitem.gameObject.GetComponent<ItemController>();
                    itct.Nametxt.text = newit.Name;
                    itct.Giatxt.text = newit.Price.ToString() + " vnđ";
                    itct.Ratetxt.text = newit.Rating.ToString();
                    itct.Unittxt.text = newit.Unit.ToString();
                    itct.Recipetxt.text = newit.Recipe.ToString();
                    itct.ImageBG.sprite = newit.Image;
                }
            }
        }
       
        
    }
}
