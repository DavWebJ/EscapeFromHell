using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace BlackPearl
{
    

    public class PanelItemInfos : MonoBehaviour
    {
        [SerializeField] private Slot slotItem = null;
        // [SerializeField] private Image itemIcon;
        [SerializeField] private Text itemNametext;
        [SerializeField] private Text itemDesctext;

        [Header("panel recipe craft")]
        // [SerializeField] private GameObject panelRecipeCraft = null;
        // [SerializeField] private Transform GridRecipePanelCraft = null;
        // [SerializeField] private GameObject buttonCraftPossible;
        // [SerializeField] private GameObject buttonCraftImpossible;
        private Slot lastSlot = null;

        public void ShowItemInfos(Slot slot)
        {
            if(slot == null || slot.currentItem == null)
            {
                HideItemInfos();
                return;
            }

            gameObject.SetActive(true);

            if(lastSlot != null && lastSlot == slot)
            {
                HideItemInfos();
                return;
            }
            lastSlot = slot;
            slotItem.slotType = slot.slotType;
            slotItem.ChangeItem(slot.currentItem);
            itemDesctext.text = slot.currentItem.ItemDescription;
            itemNametext.text = slot.currentItem.ItemName;
            // itemIcon.sprite = slot.currentItem.ItemIcon;
        //    StartCoroutine(ShowPanel_Crafting());
            
        }
        public void HideItemInfos()
        {
            if(gameObject.activeInHierarchy){
                slotItem.ChangeItem(null);
                itemDesctext.text = "";
                itemNametext.text = "";
                // itemIcon.sprite = null;
            }
            lastSlot = null;
            // buttonCraftImpossible.SetActive(false);
            // buttonCraftPossible.SetActive(false);
            gameObject.SetActive(false);
        }


        #region panel craft under item infos
        // public IEnumerator ShowPanel_Crafting()
        // {
        //     Inventory.instance.DestroyAllSlots(GridRecipePanelCraft);

        //     yield return new WaitForSeconds(0.1f);
        //     if(lastSlot != null && lastSlot.currentItem != null && lastSlot.slotType == SlotType.Craft && lastSlot.currentItem.crafting.isCraftable)
        //     {
        //         panelRecipeCraft.SetActive(true);

        //         for (int i = 0; i < slotItem.currentItem.crafting.recipeCraftItems.Length; i++)
        //         {
        //             Slot s = Inventory.instance.CreateCraftSlot(GridRecipePanelCraft);
        //             s.itemAmount.fontSize = 20;
        //             s.slotType = SlotType.Required;
        //             s.GetComponent<Image>().raycastTarget = false;
        //             s.isDraggable = false;
        //             Item requiredItem = GameManager.instance.resources.GetitemByName(slotItem.currentItem.crafting.recipeCraftItems[i].CraftItemName);

        //             if(requiredItem != null)
        //             {
        //                 requiredItem.amount = slotItem.currentItem.crafting.recipeCraftItems[i].amountRequired;
        //                 s.ChangeItem(requiredItem);
        //             }
        //         }
        //     }else
        //     {
        //         panelRecipeCraft.SetActive(false);
        //     }

        //     UpdateCraftInfos();

        // }

        public void UpdateCraftInfos()
        {
            // if(panelRecipeCraft.activeInHierarchy)
            // {
            //     if(GridRecipePanelCraft.childCount > 0)
            //     {
            //         List<bool> canCraftObject = new List<bool>();
            //         for (int i = 0; i < GridRecipePanelCraft.childCount; i++)
            //         {
            //             Slot s = GridRecipePanelCraft.GetChild(i).GetComponent<Slot>();
            //             s.UpdateSlot();

            //             canCraftObject.Add(Inventory.instance.ReturnsAmountRequiredForCraft(s.currentItem.ItemName) >= s.currentItem.amount);
            //         }
            //         buttonCraftPossible.SetActive(canCraftObject.TrueForAll(p=> p == true));
            //         buttonCraftImpossible.SetActive(canCraftObject.TrueForAll(p=> p == false));
            //     }
            // }
        }
        #endregion

        #region event craft
        public void CraftEventButton()
        {
            // Inventory.instance.panel_craft.CraftNewObject(Instantiate(lastSlot.currentItem));
        }
        #endregion

    }
}
