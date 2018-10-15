using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PixelGrid : MonoBehaviour
{
    public bool StartOnLoad;

    public GameObject pixelCube;
    public Transform pixelGridOrigin;

    public int gridX, gridY;

    public struct Pixel
    {
        public int colorNum;
        public Material mat;
        public GameObject go;
        public PixelCube pc;
    }
    
    private Pixel[,] pixels;
    private Pixel[] colorPicker;

    public enum Colors
    {
        Black,
        Blue,
        Green,
        Grey,
        Orange,
        Purple,
        Red,
        White,
        Yellow
    }

    Colors color = Colors.Grey;
    Colors chosenColor = Colors.Black;

    private RaycastHit rayHit;

    int amountOfColors;

	void Awake () 
	{
        amountOfColors = System.Enum.GetNames(typeof(Colors)).Length;
        if(StartOnLoad)
        {
            SpawnPixelGrid();
            SpawnColorPicker();
            Resize();
        }
	}

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit))
            {
                if (rayHit.collider.tag == "PixelCube")
                {
                    FindCube(rayHit.collider.gameObject);
                }
                if (rayHit.collider.tag == "ColorPicker")
                {
                    PickColor(rayHit.collider.gameObject);
                }
            }
        }
    }

    public void SpawnPixelGrid()
    {
        pixels = new Pixel[gridX, gridY];
        InitGrid();
    }

    public void SpawnPixelGrid(float x, float y)
    {
        pixels = new Pixel[gridX, gridY];
        pixelGridOrigin.position = new Vector3(x, y, 0);
        InitGrid();
    }

    public void SpawnColorPicker()
    {
        colorPicker = new Pixel[amountOfColors];
        InitColorPicker();
    }

    private void InitGrid()
    {
        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                pixels[i, j].go = Instantiate(pixelCube, new Vector3(i + 0.5f, j + 0.5f, 0) + pixelGridOrigin.position, Quaternion.Euler(0, 0, 0), pixelGridOrigin);
                pixels[i, j].go.tag = "PixelCube";
                pixels[i, j].pc = pixels[i, j].go.GetComponent<PixelCube>();
                pixels[i, j].colorNum = (int)color;
                pixels[i, j].mat = GetColor(Colors.Grey);
            }
        }
    }

    private void InitColorPicker()
    {
        for (int i = 0; i < amountOfColors; i++)
        {
            colorPicker[i].go = Instantiate(pixelCube, new Vector3(gridX + 1.0f, i + 0.5f, 0) + pixelGridOrigin.position, Quaternion.Euler(0, 0, 0), pixelGridOrigin);
            colorPicker[i].go.tag = "ColorPicker";
            colorPicker[i].pc = colorPicker[i].go.GetComponent<PixelCube>();
            colorPicker[i].colorNum = i;
            colorPicker[i].mat = GetColor((Colors)i);
            StartCoroutine(ChangeColorOnInit(colorPicker[i], GetColor((Colors)i)));
        }
    }

    public void Resize()
    {
        pixelGridOrigin.localScale = new Vector3(0.55f, 0.55f, 0.55f);
    }

    private void FindCube(GameObject cube)
    {
        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                if(pixels[i, j].go == cube)
                {
                    pixels[i, j].pc.SetMat(GetColor(chosenColor));
                    pixels[i, j].colorNum = (int)chosenColor;
                }
            }
        }
    }

    private void PickColor(GameObject cube)
    {
        for (int i = 0; i < amountOfColors; i++)
        {
            if (colorPicker[i].go == cube)
            {
                ChoseColor((Colors)colorPicker[i].colorNum);
            }
        }
    }

    public void ChoseColor(Colors newColor)
    {
        chosenColor = newColor;
    }

    private Material GetColor(Colors color)
    {
        return (Material)AssetDatabase.LoadAssetAtPath("Assets/Game/Materials/PixelCubeColors/" + color + ".mat", typeof(Material));
    }

    IEnumerator ChangeColorOnInit(Pixel pix, Material mat)
    {
        yield return new WaitForEndOfFrame();
        pix.pc.SetMat(mat);
    }
}
