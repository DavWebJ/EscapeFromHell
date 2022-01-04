using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlackPearl
{
    
    public class PanelOptions : MonoBehaviour
    {
        private Transform gridButtons = null;
        public Slot slotSelected = null;

        [Header("Buttons:")]
        [SerializeField] private GameObject btn_drop;
        [SerializeField] private GameObject btn_dropall;
        [SerializeField] private GameObject btn_split;
        [SerializeField] private GameObject btn_use_item;
        // [SerializeField] private GameObject btn_equiped_tools;
        // [SerializeField] private GameObject btn_equiped_weapon;
        [SerializeField] private GameObject btn_heal;
        public void Init()
        {
            gridButtons = transform.Find("GridButtons");
            HideOption();

        }

        public void AddItemSAfterSplit(Item item)
        {
            if(item == null || item.amount <= 0)
            {
                return;
            }

            List<Slot> List = Inventory.instance.GetSlots(Inventory.instance.panelBackPack.grid);

            if(List.Count <=0)
            {
                return;
            }

            Slot slotFound = List.FirstOrDefault(p => p.currentItem == null);

            if(slotFound == null)
            {
                print("full inventory");
                Inventory.instance.player.fpscam.DropObject(item);
                return;
            }
            slotFound.ChangeItem(item);
        }

        public void ShowOption(Slot slot)
        {
            if(slot == null || slot.currentItem == null)
            {
                return;
            }

            slotSelected = slot;
            
            gameObject.SetActive(true);
            btn_drop.SetActive(slotSelected.currentItem.amount >= 1);
            btn_dropall.SetActive(slotSelected.currentItem.amount > 1);
            btn_split.SetActive(slotSelected.currentItem.amount >= 2);
            // btn_equiped_weapon.SetActive(slotSelected.currentItem.itemType == ItemType.Weapon);
            btn_use_item.SetActive(slotSelected.currentItem.amount >= 1 && slotSelected.currentItem.itemType == ItemType.Consumables && slotSelected.currentItem.attributes.GetActions("Pills") != null);
            btn_heal.SetActive(slotSelected.currentItem.amount >= 1 && slotSelected.currentItem.itemType == ItemType.Consumables && slotSelected.currentItem.attributes.GetActions("Heal") != null && Inventory.instance.player.playerVitals.health < Inventory.instance.player.playerVitals.healthMax);
            // btn_equiped_tools.SetActive(slotSelected.currentItem.itemType == ItemType.Tools);
            // btn_drink.SetActive(slotSelected.currentItem.attributes.GetActions("Drink") != null && slotSelected.currentItem.attributes.value > 0);
            gridButtons.position = slot.transform.position;
            slotSelected.set_SelectedImage(true);
        }
        public void HideOption()
        {
            if(slotSelected != null)
            {
                slotSelected.set_SelectedImage(false);
            }
            slotSelected = null;
            if(Inventory.instance.panel_infos != null)
            {
                Inventory.instance.panel_infos.UpdateCraftInfos();
            }
            gameObject.SetActive(false);
        }

        public void HidePanelOption(bool active)
        {
            // slotSelected.set_SelectedImage(false);
            slotSelected = null;
            gameObject.SetActive(active);
        }
        public void EventBtnHotbar(Slot _slot, string nameOptions)
        {
            if(_slot == null || _slot.currentItem == null || _slot.currentItem.amount <= 0)
            {
                return;
            }
            slotSelected = _slot;
            EventBtn(nameOptions);

        }


        public void EventBtn(string nameOptions)
        {
            if(nameOptions == string.Empty || slotSelected == null || slotSelected.currentItem == null)
            {
                HideOption();
                return;
            }
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            switch (nameOptions)
            {
                case "Drop":
                    Item itemDrop = Instantiate(slotSelected.currentItem);
                    itemDrop.amount = 1;
                    Inventory.instance.player.fpscam.DropObject(itemDrop);
                    slotSelected.DeleteItem();
                    HUD.instance.SetVisualMessage(false,itemDrop);
                    
             
                break;
                case "DropAll":
                    int total = slotSelected.currentItem.amount;
                    for (int i = 0; i < total; i++)
                    {
                        Item itemAll = Instantiate(slotSelected.currentItem);
                        itemAll.amount = 1;
                        Inventory.instance.player.fpscam.DropObject(itemAll);
                        
                        slotSelected.DeleteItem();
                        HUD.instance.SetVisualMessage(false,itemAll);
                        
                    }
                  
                break;
                case "Split":
                    int result = Mathf.RoundToInt(slotSelected.currentItem.amount / 2);
                    Item itemSplit = Instantiate(slotSelected.currentItem);
                    itemSplit.amount = result;
                    AddItemSAfterSplit(itemSplit);
                    slotSelected.DeleteItem(result);
                   
                break;
                case "Heal":
                
                    if(slotSelected.currentItem.attributes.actions.Length > 0)
                    {
                        for (int i = 0; i < slotSelected.currentItem.attributes.actions.Length; i++)
                        {
                            Actions actions = slotSelected.currentItem.attributes.actions[i];
                            if(actions != null && actions.name == "Heal")
                            {
                                if(Inventory.instance.player.playerVitals.health >= Inventory.instance.player.playerVitals.healthMax )
                                    return;

                                Transform holder = player.GetComponent<FirstPersonAIO>().fpscam.armsHolder;
                                if(holder.transform.childCount > 0)
                                {
                                    HotBar.instance.DestroyWeaponHands();
                                
                                }
                                    GameObject tools = Instantiate(slotSelected.currentItem.ToolsArmPrefab,player.GetComponent<FirstPersonAIO>().fpscam.armsHolder);
                                if(tools.GetComponent<HealthArmsController>())
                                {
                                    tools.GetComponent<HealthArmsController>().SetToolsItem(slotSelected.currentItem);
                                    tools.GetComponent<HealthArmsController>().Initialized();
                                    Destroy(tools,3.3f);
                                    

                                }
                            }
                            
                        }
                        if(slotSelected.currentItem.attributes.destroyItemAfterAction)
                            slotSelected.DeleteItem();
                        if(nameOptions == "Heal")
                        {
                            Inventory.instance.OpenCloseInventory();
                        }
                    }
                    
                break;
                case "Pills":
                    
                    if(slotSelected.currentItem.attributes.actions.Length > 0)
                    {
                        for (int i = 0; i < slotSelected.currentItem.attributes.actions.Length; i++)
                        {
                            Actions actions = slotSelected.currentItem.attributes.actions[i];
                            if(actions != null && actions.name == "Pills")
                            {
                                if(actions.fonctions == "AddHealth")
                                {
                                    if(Inventory.instance.player.playerVitals.health >= Inventory.instance.player.playerVitals.healthMax )
                                        return;

                                    Inventory.instance.player.playerVitals.AddHealth(actions.value);
                                }
                                
                            }
                            
                        }
                        if(slotSelected.currentItem.attributes.destroyItemAfterAction)
                            slotSelected.DeleteItem();
                    }
                    
                    // ActionItem("Pills");
                break;
                case "equiped_weapon":
                // Inventory.instance.selectStartSlot(slotSelected);
                // HotBar.instance.CreateWeaponHands(slotSelected);
                break;
                case "equiped_tools":
                // Inventory.instance.selectStartSlot(slotSelected);
                break;
                case "Drink":
                    if(slotSelected.currentItem.attributes.value > 0)
                    {
                        slotSelected.currentItem.attributes.DecreaseValue();
                  
                    }

                break;
                case "Life":
                    if(slotSelected.currentItem.attributes.value > 0)
                    {
                        slotSelected.currentItem.attributes.DecreaseValue();
                    
                    }

                break;
                default:
                break;
            }


            HideOption();
           
                
        }

       
    }

}