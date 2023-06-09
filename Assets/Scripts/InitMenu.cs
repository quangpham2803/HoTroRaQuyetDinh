﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InitMenu : MonoBehaviour
{

    public UserInfomation User;
    private SQL _SQL = new SQL();
    public List<MenuInFo> listMenu;
    public Transform Content;
    public GameObject MenuButon;
    public GameObject Panel;
    public TMP_Text name;
    public TMP_Text date;
    public TMP_Text totalPrice;
    public TMP_Text totalTable;
    public Transform ContentPanel;
    public GameObject ItemObject;

    private void Start()
    {
        Panel.gameObject.SetActive(false);
    }
    public void Init()
    {
        if (listMenu.Count > 0)
        {
            listMenu.Clear();

            foreach (Transform child in Content)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        User = GameObject.FindGameObjectWithTag("User").GetComponent<UserInfomation>();
        listMenu = _SQL.GetAllMenu(User.ID);
        foreach (MenuInFo it in listMenu)
        {
            GameObject newitem = Instantiate(MenuButon, Content);
            MenuInFo newit = newitem.GetComponent<MenuInFo>();
            newit.id = it.id;
            newit.name = it.name;
            newit.date = it.date;
            newit.Items = it.Items;
            newit.totalPrice = it.totalPrice;
            newit.totalTable = it.totalTable;
            newitem.GetComponentInChildren<TMP_Text>().text = it.name;
            newitem.GetComponentInChildren<Button>().onClick.AddListener(() => { OpenPopup(newit); });

        }
    }
    public void OpenPopup(MenuInFo mn)
    {

        foreach (Transform child in ContentPanel)
        {
            GameObject.Destroy(child.gameObject);
        }

        Panel.gameObject.SetActive(true);
        name.text = mn.name;
        date.text = mn.date;
        totalPrice.text ="Tông giá của Menu: " + (mn.totalPrice* mn.totalTable).ToString() + " Vnđ";
        totalTable.text = mn.totalTable.ToString() + " Bàn";
        bool flag = false;
        foreach (Item it in mn.Items)
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
            if (newit.Category == 4)
            {
                flag = true;
            }    
        }
        if (flag)
        {
            totalPrice.text = "Tông giá của Menu: " + (mn.totalPrice * mn.totalTable).ToString() + " Vnđ" + ", Tông giá chưa bao gồm tiền nước";
        }    
    }

}
