using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;
using System.IO;
using System;
using System.Data;

public class SQL : MonoBehaviour
{
    string connectionString = "Data Source=MSI;Initial Catalog=DES_Restaurant;Integrated Security=True; Max Pool Size=100;Connect Timeout=60";
    public static SQL instance;
    // Start is called before the first frame update

    public bool CheckLogin(string username, string password)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string sql = "select Customer_ID, Customer_Name, Customer_Password from Customer where Customer_Name = '" + username + "' and Customer_Password = '" + password + "'";
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = connection;
        cmd.CommandText = sql;

        using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
        {
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    string id = reader.GetString(0);
                    string Username = reader.GetString(1);
                    string Password = reader.GetString(2);
                    if (Username != null)
                    {
                        UserInfomation us = GameObject.FindGameObjectWithTag("User").GetComponent<UserInfomation>();
                        us.ID = id;
                        us.name = Username;
                        us.password = Password;
                        return true;
                        connection.Close();
                    }
                    else
                    {
                        return false;
                        connection.Close();
                    }
                }
            }
        }
        return false;
    }

    public void SaveMenu(DateTime date, string customer, string name_menu, List<Item> items, float total, int numTable)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand("insert into Menu(Menu_Time, Menu_Customer, Menu_Name, Menu_TableCount, Menu_TablePrice) values (@date,@user,@name,@tablecount, @tableprice)", connection);
        SqlParameter param_date = new SqlParameter("@date", SqlDbType.Date);
        param_date.Value = date;

        SqlParameter param_user = new SqlParameter("@user", SqlDbType.VarChar);
        param_user.Value = customer;

        SqlParameter param_name = new SqlParameter("@name", SqlDbType.NVarChar);
        param_name.Value = name_menu;

        SqlParameter param_tablecount = new SqlParameter("@tablecount", SqlDbType.Int);
        param_tablecount.Value = numTable;

        SqlParameter param_ntableprice = new SqlParameter("@tableprice", SqlDbType.BigInt);
        param_ntableprice.Value = total;

        cmd.Parameters.Add(param_date);
        cmd.Parameters.Add(param_user);
        cmd.Parameters.Add(param_name);
        cmd.Parameters.Add(param_tablecount);
        cmd.Parameters.Add(param_ntableprice);
        cmd.ExecuteNonQuery();

        // insert item to MenuItem Detail 
        string sql = "SELECT Menu_ID FROM Menu WHERE Menu_ID = (SELECT MAX(Menu_ID) FROM Menu);";
        SqlCommand cmd_1 = new SqlCommand();

        cmd_1.Connection = connection;
        cmd_1.CommandText = sql;
        int id_menu = -1;
        using (System.Data.Common.DbDataReader reader = cmd_1.ExecuteReader())
        {
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    id_menu = reader.GetInt32(0);
                }
            }
        }

        if (id_menu != -1)
        {

            if (items.Count > 0)
            {
                foreach (Item it in items)
                {
                    try
                    {
                        string sql_1 = "Insert into MenuDetail(MenuDetail_Item, MenuDetail_MenuID) Values(" + it.ID + "," + id_menu + ")";
                        SqlCommand cmd_2 = new SqlCommand();
                        cmd_2.Connection = connection;
                        cmd_2.CommandText = sql_1;
                        cmd_2.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        Debug.Log("Trung Menu");
                    }

                }


            }

        }
        connection.Close();

    }

    public List<MenuInFo> GetAllMenu(string user)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string sql = "select Menu_ID, Menu_Name, Menu_Time, Menu_TableCount, Menu_TablePrice from Menu where Menu_Customer = '" + user + "'";
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = connection;
        cmd.CommandText = sql;
        List<MenuInFo> newMenus = new List<MenuInFo>();
        using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
        {
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    MenuInFo newMenu = new MenuInFo();
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string date = reader.GetDateTime(2).ToString();
                    int table = reader.GetInt32(3);
                    long price = reader.GetInt64(4);
                   
                    newMenu.id = id;
                    newMenu.name = name;
                    newMenu.date = date;
                    newMenu.totalTable = table;
                    newMenu.totalPrice = (float)price;
                    List<Item> listItem = GetALLFoodByID(id);
                    newMenu.Items = listItem;
                    newMenus.Add(newMenu);

                }
            }
        }
        connection.Close();
        return newMenus;
    }

    List<int> GetListIDFood(int id)
    {
        List<int> intlist = new List<int>();
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string sql = "select MenuDetail_Item from MenuDetail where MenuDetail_MenuID = " + id + " ";
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = connection;
        cmd.CommandText = sql;

        using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
        {
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    int id_new = reader.GetInt32(0);
                    intlist.Add(id_new);

                }
            }
        }
        connection.Close();
        return intlist;

    }

    List<Item> GetALLFoodByID(int id)
    {
        List<int> listint = GetListIDFood(id);
        List<Item> listItem = new List<Item>();
        if (listint.Count > 0)
        {
            for (int i = 0; i < listint.Count; i++)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string sql = "select MenuItem_ID, MenuItem_Name, MenuItem_Image, MenuItem_Price, MenuItem_Calo, MenuItem_Rating, MenuItem_Speed, MenuItem_Category, MenuItem_Unit, MenuItems_Recipe from MenuItems where MenuItem_ID = " + listint[i] + " ";
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            int ID = reader.GetInt32(0);
                            string Name = reader.GetString(1);

                            byte[] imageData = (byte[])reader["MenuItem_Image"];
                            Texture2D texture = new Texture2D(1, 1);
                            texture.LoadImage(imageData);
                            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                            long Price = reader.GetInt64(3);
                            double Calo = reader.GetDouble(4);
                            double Rating = reader.GetDouble(5);
                            int Speed = reader.GetInt32(6);
                            int Category = reader.GetInt32(7);
                            string Unit = reader.GetString(8);
                            string Recipe;
                            try
                            {
                                Recipe = reader.GetString(9);
                                Recipe = Recipe.Substring(3, Recipe.Length - 8);
                            }
                            catch (Exception)
                            {
                                Recipe = "";
                            }


                            Item newItem = new Item();

                            newItem.initDataItem(ID, Name, sprite, Price, (float)Rating, (float)Calo, Speed, Category, Unit, Recipe);
                            listItem.Add(newItem);
                        }
                    }
                }
            }
        }
        return listItem;
    }

    public List<Item> SearchFood(string key)
    {
        List<Item> listItem = new List<Item>();
  
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string sql = "select MenuItem_ID, MenuItem_Name, MenuItem_Image, MenuItem_Price, MenuItem_Calo, MenuItem_Rating, MenuItem_Speed, MenuItem_Category, MenuItem_Unit, MenuItems_Recipe from MenuItems where lower(MenuItem_Name) like lower(N'%" + key+ "%')";
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            int ID = reader.GetInt32(0);
                            string Name = reader.GetString(1);

                            byte[] imageData = (byte[])reader["MenuItem_Image"];
                            Texture2D texture = new Texture2D(1, 1);
                            texture.LoadImage(imageData);
                            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                            long Price = reader.GetInt64(3);
                            double Calo = reader.GetDouble(4);
                            double Rating = reader.GetDouble(5);
                            int Speed = reader.GetInt32(6);
                            int Category = reader.GetInt32(7);
                            string Unit = reader.GetString(8);
                            string Recipe;
                            try
                            {
                                Recipe = reader.GetString(9);
                                Recipe = Recipe.Substring(3, Recipe.Length - 8);
                            }
                            catch (Exception)
                            {
                                Recipe = "";
                            }


                            Item newItem = new Item();

                            newItem.initDataItem(ID, Name, sprite, Price, (float)Rating, (float)Calo, Speed, Category, Unit, Recipe);
                            listItem.Add(newItem);
                        }
                    
                
            }
        }
        return listItem;
    }

    public List<Item> GetALLFood()
    {

        List<Item> listItem = new List<Item>();

        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string sql = "select Top 20 MenuItem_ID, MenuItem_Name, MenuItem_Image, MenuItem_Price, MenuItem_Calo, MenuItem_Rating, MenuItem_Speed, MenuItem_Category, MenuItem_Unit, MenuItems_Recipe from MenuItems ";
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = connection;
        cmd.CommandText = sql;

        using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
        {
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    int ID = reader.GetInt32(0);
                    string Name = reader.GetString(1);

                    byte[] imageData = (byte[])reader["MenuItem_Image"];
                    Texture2D texture = new Texture2D(1, 1);
                    texture.LoadImage(imageData);
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                    long Price = reader.GetInt64(3);
                    double Calo = reader.GetDouble(4);
                    double Rating = reader.GetDouble(5);
                    int Speed = reader.GetInt32(6);
                    int Category = reader.GetInt32(7);
                    string Unit = reader.GetString(8);
                    string Recipe;
                    try
                    {
                        Recipe = reader.GetString(9);
                        Recipe = Recipe.Substring(3, Recipe.Length - 8);
                    }
                    catch (Exception)
                    {
                        Recipe = "";
                    }


                    Item newItem = new Item();

                    newItem.initDataItem(ID, Name, sprite, Price, (float)Rating, (float)Calo, Speed, Category, Unit, Recipe);
                    listItem.Add(newItem);
                }
            }

        }

        return listItem;
    }

    public void SignUp(string UserName, string Password, string PhoneNumber)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string sql = "INSERT INTO Customer(Customer_ID, Customer_Name, Customer_Password, Customer_Phone) VALUES ( 'KH' + Cast((Cast((select SUBSTRING(Max(Customer_ID), 3,LEN(Max(Customer_ID))) from Customer) AS INT) + 1) as varchar), '" + UserName+"', '"+Password+"','"+PhoneNumber+"')";
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = connection;
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        connection.Close();

    }
    public void GetFoodFromDB(string price, string speed, string rating, int category)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string sql = "select Top 5  MenuItem_ID, MenuItem_Name, MenuItem_Image, MenuItem_Price, MenuItem_Calo, MenuItem_Rating, MenuItem_Speed, MenuItem_Category, MenuItem_Unit, MenuItems_Recipe from MenuItems where MenuItem_Price " + price + " and " + speed + " and " + rating + " and MenuItem_Category = " + category + "";
        //string sql = "select Top 5 MenuItem_ID, MenuItem_Name, MenuItem_Image, MenuItem_Price, MenuItem_Calo, MenuItem_Rating, MenuItem_Speed, MenuItem_Category, MenuItem_Unit from MenuItems where MenuItem_Price <= 600000 and (MenuItem_Speed >= 1 and MenuItem_Speed <= 9) and (MenuItem_Rating <=5 and MenuItem_Rating >= 1) and MenuItem_Category = 2";
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

                    byte[] imageData = (byte[])reader["MenuItem_Image"];
                    Texture2D texture = new Texture2D(1, 1);
                    texture.LoadImage(imageData);
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                    long Price = reader.GetInt64(3);
                    double Calo = reader.GetDouble(4);
                    double Rating = reader.GetDouble(5);
                    int Speed = reader.GetInt32(6);
                    int Category = reader.GetInt32(7);
                    string Unit = reader.GetString(8);
                    string Recipe;
                    try
                    {
                        Recipe = reader.GetString(9);
                        Recipe = Recipe.Substring(3, Recipe.Length - 8);
                    }
                    catch (Exception)
                    {
                        Recipe = "";
                    }


                    Item newItem = new Item();

                    newItem.initDataItem(ID, Name, sprite, Price, (float)Rating, (float)Calo, Speed, Category, Unit, Recipe);
                    SelectController.instance.Items.Add(newItem);
                }
            }
        }
        connection.Close();
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
                    connection.Close();
                }
            }
        }
        return 0;
    }
}
