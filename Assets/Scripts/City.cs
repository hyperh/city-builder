using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {
    // get and set will access a private int called Cash
    public int Cash { get; set; }

    public int Day{ get; set; }
    public float PopulationCurrent { get; set; }
    public float PopulationCeiling { get; set; }
    public int JobsCurrent { get; set; }
    public int JobsCeiling { get; set; }
    public float Food { get; set; }

    public int[] buildingCounts = new int[4];

    private UIController uiController;

    private int HOUSES_INDEX = 1;
    private int FARMS_INDEX = 2;
    private int FACTORIES_INDEX = 3;

    // Use this for initialization
    void Start () {
        uiController = GetComponent<UIController>();
        
        Cash = 5000000;
	}
	
	public void EndTurn()
    {
        Day++;
        CalculateCash();
        CalculatePopulation();
        CalculateJobs();
        CalculateFood();
        
        uiController.UpdateCityData();
        uiController.UpdateDayCount();

        Debug.Log("Day Ended");
        Debug.LogFormat("Jobs {0}/{1}, Cash: {2}, Pop: {3}/{4}, Food: {5}",
            JobsCurrent, JobsCeiling, Cash, PopulationCurrent, PopulationCeiling, Food);
    }

    void CalculateJobs()
    {
        JobsCeiling = buildingCounts[FACTORIES_INDEX] * 10;
        JobsCurrent = Mathf.Min((int) PopulationCurrent, JobsCeiling);
    }

    void CalculateCash()
    {
        Cash += JobsCurrent * 2;

    }

    public void DepositCash(int cash)
    {
        Cash += cash;

    }

    void CalculateFood()
    {
        Food += buildingCounts[FARMS_INDEX] * 4f;
    }

    void CalculatePopulation()
    {
        PopulationCeiling = buildingCounts[HOUSES_INDEX] * 5;
        if (Food >= PopulationCurrent && PopulationCurrent < PopulationCeiling)
        {
            Food -= PopulationCurrent * 0.25f;
            PopulationCurrent = Mathf.Min(PopulationCurrent += Food * 0.25f, PopulationCeiling);
        }
        else if (Food < PopulationCurrent)
        {
            PopulationCurrent -= (PopulationCurrent - Food) * 0.5f;
        }

    }
}
