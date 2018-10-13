using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    int count = 0;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            ConsoleProDebug.LogToFilter("test", "rwar");
            Debug.Log("test");
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start ()
    {
		
	}

	void Update ()
    {
        count++;

        ConsoleProDebug.Watch("Count", count.ToString());

        //Debug.Log("Player X Position:" + transform.position.x.ToString() + "\nCPAPI:{"cmd":"Watch" "name":"" + "PXPos" + ""}");
	}
}
