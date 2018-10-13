using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	
	public enum PlayerState
    {
        idle,
        walking,
        running,
        jump,
        inAirUp,
        inAirDown,
        damaged
    };

    PlayerState ps;

	void Start () 
	{
        ps = PlayerState.idle;
        ConsoleProDebug.Watch("Player State", ps.ToString());
    }
	
	void Update () 
	{
        StateMachine();
	}

    public void StateChange(PlayerState state)
    {
        ps = state;
        ConsoleProDebug.Watch("Player State", ps.ToString());
    }

    private void StateMachine()
    {
        switch(ps)
        {
            case (PlayerState.idle):
                Idle();
                break;
            case (PlayerState.walking):
                Walking();
                break;
            case (PlayerState.running):
                Running();
                break;
            case (PlayerState.jump):
                Jump();
                break;
            case (PlayerState.inAirUp):
                InAirUp();
                break;
            case (PlayerState.inAirDown):
                InAirDown();
                break;
            case (PlayerState.damaged):
                Damaged();
                break;
        }
    }

    private void Idle()
    {

    }

    private void Walking()
    {

    }

    private void Running()
    {

    }

    private void Jump()
    {

    }

    private void InAirUp()
    {

    }

    private void InAirDown()
    {

    }

    private void Damaged()
    {

    }
}
