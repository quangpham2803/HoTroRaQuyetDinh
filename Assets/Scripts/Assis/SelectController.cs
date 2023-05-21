using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour
{
    public static SelectController instance;

    public UIButtonItem CategoryHandleButton;
    public GameObject CategoryHandleGameObject;
    public GameObject PopupGoiYMon;
    public Item ItemObject;
    public Transform PopupGoiYMonContent;

    public List<Item> Items = new List<Item>();

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this; 
    }

    private SQL _SQL = new SQL();

    public void OnclickGoiYMon()
    {
        CategoryCriteriaController Category = CategoryHandleGameObject.GetComponent<CategoryCriteriaController>();
        _SQL.GetFoodFromDB(Category.PriceValue, Category.SpeedValue, Category.RatingValue, CategoryHandleButton.idButton + 1);
        if (Items.Count > 0 )
        {
            //add food to content from list item
            foreach (Item it in Items)
            {
                Item newitem  = Instantiate(ItemObject, PopupGoiYMonContent);
                newitem = it;
            }    
        }    
        PopupGoiYMon.SetActive(true);
    }    
    
    

}
