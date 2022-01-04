using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class NavigationBuildEvent : MonoBehaviour {


    [SerializeField] GameObject firstPanelBuild;
    [SerializeField] GameObject CanvasBuild;
    public EventSystem Build_Event;


    
    void Start () {
        firstPanelBuild = Build_Event.firstSelectedGameObject;
    }

    private void OnEnable()
    {
        Build_Event.firstSelectedGameObject = firstPanelBuild;
    }
    private void OnDisable()
    {
        Build_Event.firstSelectedGameObject = firstPanelBuild;
    }

    void Update () {
        CheckBuildingCanvas();
	}

    public void CheckBuildingCanvas()
    {
        if (CanvasBuild.activeSelf)
        {
            if (Build_Event.currentSelectedGameObject != firstPanelBuild)
            {


                if (Build_Event.currentSelectedGameObject == null)
                {
                    Build_Event.SetSelectedGameObject(firstPanelBuild);
                }
                else
                {
                    firstPanelBuild = Build_Event.currentSelectedGameObject;
                }
            }


        }



    }

}
