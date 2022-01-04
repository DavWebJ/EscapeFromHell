using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using BlackPearl;
public class  NaviguationSystemHotbar : MonoBehaviour {

    [Header("events hotbar systeme")]
   public GameObject firstInventorySlots,lastInventorySlots;
    public static NaviguationSystemHotbar instance = null;



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

    // public void checkFirstSelected(Slot slot)
    // {
    //     if(slot != null)
    //     {
    //         firstInventorySlots = slot.gameObject;
    //     }
        
    //     EventSystem.current.SetSelectedGameObject(lastInventorySlots == null ? firstInventorySlots : lastInventorySlots);

    //     EventSystem.current.currentSelectedGameObject.GetComponent<Slot>();
        
    // }

    // public void checkLastSelected()
    // {
    //     Slot slot;
        
    //     slot = HotBar.instance.GetCurrentSlot();
    //    lastInventorySlots = slot.gameObject;
    //    EventSystem.current.SetSelectedGameObject(lastInventorySlots);
     
    // }
  

}

