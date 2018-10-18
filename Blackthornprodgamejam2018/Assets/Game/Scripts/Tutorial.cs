using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour 
{

    public GameObject tutorial;
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Return))
        {
            tutorial.SetActive(false);
            GameManager.instance.StartGameMode().StartCountDown();
        }
	}
}
