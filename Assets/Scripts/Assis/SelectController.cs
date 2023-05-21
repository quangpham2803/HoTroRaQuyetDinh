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

    private void MatrixPrice()
    {
        float[,] Matrix_Price = new float[Items.Count + 1, Items.Count + 1];
        //init First row and column
        for (int i = 0; i < Items.Count + 1; i++)
        {
            if (i == 0)
            {
                //row
                Matrix_Price[i, 0] = 0;

                // col
                Matrix_Price[0, i] = 0;
            }
            else
            {
                //row
                Matrix_Price[i, 0] = Items[i - 1].ID;

                // col
                Matrix_Price[0, i] = Items[i - 1].ID;
            }

        }


        

        for (int i = 1; i < Items.Count + 1; i++)
        {
            for (int j = 1; j < Items.Count + 1; j++)
            {
                int menu1 = (int)Matrix_Price[i, 0];
                int menu2 = (int)Matrix_Price[0, j];
                float value = _SQL.GetValueInCriteria(menu1, menu2, 1);
                Matrix_Price[i, j] = value;
            }
        }

        string output = "";

        for (int i = 0; i < Items.Count + 1; i++)
        {
            for (int j = 0; j < Items.Count + 1; j++)
            {
                output += Matrix_Price[i, j] + " ";
            }
            output += "\n";
        }

        Debug.Log(output);

        /// sum
        float[] sum = new float[Items.Count];
        string output_1 = "";
        int sum_count = 0;
        for (int i = 1; i < Items.Count + 1; i++)
        {
            for (int j = 1; j < Items.Count + 1; j++)
            {
                sum[sum_count] += Matrix_Price[j, i];
            }
            output_1 += sum[sum_count] + " ";
            sum_count++;

            if (sum_count >= Items.Count)
            {
                break;
            }

        }
        Debug.Log(output_1);

        /// matrix update
        float[,] Matrix_Price_Update = Matrix_Price;
        for (int i = 1; i < Items.Count + 1; i++)
        {
            for (int j = 1; j < Items.Count + 1; j++)
            {
                float value = Matrix_Price[i, j] / sum[j-1];
              
                Matrix_Price_Update[i, j] = value;
            }
        }

        string output_2 = "";

        for (int i = 0; i < Items.Count + 1; i++)
        {
            for (int j = 0; j < Items.Count + 1; j++)
            {
                output_2 += Matrix_Price_Update[i, j] + " ";
            }
            output_2 += "\n";
        }

        Debug.Log(output_2);

    }    

    public void OnclickGoiYMon()
    {
        CategoryCriteriaController Category = CategoryHandleGameObject.GetComponent<CategoryCriteriaController>();
        _SQL.GetFoodFromDB(Category.PriceValue, Category.SpeedValue, Category.RatingValue, CategoryHandleButton.idButton + 1);

        if (Items.Count > 0)
        {
            // Handler AHP
            //Init Matrix Price from List Item
            MatrixPrice();
            //add food to content from list item
            foreach (Item it in Items)
            {
                Item newitem = Instantiate(ItemObject, PopupGoiYMonContent);
                newitem = it;
            }
        }
        PopupGoiYMon.SetActive(true);
    }



}
