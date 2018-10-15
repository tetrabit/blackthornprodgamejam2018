using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bugs : MonoBehaviour 
{
    private int bugs = 0;

    public int GetBugs(){ return bugs; }

    public void AddBugs()
    {
        bugs++;
    }

    public void AddBugs(int add)
    {
        bugs += add;
    }
}
