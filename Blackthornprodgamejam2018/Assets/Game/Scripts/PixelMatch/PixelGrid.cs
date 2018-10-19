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

    [System.Serializable]
    public struct Pixel
    {
        public int colorNum;
        public Material mat;
        public GameObject go;
        public PixelCube pc;
    }
    
    private Pixel[,] pixels;
    private Pixel[] colorPicker;

    private bool runUpdate;

    public enum Colors
    {
        Grey,
        DarkBlue,
        LightBlue,
        DarkGreen,
        LightGreen,
        DarkRed,
        LightRed,
        Pink,
        Yellow,
        White,
        Black
    }

    Colors color = Colors.Grey;
    Colors chosenColor = Colors.Black;
    
    public enum SpriteName
    {
        Naut,
        Eye,
        EyeBall
    }

    public SpriteName currentSprite = SpriteName.Naut;

    [System.Serializable]
    public struct DrawSprite
    {
        public int x;
        public int y;
        public SpriteName sn;
        public Sprite sprite;
        public Pixel[,] pixel;
    }

    public int[,] nautCheck  = { { 0,0,4,4,4,0,0,0,0,0,0,0 }, {0,0,0,0,4,0,4,4,4,0,0,0}, {4,4,4,4,3,4,4,8,4,0,0,3}, {0,0,4,3,4,4,4,8,4,3,3,3}, {4,4,3,4,4,4,4,8,4,0,0,3}, {0,0,0,0,4,0,4,8,4,0,0,8}, {0,0,4,4,4,0,0,0,0,0,0,0} };
    public int[,] eyeCheck = { { 0,0,0,7,9,9,9,9,0,0,0 }, { 0,0,9,9,5,5,5,9,7,0,0 }, { 0, 9, 9, 5, 6, 6, 6, 5, 9, 9, 0 }, { 9, 9, 5, 6, 6, 10, 6, 6, 5, 9, 9 }, { 9, 9, 5, 6, 10, 10, 10, 6, 5, 9, 9 }, { 9, 9, 5, 6, 6, 10, 6, 6, 5, 9, 9 }, { 9, 7, 9, 5, 6, 6, 6, 5, 9, 7, 9 }, { 7, 9, 9, 9, 5, 5, 5, 9, 9, 9, 7 }, { 0, 9, 9, 9, 9, 9, 9, 7, 9, 9, 0 }, { 0, 0, 9, 9, 9, 9, 7, 9, 9, 0, 0 }, { 0, 0, 0, 9, 9, 7, 9, 9, 0, 0, 0 } };
    public int[,] eyeBallCheck = { { 0,0,0,7,9,9,9,9,0,0,0 }, { 0,0,9,9,1,1,1,9,7,0,0}, { 0,9,9,1,2,2,2,1,9,9,0}, {9,9,1,2,2,10,2,2,1,9,9}, {9,9,1,2,10,10,10,2,1,9,9}, {9,9,1,2,2,10,2,2,1,9,9}, {9,7,9,1,2,2,2,1,9,7,9}, {7,9,9,9,1,1,1,9,9,9,7}, {0,9,9,9,9,9,9,7,9,9,0}, {0,0,9,9,9,9,7,9,9,0,0}, {0,0,0,9,9,7,9,9,0,0,0} };

    public DrawSprite[] drawSprite;
    
    private RaycastHit rayHit;

    int amountOfColors;

	void Awake () 
	{
        amountOfColors = System.Enum.GetNames(typeof(Colors)).Length;
        if(StartOnLoad)
        {
            Init();
        }
	}

    void Update()
    {
        if(runUpdate)
        {
            if (Input.GetMouseButton(0))
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
    }

    public void StartGame()
    {
        Init();
    }

    private void Init()
    {
        for(int i = 0; i < drawSprite.Length; i++)
        {
            if(drawSprite[i].sn == currentSprite)
            {
                SpawnPixelGrid(drawSprite[i].x, drawSprite[i].y);
            }
        }
        SpawnColorPicker();
        Resize();
        runUpdate = true;
    }

    public void SpawnPixelGrid()
    {
        pixels = new Pixel[gridX, gridY];
        InitGrid();
    }

    public void SpawnPixelGrid(int x, int y)
    {
        gridX = x;
        gridY = y;
        pixels = new Pixel[gridX, gridY];
        InitGrid(gridX, gridY);
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

    private void InitGrid(int x, int y)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
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

        //use this if you need to hard code another sprite
        /*
        string copyPaste = "";
        for (int i = 0; i < gridX; i++)
        {
            copyPaste += "{";
            for (int j = 0; j < gridY; j++)
            {
                if (j != gridY)
                    copyPaste += pixels[i, j].colorNum + ",";
                else
                    copyPaste += pixels[i, j].colorNum;
            }
            copyPaste += "}, ";
        }

        Debug.Log(copyPaste);
        */
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

    public void CheckDrawing()
    {
        bool correct = false;

        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                if(currentSprite == SpriteName.Naut)
                {
                    if(pixels[i,j].colorNum == nautCheck[i,j])
                    {
                        continue;
                    }
                    else
                    {
                        correct = false;
                    }
                }
                if(currentSprite == SpriteName.Eye)
                {
                    if (pixels[i, j].colorNum == eyeCheck[i, j])
                    {
                        continue;
                    }
                    else
                    {
                        correct = false;
                    }
                }
                if(currentSprite == SpriteName.EyeBall)
                {
                    if (pixels[i, j].colorNum == eyeBallCheck[i, j])
                    {
                        continue;
                    }
                    else
                    {
                        correct = false;
                    }
                }
            }
        }
    }

    IEnumerator ChangeColorOnInit(Pixel pix, Material mat)
    {
        yield return new WaitForEndOfFrame();
        pix.pc.SetMat(mat);
    }
}
