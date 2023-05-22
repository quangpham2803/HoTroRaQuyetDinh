using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class SelectController : MonoBehaviour
{
    public static SelectController instance;

    public UIButtonItem CategoryHandleButton;
    public GameObject CategoryHandleGameObject;
    public GameObject PopupGoiYMon;
    public GameObject ItemObject;
    public Transform PopupGoiYMonContent;

    public List<Item> Items = new List<Item>();

    public float[,] Matrix_Result;
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
                float value = Matrix_Price[i, j] / sum[j - 1];

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

        // weight
        /// sum
        float[] weight = new float[Items.Count];
        string output_3 = "";
        int weight_count = 0;
        for (int i = 1; i < Items.Count + 1; i++)
        {
            for (int j = 1; j < Items.Count + 1; j++)
            {
                weight[weight_count] += Matrix_Price[i, j];
            }
            weight[weight_count] /= Items.Count;

            output_3 += weight[weight_count] + " ";
            weight_count++;

            if (weight_count >= Items.Count)
            {
                break;
            }

        }
        Debug.Log(output_3);

        //add weight to matrix result
        for (int i = 1; i < Items.Count + 1; i++)
        {
            Matrix_Result[i, 1] = weight[i - 1];
        }
    }
    private void MatrixRating()
    {
        float[,] Matrix_Rating = new float[Items.Count + 1, Items.Count + 1];
        //init First row and column
        for (int i = 0; i < Items.Count + 1; i++)
        {
            if (i == 0)
            {
                //row
                Matrix_Rating[i, 0] = 0;

                // col
                Matrix_Rating[0, i] = 0;
            }
            else
            {
                //row
                Matrix_Rating[i, 0] = Items[i - 1].ID;

                // col
                Matrix_Rating[0, i] = Items[i - 1].ID;
            }

        }

        for (int i = 1; i < Items.Count + 1; i++)
        {
            for (int j = 1; j < Items.Count + 1; j++)
            {
                int menu1 = (int)Matrix_Rating[i, 0];
                int menu2 = (int)Matrix_Rating[0, j];
                float value = _SQL.GetValueInCriteria(menu1, menu2, 2);
                Matrix_Rating[i, j] = value;
            }
        }

        string output = "";

        for (int i = 0; i < Items.Count + 1; i++)
        {
            for (int j = 0; j < Items.Count + 1; j++)
            {
                output += Matrix_Rating[i, j] + " ";
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
                sum[sum_count] += Matrix_Rating[j, i];
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
        float[,] Matrix_Rating_Update = Matrix_Rating;
        for (int i = 1; i < Items.Count + 1; i++)
        {
            for (int j = 1; j < Items.Count + 1; j++)
            {
                float value = Matrix_Rating[i, j] / sum[j - 1];

                Matrix_Rating_Update[i, j] = value;
            }
        }

        string output_2 = "";

        for (int i = 0; i < Items.Count + 1; i++)
        {
            for (int j = 0; j < Items.Count + 1; j++)
            {
                output_2 += Matrix_Rating_Update[i, j] + " ";
            }
            output_2 += "\n";
        }

        Debug.Log(output_2);

        // weight
        /// sum
        float[] weight = new float[Items.Count];
        string output_3 = "";
        int weight_count = 0;
        for (int i = 1; i < Items.Count + 1; i++)
        {
            for (int j = 1; j < Items.Count + 1; j++)
            {
                weight[weight_count] += Matrix_Rating[i, j];
            }
            weight[weight_count] /= Items.Count;

            output_3 += weight[weight_count] + " ";
            weight_count++;

            if (weight_count >= Items.Count)
            {
                break;
            }

        }
        Debug.Log(output_3);

        //add weight to matrix result
        for (int i = 1; i < Items.Count + 1; i++)
        {
            Matrix_Result[i, 2] = weight[i - 1];
        }
    }
    private void MatrixSpeed()
    {
        float[,] Matrix_Speed = new float[Items.Count + 1, Items.Count + 1];
        //init First row and column
        for (int i = 0; i < Items.Count + 1; i++)
        {
            if (i == 0)
            {
                //row
                Matrix_Speed[i, 0] = 0;

                // col
                Matrix_Speed[0, i] = 0;
            }
            else
            {
                //row
                Matrix_Speed[i, 0] = Items[i - 1].ID;

                // col
                Matrix_Speed[0, i] = Items[i - 1].ID;
            }

        }

        for (int i = 1; i < Items.Count + 1; i++)
        {
            for (int j = 1; j < Items.Count + 1; j++)
            {
                int menu1 = (int)Matrix_Speed[i, 0];
                int menu2 = (int)Matrix_Speed[0, j];
                float value = _SQL.GetValueInCriteria(menu1, menu2, 4);
                Matrix_Speed[i, j] = value;
            }
        }

        string output = "";

        for (int i = 0; i < Items.Count + 1; i++)
        {
            for (int j = 0; j < Items.Count + 1; j++)
            {
                output += Matrix_Speed[i, j] + " ";
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
                sum[sum_count] += Matrix_Speed[j, i];
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
        float[,] Matrix_Speed_Update = Matrix_Speed;
        for (int i = 1; i < Items.Count + 1; i++)
        {
            for (int j = 1; j < Items.Count + 1; j++)
            {
                float value = Matrix_Speed[i, j] / sum[j - 1];

                Matrix_Speed_Update[i, j] = value;
            }
        }

        string output_2 = "";

        for (int i = 0; i < Items.Count + 1; i++)
        {
            for (int j = 0; j < Items.Count + 1; j++)
            {
                output_2 += Matrix_Speed_Update[i, j] + " ";
            }
            output_2 += "\n";
        }

        Debug.Log(output_2);

        // weight
        /// sum
        float[] weight = new float[Items.Count];
        string output_3 = "";
        int weight_count = 0;
        for (int i = 1; i < Items.Count + 1; i++)
        {
            for (int j = 1; j < Items.Count + 1; j++)
            {
                weight[weight_count] += Matrix_Speed[i, j];
            }
            weight[weight_count] /= Items.Count;

            output_3 += weight[weight_count] + " ";
            weight_count++;

            if (weight_count >= Items.Count)
            {
                break;
            }

        }
        Debug.Log(output_3);

        //add weight to matrix result
        for (int i = 1; i < Items.Count + 1; i++)
        {
            Matrix_Result[i, 4] = weight[i - 1];
        }
    }
    private void MatrixCalo()
    {
        float[,] Matrix_Calo = new float[Items.Count + 1, Items.Count + 1];
        //init First row and column
        for (int i = 0; i < Items.Count + 1; i++)
        {
            if (i == 0)
            {
                //row
                Matrix_Calo[i, 0] = 0;

                // col
                Matrix_Calo[0, i] = 0;
            }
            else
            {
                //row
                Matrix_Calo[i, 0] = Items[i - 1].ID;

                // col
                Matrix_Calo[0, i] = Items[i - 1].ID;
            }

        }

        for (int i = 1; i < Items.Count + 1; i++)
        {
            for (int j = 1; j < Items.Count + 1; j++)
            {
                int menu1 = (int)Matrix_Calo[i, 0];
                int menu2 = (int)Matrix_Calo[0, j];
                float value = _SQL.GetValueInCriteria(menu1, menu2, 3);
                Matrix_Calo[i, j] = value;
            }
        }

        string output = "";

        for (int i = 0; i < Items.Count + 1; i++)
        {
            for (int j = 0; j < Items.Count + 1; j++)
            {
                output += Matrix_Calo[i, j] + " ";
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
                sum[sum_count] += Matrix_Calo[j, i];
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
        float[,] Matrix_Calo_Update = Matrix_Calo;
        for (int i = 1; i < Items.Count + 1; i++)
        {
            for (int j = 1; j < Items.Count + 1; j++)
            {
                float value = Matrix_Calo[i, j] / sum[j - 1];

                Matrix_Calo_Update[i, j] = value;
            }
        }

        string output_2 = "";

        for (int i = 0; i < Items.Count + 1; i++)
        {
            for (int j = 0; j < Items.Count + 1; j++)
            {
                output_2 += Matrix_Calo_Update[i, j] + " ";
            }
            output_2 += "\n";
        }

        Debug.Log(output_2);

        // weight
        /// sum
        float[] weight = new float[Items.Count];
        string output_3 = "";
        int weight_count = 0;
        for (int i = 1; i < Items.Count + 1; i++)
        {
            for (int j = 1; j < Items.Count + 1; j++)
            {
                weight[weight_count] += Matrix_Calo[i, j];
            }
            weight[weight_count] /= Items.Count;

            output_3 += weight[weight_count] + " ";
            weight_count++;

            if (weight_count >= Items.Count)
            {
                break;
            }

        }
        Debug.Log(output_3);

        //add weight to matrix result
        for (int i = 1; i < Items.Count + 1; i++)
        {
            Matrix_Result[i, 3] = weight[i - 1];
        }
    }

    public void OnclickGoiYMon()
    {
        if (Items.Count > 0)
        {
            Items.Clear();

            foreach (Transform child in PopupGoiYMonContent)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        CategoryCriteriaController Category = CategoryHandleGameObject.GetComponent<CategoryCriteriaController>();
        _SQL.GetFoodFromDB(Category.PriceValue, Category.SpeedValue, Category.RatingValue, CategoryHandleButton.idButton + 1);

        if (Items.Count > 0)
        {
            // Handler AHP
            //intit Matix result
            Matrix_Result = new float[Items.Count + 1, 5];
            //init First row and column
            for (int i = 0; i < Items.Count + 1; i++)
            {
                if (i == 0)
                {
                    //row
                    Matrix_Result[i, 0] = 0;

                    // col
                    Matrix_Result[0, i] = 0;
                }
                else
                {
                    //row
                    Matrix_Result[i, 0] = Items[i - 1].ID;
                }

            }

            //Init Matrix Price from List Item
            MatrixPrice();
            MatrixRating();
            MatrixSpeed();
            MatrixCalo();
            //
            string output = "";

            for (int i = 0; i < Items.Count + 1; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    output += Matrix_Result[i, j] + " ";
                }
                output += "\n";
            }
            Debug.Log(output);
            float[] WeightsCriteria = new float[4];
            WeightsCriteria[0] = 0.4845f;
            WeightsCriteria[1] = 0.3661f;
            WeightsCriteria[2] = 0.1018f;
            WeightsCriteria[3] = 0.0476f;



            float[] Result = new float[Items.Count];

            for (int i = 1; i < Items.Count + 1; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    Result[i - 1] += Matrix_Result[i, j] * WeightsCriteria[j - 1];
                }
            }

            string output_1 = "";

            for (int i = 0; i < Items.Count; i++)
            {
                output_1 += Result[i] + " ";
                output_1 += "\n";
            }
            Debug.Log(output_1);

            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Result_AHP = Result[i];
            }

            Items.Sort(new FloatComparer());

            //add food to content from list item
            foreach (Item it in Items)
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
        PopupGoiYMon.SetActive(true);
    }

}



public class FloatComparer : IComparer<Item>
{
    public int Compare(Item x, Item y)
    {
        if (x.Result_AHP < y.Result_AHP)
            return 1;
        else if (x.Result_AHP > y.Result_AHP)
            return -1;
        else
            return 0;
    }
}
