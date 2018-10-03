using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour {

    // You can use SerializeField to drag and drop in inspector
    // Or you can just have it look for the object on start using GetComponent or FindObjectOfType
    [SerializeField]
    private City city;

    [SerializeField]
    private UIController uiController;

    [SerializeField]
    private Building[] buildings;

    [SerializeField]
    private Board board;

    private Building selectedBuilding;

    int ACTION_ADD = 0;
    int ACTION_REMOVE = 1;

    int LEFT_MOUSE_BTN = 0;
    int RIGHT_MOUSE_BTN = 1;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(LEFT_MOUSE_BTN) && Input.GetKey(KeyCode.LeftShift) && selectedBuilding != null)
        {
            InteractWithBoard(ACTION_ADD);
        }
		else if (Input.GetMouseButtonDown(LEFT_MOUSE_BTN) && selectedBuilding != null)
        {
            InteractWithBoard(ACTION_ADD);
        }

        if (Input.GetMouseButtonDown(RIGHT_MOUSE_BTN))
        {
            InteractWithBoard(ACTION_REMOVE);
        }
	}

    void InteractWithBoard(int action)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // out forces a pass by reference, even on primitive types like int
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 gridPosition = board.CalculateGridPosition(hit.point);
            // Make sure you can't place a building by clicking on top of a button or if another building there
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                if (action == ACTION_ADD && board.CheckForBuildingAtPosition(gridPosition) == null)
                {
                    if (city.Cash >= selectedBuilding.cost)
                    {
                        city.DepositCash(-selectedBuilding.cost);
                        uiController.UpdateCityData();
                        city.buildingCounts[selectedBuilding.id]++;
                        board.AddBuilding(selectedBuilding, gridPosition);
                    }
                }
                else if (action == ACTION_REMOVE && board.CheckForBuildingAtPosition(gridPosition) != null)
                {
                    city.DepositCash(board.CheckForBuildingAtPosition(gridPosition).cost / 2);
                    board.RemoveBuilding(gridPosition);
                    uiController.UpdateCityData();
                }
            }
        }
    }

    public void EnableBuilder(int building)
    {
        selectedBuilding = buildings[building];
        Debug.LogFormat("Selected building {0}", selectedBuilding.buildingName);
    }
}
