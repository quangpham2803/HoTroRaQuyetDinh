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
    public void GetSomeThing()
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string sql = "select Customer_Name, Customer_Password from Customer";
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
                    Debug.Log(Username.ToString() +"    "+ Password.ToString());
                }
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
