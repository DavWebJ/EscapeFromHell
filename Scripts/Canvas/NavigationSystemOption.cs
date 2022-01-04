using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using BlackPearl;
public class NavigationSystemOption : MonoBehaviour
{
    [Header("inventory canvas events systeme")]
    [SerializeField] private GameObject firstSlots,lastSlots;
    public static NavigationSystemOption instance = null;



    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
    }
   
  

    private void OnEnable()
    {
        // EventSystem.current.SetSelectedGameObject(firstSlots);
       
    }

    private void OnDisable() {
        // EventSystem.current.SetSelectedGameObject(NaviguationSystemCanvas.instance.firstInventorySlots);
    }

    // public void checkFirstSelected()
    // {
    //     EventSystem.current.SetSelectedGameObject(lastSlots == null ? firstSlots : lastSlots);
    // }
}
