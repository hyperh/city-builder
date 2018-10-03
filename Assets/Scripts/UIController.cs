using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    // SerializeField will expose private members to the Unity inspector, so we can put in references to the actual text
    [SerializeField]
    private Text dayText;
    [SerializeField]
    private Text cityText;

    private City city;

	// Use this for initialization
	void Start () {
        // Will look through all objects in scene
        // city = FindObjectOfType<City>();

        // Only looks for components attached to object
        // Since City and UIController are on the same object, we can look for them there
        city = GetComponent<City>();
	}
	
    public void UpdateCityData()
    {
        cityText.text = string.Format
            ("Jobs {0}/{1}\nCash: ${2} (+${6})\nPopulation: {3}/{4}\nFood: {5}",
            city.JobsCurrent, city.JobsCeiling, city.Cash, (int) city.PopulationCurrent, (int) city.PopulationCeiling, (int) city.Food, city.JobsCurrent * 2);
    }

    public void UpdateDayCount()
    {
        dayText.text = string.Format("Day {0}", city.Day);
    }
}
