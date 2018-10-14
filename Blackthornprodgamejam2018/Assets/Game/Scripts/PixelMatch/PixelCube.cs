using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelCube : MonoBehaviour 
{
    [SerializeField]
    private Renderer mat;

    void Start()
    {
        mat = GetComponent<Renderer>();
    }

    public void SetMat(Material newMat)
    {
        mat.material = newMat;
    }
}
