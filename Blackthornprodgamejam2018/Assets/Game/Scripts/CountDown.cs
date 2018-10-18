using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour 
{

    private Animator anim;

    private void Start()
    {
        GameManager.instance.StartGameMode().SetCountDown(this);
        anim = GetComponent<Animator>();
    }

    public void AnimationEnded()
    {
        GameManager.instance.StartGameMode().AnimationEnded();
        Destroy(gameObject);
    }

    public void StartCountDown()
    {
        if(anim != null)
        {
            anim.Play("CountDown");
        }
    }
}
