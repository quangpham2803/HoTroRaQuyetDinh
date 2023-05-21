using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID;
    public string Name;
    public string Image;
    public float Price;
    public float Rating;
    public float Calo;
    public int Speed;
    public int Category;
    public string Unit;

    public void initDataItem(int id, string name, string image, float price, float rating, float calo, int speed, int category, string unit)
    {
        ID = id;
        Name = name;
        Image = image;
        Price = price;
        Rating = rating;
        Calo = calo;
        Speed = speed;
        Category = category;
        Unit = unit;
    }    
}
