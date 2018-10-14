using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private Player p;
    private PixelGrid pg;
    private ButtonMash bm;

    void Awake()
    {
        CreateSingleton();
        GetManagers();
    }

    private void CreateSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void GetManagers()
    {
        p = GetComponent<Player>();
        pg = GetComponent<PixelGrid>();
        bm = GetComponent<ButtonMash>();
    }

    public Player Player() { return p; }
    public PixelGrid PixelGrid() { return pg; }
    public ButtonMash ButtomMash() { return bm; }
}
