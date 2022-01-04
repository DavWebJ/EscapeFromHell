using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace BlackPearl
{
    

    public class PanelCraft : MonoBehaviour
    {
        public Transform gridCraft = null;
        
        private Transform gridQ = null;

        public void Init()
        {
            gridCraft = transform.Find("GridCraft/Viewport/Content");
            gridQ = transform.Find("GridQueue");

            Item[] itemDataBase = Resources.LoadAll<Item>("Inventory/Item");
            if(itemDataBase.Length > 0)
            {
                for (int i = 0; i < itemDataBase.Length; i++)
                {
                    if(itemDataBase[i].crafting.isCraftable == true)
                    {
                       Slot slot = Inventory.instance.CreateCraftSlot(gridCraft);
                       Item itemCrafted = Instantiate(itemDataBase[i]);
                       slot.slotType = SlotType.Craft;
                       slot.isDraggable = false;
                       itemCrafted.amount = itemDataBase[i].crafting.resultQuantity;
                       slot.ChangeItem(itemCrafted);
                       
                    }
                }
            }
           
        }

        public void HidePanel()
        {

            gameObject.SetActive(false);

        }

        public void CraftNewObject(Item item)
        {
            if(item== null){
                return;
            }

            Slot s = Inventory.instance.CreateCraftSlot(gridQ);
            s.isDraggable = false;
            s.slotType = SlotType.CraftQ;
            s.ChangeItem(item);

            s.recipeCraftItems = item.crafting.recipeCraftItems;
            List<Slot> allSlots = Inventory.instance.GetAllSlots();
            if(allSlots.Count <= 0)
                return;

            for (int i = 0; i < s.recipeCraftItems.Length; i++)
            {
                for (int g = 0; g < s.recipeCraftItems[i].amountRequired; g++)
                {
                    Slot slotFound = allSlots.FirstOrDefault(p => p.currentItem != null && p.currentItem.ItemName == s.recipeCraftItems[i].CraftItemName);
                    if(slotFound != null)
                    {
                        slotFound.DeleteItem();
                    }
                }
            }

            Inventory.instance.panel_infos.UpdateCraftInfos();
        }

        public bool CheckSlotQueueCrafting(Slot slot)
        {
            return (gridQ.childCount > 0 && gridQ.GetChild(0).GetComponent<Slot>() == slot);
            

            
        }
    }
}
