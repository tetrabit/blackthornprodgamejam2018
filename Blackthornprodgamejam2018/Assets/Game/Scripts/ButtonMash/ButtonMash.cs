using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMash : MonoBehaviour 
{
    public bool startOnLoad;
    private bool runUpdate;

    public GameObject mashBar;

    [Header("MUST BE INSIDE A CANVAS OBJECT")]
    public Transform mashBarOrigin;
    private Slider mashBarSlider;

    [Space]
    public AnimationCurve difficultyRamp;

    public float startingDifficulty;
    public float pointsPerMash;

    public float maxPoints;
    public float difficulty;
    public float mashPoints;

    private float startTime;
    public float endTime;

    private bool gameOver;

	void Start () 
	{
        if (startOnLoad)
        {
            Init(mashBar);
        }
	}
	
	void Update () 
	{
        if(runUpdate)
        {
            if(!gameOver)
            {
                Controls();
                SubtractPoints();
                UpdateBar();
                CheckForFailure();
                IncreaseDifficulty();
                IncrementTime();
            }
        }
	}

    public void StartGame()
    {
        runUpdate = true;
        Init(mashBar);
        mashBar.SetActive(true);
    }

    public void Init()
    {
        runUpdate = true;
        startTime = Time.time;
        GameObject holdBar = Instantiate(mashBar, mashBarOrigin);
        mashBarSlider = holdBar.GetComponent<Slider>();
    }

    public void Init(GameObject sceneObject)
    {
        runUpdate = true;
        startTime = Time.time;
        GameObject holdBar = sceneObject;
        mashBarSlider = holdBar.GetComponent<Slider>();
    }

    private void Controls()
    {
        if (Input.anyKeyDown)
        {
            AddPoints();
        }
    }

    private void AddPoints()
    {
        if(mashPoints < maxPoints)
        {
            mashPoints += pointsPerMash;
        }
        else
        {
            mashPoints = maxPoints;
        }
    }

    private void IncrementTime()
    {
        endTime -= Time.deltaTime;
        if (endTime <= 0)
            gameOver = true;
    }

    private void SubtractPoints()
    {
        mashPoints -= Time.deltaTime * difficulty;
    }

    private void UpdateBar()
    {
        mashBarSlider.value = (Mathf.Clamp(mashPoints, 0, maxPoints)/maxPoints);
    }

    private void IncreaseDifficulty()
    {
        difficulty = startingDifficulty * difficultyRamp.Evaluate(Time.time - startTime);
    }

    private void CheckForFailure()
    {
        if(mashBarSlider.value < .25)
        {
            GameManager.instance.GoalCompletion().ButtonMash(true);
        }
    }
}
