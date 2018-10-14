﻿using System.Collections;
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

	void Start () 
	{
        if (startOnLoad)
            Init();
	}
	
	void Update () 
	{
        if(runUpdate)
        {
            Controls();
            SubtractPoints();
            UpdateBar();
            IncreaseDifficulty();
        }
	}

    public void Init()
    {
        runUpdate = true;
        startTime = Time.time;
        GameObject holdBar = Instantiate(mashBar, mashBarOrigin);
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
}
