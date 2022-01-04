using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using BlackPearl;
public class  NaviguationSystemCanvas : MonoBehaviour {

    [Header("inventory canvas events systeme")]
   public GameObject firstInventorySlots,lastInventorySlots;
    public static NaviguationSystemCanvas instance = null;



    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
    }
   
  

    private void OnEnable()
    {
        // EventSystem.current.SetSelectedGameObject(firstInventorySlots);
        // if(Inventory.instance.isInventoryOpen)
        // {
        //     Slot slot;
        //     slot = EventSystem.current.currentSelectedGameObject.GetComponent<Slot>();
        //     Inventory.instance.panel_options.ShowOption(slot);
        // }

       
    }

    private void OnDisable() {
        // EventSystem.current.SetSelectedGameObject(lastInventorySlots == null ? firstInventorySlots : lastInventorySlots);
    }

    // public void checkFirstSelected()
    // {
    //     EventSystem.current.SetSelectedGameObject(lastInventorySlots == null ? firstInventorySlots : lastInventorySlots);
    //     Slot slot;
    //     slot = EventSystem.current.currentSelectedGameObject.GetComponent<Slot>();
    //     Inventory.instance.panel_options.ShowOption(slot);
    // }

    // public void checkLastSelected()
    // {
    //     Slot slot;
        
    //     slot = Inventory.instance.panel_options.slotSelected;
    //    lastInventorySlots = slot.gameObject;
    //    EventSystem.current.SetSelectedGameObject(lastInventorySlots);
    //     Inventory.instance.panel_options.ShowOption(slot);
    // }
  

}

