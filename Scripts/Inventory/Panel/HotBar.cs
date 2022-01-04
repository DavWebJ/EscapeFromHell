using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackPearl
{
    public class HotBar : MonoBehaviour
    {
        public static HotBar instance = null;
        public Transform gridSlots = null;
        public int currSlot = 0;
        FirstPersonAIO player = null;
        private int NumberSlot = 6;
        public bool isInit = false;
        private float timer = 0;
        private float delaySelection = 0.2f;


        private void Awake() {
            
            if(instance == null)
            {
                instance = this;
            }

            gridSlots = transform.Find("GridSlot");

        }


        private void Update() {
            
            if(isInit && !GameManager.instance.CheckHUD)
            {
                if(timer <= 0)
                    InputHotbar();
                
                if(timer > 0)
                {
                    timer -= Time.deltaTime;
                }
            }
        }

        public void Init(FirstPersonAIO _player)
        {
            player =_player;
            StartCoroutine(CreateSlot());
          
            
        }

        public void InputHotbar()
        {
            if(Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetAxis("D-Pad Horizontal") > 0)
            {
                if(currSlot >= gridSlots.childCount -1)
                {
                    currSlot = 0;
                }else{
                    currSlot ++;
                }
                // DestroyWeaponHands();
                Selection();
            }

            if(Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("D-Pad Horizontal") < 0)
            {
                if(currSlot <= 0)
                {
                    currSlot = gridSlots.childCount -1;
                }else{
                    currSlot --;
                }
                // DestroyWeaponHands();
                Selection();
            }

        }

    //    public void CheckForSlotSelection()
    //    {
    //        for (int i = 0; i < gridSlots.childCount; i++)
    //        {
    //            if(i == currSlot)
    //            {
    //                 Slot slot = gridSlots.GetChild(i).GetComponent<Slot>();
    //                 NaviguationSystemHotbar.instance.checkFirstSelected(slot);
    //                 if(!slot.select_Image.enabled)
    //                     slot.set_SelectedImage(true);
                    
    //            }else
    //            {
    //                 Slot slot = gridSlots.GetChild(i).GetComponent<Slot>();
    //                 NaviguationSystemHotbar.instance.checkFirstSelected(slot);
    //                 if(!slot.select_Image.enabled)
    //                     gridSlots.GetChild(0).GetComponent<Slot>().set_SelectedImage(true);
    //            }

    //        }

    //    }



        public void Selection()
        {
        
            timer = delaySelection;
            if(gridSlots.childCount <= 0)
            return;

            DestroyWeaponHands();
            
            for (int i = 0; i < gridSlots.childCount; i++)
            {
             
                if(i == currSlot)
                {
                    Slot slot = gridSlots.GetChild(i).GetComponent<Slot>();
                    slot.set_SelectedImage(true);
                    CreateWeaponHands(slot);
                   
                    
                    
                    
                }else
                {
                
                    gridSlots.GetChild(i).GetComponent<Slot>().set_SelectedImage(false);

                  
                 
                    
                }
            }
        }


        
        
        public void CreateWeaponHands(Slot slot)
        {

            if(slot == null ||slot.currentItem == null || slot.currentItem.amount <= 0 || slot.currentItem.ItemGroundPrefabs == null)
            {
                return;
            }
            
            // if(slot.currentItem.WeaponPrefab == null || slot.currentItem.ToolsPrefab == null)
            // {
            //     DestroyWeaponHands();
            //     GameObject arms_empty = Instantiate(unarmed_prefabs,player.fpscam.armsHolder);
            // }
            
            if(slot.currentItem.itemType == ItemType.Buildable)
            {
                Debug.Log("is a buidable object");
            }
            else if(slot.currentItem.objectType == ObjectType.Equipable && slot.currentItem.itemType == ItemType.Weapon)
            {
                
                
                if(slot == null || slot.currentItem == null || slot.currentItem.amount <=0 || slot.currentItem.WeaponArmPrefab == null)
                return;
                
                GameObject weapon = Instantiate(slot.currentItem.WeaponArmPrefab,player.fpscam.armsHolder);
      
                if(weapon.GetComponent<WeaponController>())
                    weapon.GetComponent<WeaponController>().SetWeaponItem(slot.currentItem);
                
            

            }
            else if(slot.currentItem.objectType == ObjectType.Equipable && slot.currentItem.itemType == ItemType.Tools)
            {
                
                
                if(slot == null || slot.currentItem == null || slot.currentItem.amount <=0 || slot.currentItem.ToolsArmPrefab == null)
                return;
                
                GameObject tools = Instantiate(slot.currentItem.ToolsArmPrefab,player.fpscam.armsHolder);
      
                if(tools.GetComponent<ToolsController>())
                    tools.GetComponent<ToolsController>().SetToolsItem(slot.currentItem);
                
                if(tools.GetComponent<FlashLightController>())
                {
                    tools.GetComponent<FlashLightController>().SetToolsItem(slot.currentItem);
                    tools.GetComponent<FlashLightController>().Initialized();
                }
                if(tools.GetComponent<LighterController>())
                {
                    tools.GetComponent<LighterController>().SetToolsItem(slot.currentItem);
                    tools.GetComponent<LighterController>().Initialized();
                }


            }
            else
            {
                return;
            }
            HUDWeapon.instance.GetWeaponInfos(null);
           
        }


        public void DestroyWeaponHands()
        {
            Transform holder = player.fpscam.armsHolder;
            if(holder.childCount > 0)
            {
                
                for (int i = 0; i < holder.childCount; i++)
                {
                    
                    holder.GetChild(i).GetComponent<Animator>().SetTrigger("hide");
                    Destroy(holder.GetChild(i).gameObject);
                     
                }
                
            }
           
           
            HUDWeapon.instance.GetWeaponInfos(null);
       
        }

        public IEnumerator CreateSlot()
        {
            if(NumberSlot <= 0)
            {
               yield return null;
            }

            if(gridSlots.childCount > 0)
            {
                for (int s = 0; s < gridSlots.childCount; s++)
                {
                    Destroy(gridSlots.GetChild(s).gameObject);
                }
            }
            yield return new WaitForSeconds(.2f);
            int nb = 1;
            for (int i = 0; i < NumberSlot; i++)
            {
                Slot s = Instantiate(Inventory.instance.slotHotbarPref,gridSlots).GetComponent<Slot>();
                s.slotType = SlotType.Hot_Bar;
                s.SetId(nb.ToString());
                nb++;
            }
                Selection();
                // Item lighter =
                // Inventory.instance.AddItemBackPack();
                // GameObject tools = Instantiate(slot.currentItem.ToolsArmPrefab,player.fpscam.armsHolder);
                isInit = true;
            
        }

        public bool AddItemToSlot(Item item)
        {
            if(item == null || item.amount <= 0)
                return false;
            Slot slotSelected = GetCurrentSlot();

            if(slotSelected == null)
                return false;

            if(slotSelected.currentItem != null)
            {
              
                player.fpscam.DropObject(slotSelected.currentItem);
                
            }
            slotSelected.ChangeItem(item);
            Selection();
            return true;
        }

        public Slot GetCurrentSlot()
        {
            for (int i = 0; i < gridSlots.childCount; i++)
            {
                if(currSlot == gridSlots.GetChild(i).GetSiblingIndex())
                {
                    return gridSlots.GetChild(i).GetComponent<Slot>();
                }
            }

            return null;
        }

    }
}