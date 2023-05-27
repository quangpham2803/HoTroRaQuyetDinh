using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MainViewController : MonoBehaviour
{
    public UserInfomation User;
    private SQL _SQL = new SQL();
    public List<Item> listItems;
    public Transform ContentPanel;
    public GameObject ItemObject;
    public TMP_Text NameTxt;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        User = GameObject.FindGameObjectWithTag("User").GetComponent<UserInfomation>();
        NameTxt.text = "hi, " + User.name;
       listItems = _SQL.GetALLFood();
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
