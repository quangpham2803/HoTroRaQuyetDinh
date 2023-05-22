using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MenuController : MonoBehaviour
{
    public List<Item> ItemsInMenu;
    public UserInfomation User;
    public DateTime currentTime;
    public string Menu_Name;
    private SQL _SQL = new SQL();
    public TMP_InputField nameinput;
    public TMP_Text DateCreate;
    public GameObject ItemObject;
    public Transform PopupGoiYMonContent;

    private void Start()
    {
        User = GameObject.FindGameObjectWithTag("User").GetComponent<UserInfomation>();
    }

    

    public void InitMenu()
    {
        currentTime = DateTime.Now.Date;
        DateCreate.text = "Ngày Tạo: " + currentTime.ToString();
   
        SelectController sct = GameObject.FindGameObjectWithTag("Assis").GetComponent<SelectController>();
        ItemsInMenu = sct.ItemsInMenu;
        foreach (Item it in ItemsInMenu)
        {
            GameObject newitem = Instantiate(ItemObject, PopupGoiYMonContent);
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
    public void XacNhanTao()
    {
        Menu_Name = nameinput.text;
        _SQL.SaveMenu(currentTime, User.ID, Menu_Name, ItemsInMenu);
        if (ItemsInMenu.Count > 0)
        {
            ItemsInMenu.Clear();

            foreach (Transform child in PopupGoiYMonContent)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }    
} 
