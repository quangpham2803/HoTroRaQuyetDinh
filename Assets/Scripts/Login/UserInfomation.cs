using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfomation : MonoBehaviour
{
    public string ID;
    public string name;
    public string password;

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void OnLevelWasLoaded(int level)
    {
        GameObject[] User = GameObject.FindGameObjectsWithTag("User");

        if (User.Length > 0)
        {
            Destroy(User[1]);
        }    
    }
}
