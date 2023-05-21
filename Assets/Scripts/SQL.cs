using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;

public class SQL : MonoBehaviour
{
    string connectionString = "Data Source=MSI;Initial Catalog=DES_Restaurant;Integrated Security=True";
    public static SQL instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            instance = this;
        }    
        
    }
    public bool CheckLogin(string username, string password)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string sql = "select Customer_Name, Customer_Password from Customer where Customer_Name = '" + username + "' and Customer_Password = '" + password + "'";
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = connection;
        cmd.CommandText = sql;

        using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
        {
            if (reader.HasRows)
            {

                while (reader.Read())
                {                 
                    var Username = reader.GetValue(0);
                    var Password = reader.GetValue(1);
                    if (Username != null)
                    {
                        return true;
                    }    
                    else
                    {
                        return false;
                    }    
                }
            }
        }
        return false;
    }

    public void SignUp(string UserName, string Password, string PhoneNumber)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string sql = "";// "Insert Into Customer(Customer_ID, Customer_Name, Customer_Password, Customer_Phone) values('" +UserName+"'"CS_001','admin', 'admin', 'admin')";
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = connection;
        cmd.CommandText = sql;
        
    }    
    public void GetFoodFromDB(string price, string speed, string rating, int category)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string sql = "select MenuItem_ID, MenuItem_Name, MenuItem_Image, MenuItem_Price, MenuItem_Calo, MenuItem_Rating, MenuItem_Speed, MenuItem_Category, MenuItem_Unit from MenuItems where MenuItem_Price "+ price + " and " + speed + " and " + rating + " and MenuItem_Category = " + category + "";
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = connection;
        cmd.CommandText = sql;
        SelectController.instance.Items.Clear();
        using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
        {
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    int ID = reader.GetInt32(0);
                    string Name = reader.GetString(1);
                    //string Image = reader.GetString(2);
                    long Price = reader.GetInt64(3);
                    double Calo = reader.GetDouble(4);
                    double Rating = reader.GetDouble(5);
                    int Speed = reader.GetInt32(6);
                    int Category = reader.GetInt32(7);
                    string Unit = reader.GetString(8);
                    Item newItem = new Item();
                    newItem.initDataItem(ID, Name, "",Price, (float)Rating, (float)Calo, Speed, Category, Unit);
                    SelectController.instance.Items.Add(newItem);
                }
            }
        }
   
    }   
    
    public float GetValueInCriteria(int menu1, int menu2, int criteria)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string sql = "select Priority_criteria_forMenu_Score from Priority_criteria_forMenu where Priority_criteria_forMenu_1 = " + menu1 + " and Priority_criteria_forMenu_2 = " + menu2 + " and Priority_criteria_ID = " + criteria + "";
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = connection;
        cmd.CommandText = sql;
      
        using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
        {
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    return (float)reader.GetDouble(0);
                }
            }
        }
        return 0;
    }    
}
