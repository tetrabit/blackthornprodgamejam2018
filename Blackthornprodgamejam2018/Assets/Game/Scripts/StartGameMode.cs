using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameMode : MonoBehaviour 
{
    public enum CurrentGame
    {
        ButtonMash,
        PixelGrid,
        MetaGame
    }

    public CurrentGame currentGame = CurrentGame.ButtonMash;

    bool animationEnded = false;

    CountDown countDown;

    public void AnimationEnded()
    {
        switch (currentGame)
        {
            case (CurrentGame.ButtonMash):
                GameManager.instance.ButtomMash().StartGame();
                break;
            case (CurrentGame.PixelGrid):
                GameManager.instance.ButtomMash().StartGame();
                break;
            case (CurrentGame.MetaGame):
                break;
        }
    }

    public void SetCountDown(CountDown cd)
    {
        countDown = cd;
    }

    public void StartCountDown()
    {
        if (countDown != null)
            countDown.StartCountDown();
    }
}
