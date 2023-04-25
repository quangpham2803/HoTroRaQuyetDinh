using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;

public class SQL : MonoBehaviour
{
    string connectionString = "Data Source=MSI;Initial Catalog=DES_Restaurant;Integrated Security=True";
    
    // Start is called before the first frame update
    void Start()
    {
       
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

}
