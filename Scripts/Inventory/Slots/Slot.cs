using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
namespace BlackPearl
{
    public enum SlotType
    {
        None,Hot_Bar,Craft,Required,CraftQ,Infos,nodraggable
    }
    public class Slot : MonoBehaviour//,ISelectHandler,IDeselectHandler
    {
        [HideInInspector] public Item currentItem = null;
        public ItemType itemAccepted = ItemType.All;
        private Image itemIcon = null;
        public Text itemAmount = null;
        public Text txt_id = null;
        public Text CancelCraftText = null;
        private Image fill_craft = null;
        public SlotType slotType = SlotType.None;
        public bool isDraggable = true;

        [Header("Icon slot event mouse")]
        [Tooltip("image du slot au differentes action mouse event hover,select,exit ")]
        public Image hover_Image = null;
        public Image select_Image = null;
       // public float weight;
        // [SerializeField] private GameObject BarHorizontal = null;
        // [SerializeField] private Image fill_HorizontalBar = null;
        // [SerializeField] private GameObject BarVertical = null;
        // [SerializeField] private Image fill_ItemVerticalBar = null;
        [HideInInspector] public PanelBackPack BackPack = null;
        [HideInInspector] public RecipeCraftItem[] recipeCraftItems = null;



        private void Awake() {
            itemIcon = transform.Find("icon").GetComponent<Image>();
            hover_Image = transform.Find("hover").GetComponent<Image>();
            hover_Image.enabled = false;
            
            
            select_Image = transform.Find("select").GetComponent<Image>();
            // fill_craft = transform.Find("fill_craft").GetComponent<Image>();
            // CancelCraftText = transform.Find("X").GetComponent<Text>();
            itemAmount = transform.Find("qty").GetComponent<Text>();
            txt_id = transform.Find("ID").GetComponent<Text>();
            // fill_craft.fillAmount = 0;
            // CancelCraftText.enabled =false;
            BackPack = GameObject.FindGameObjectWithTag("BagPack").GetComponent<PanelBackPack>();
            
            select_Image.enabled = false;

            
            //     BarHorizontal = transform.Find("BarHorizontal").gameObject;
            //     BarVertical = transform.Find("BarVertical").gameObject;
            //     fill_HorizontalBar = BarHorizontal.transform.Find("Fill_HorizontalBar").GetComponent<Image>();
            //     fill_ItemVerticalBar = BarVertical.transform.Find("Fill_VerticalBar").GetComponent<Image>();
            


        }
        private void Start() {
            
        }

        private void Update() {
            
            if(slotType == SlotType.Hot_Bar)
            {
                HotbarAction();
            }else if(slotType == SlotType.CraftQ)
            {
                UpdateCraftQ();
            }
            
        }

        #region hotbar
        
        public void HotbarAction()
        {
            if(currentItem != null)
            {
                if(Input.GetKeyDown(GameManager.instance.input.input_dropobject))
                {
                    Inventory.instance.panel_options.EventBtnHotbar(HotBar.instance.GetCurrentSlot(),"Drop");
                    
                    HotBar.instance.Selection();
                }
            }
        }
        #endregion

        #region update Slot & icon    
        public void ChangeItem(Item item)
        {
            currentItem = item;
            UpdateSlot();
        }

        public void UpdateSlot()
        {
            if(currentItem != null && currentItem.amount <= 0)
            {
                isDraggable = true;
                currentItem = null;
            }
            

            if(currentItem != null && currentItem.objectType == ObjectType.None)
            {
                
                isDraggable = false;
            }
            
            UpdateItemIcon();
            UpdateItemText();
            UpdateQualityItemBar();
            
           
            
        }

        private void UpdateItemIcon()
        {
            if(currentItem == null)
            {
                itemIcon.sprite = null;
                itemIcon.color = new Color(255,255,255,0);
                return;
            }
            itemIcon.sprite = currentItem.ItemIcon;
            itemIcon.type = Image.Type.Simple;
            itemIcon.preserveAspect = true;
            itemIcon.color = Color.white;
        }

        private void UpdateItemText()
        {
            if(currentItem == null)
            {
                itemAmount.text = string.Empty;
                return;
            }
            if(slotType == SlotType.Craft || slotType == SlotType.CraftQ)
            {
                itemAmount.text = (currentItem.crafting.resultQuantity > 1) ? "x " + currentItem.crafting.resultQuantity : string.Empty;
                
                //return;
            }else if(slotType == SlotType.Required)
            {
                string _amountRequired = currentItem.amount.ToString();
                int _amountInbackpack = Inventory.instance.ReturnsAmountRequiredForCraft(currentItem.ItemName);
                itemAmount.text = _amountInbackpack +" / " + _amountRequired;
                itemAmount.color = (_amountInbackpack >= currentItem.amount) ? Color.green : Color.red;
                
            }else{
                
                itemAmount.text = (currentItem.amount > 0) ? "x " + currentItem.amount : string.Empty;
            }
        }

        private void UpdateQualityItemBar()
        {
            // BarHorizontal.SetActive(currentItem != null && currentItem.attributes.ActivateConsumableLifeBar() && slotType != SlotType.Infos);
            // BarVertical.SetActive(currentItem != null && currentItem.attributes.ActivateVerticalBar() && slotType != SlotType.Infos);
            // fill_HorizontalBar.fillAmount = 0;
            // fill_ItemVerticalBar.fillAmount = 0;

            // if(BarHorizontal.activeSelf)
            // {
            //     fill_HorizontalBar.fillAmount = currentItem.attributes.GetPercentage();
            //     fill_HorizontalBar.color = HUD.instance.lifeBarColor.Evaluate(currentItem.attributes.GetPercentage());
            // }

            // if(BarVertical.activeSelf)
            // {
            //     fill_ItemVerticalBar.fillAmount = currentItem.attributes.GetPercentage();
            // }
        }

        private void UpdateCraftQ()
        {
            // if(Inventory.instance.panel_craft.CheckSlotQueueCrafting(this))
            // {
                
            //     float TimeTocraft = currentItem.crafting.timeToCraft;
            //     currentItem.crafting.timer += Time.deltaTime;
            //     fill_craft.fillAmount = Inventory.instance.GetPercentage(currentItem.crafting.timer,TimeTocraft);
            //     if(currentItem.crafting.timer > TimeTocraft)
            //     {
            //         Inventory.instance.AddItemBackPack(currentItem);
            //         Inventory.instance.panel_infos.UpdateCraftInfos();
            //         Destroy(gameObject);
            //     }
            // }
        }

        private void CancelCraftInQueue()
        {
            // if(recipeCraftItems.Length <= 0)
            // {
            //     return;
            // }

            // for (int i = 0; i < recipeCraftItems.Length; i++)
            // {
            //     Item item = GameManager.instance.resources.GetitemByName(recipeCraftItems[i].CraftItemName);
            //     if(item != null){
            //         item.amount = recipeCraftItems[i].amountRequired;
            //         Inventory.instance.AddItemBackPack(item);
            //     }
            // }
            // Inventory.instance.panel_infos.UpdateCraftInfos();

            // Destroy(gameObject);
        }

        public void set_SelectedImage(bool active)
        {
            select_Image.enabled = active;
            hover_Image.enabled = false;
            
        }
        #endregion

        public void SetId(string _id)
        {
            txt_id.text = _id;
        }

        #region mouse event
        public void MouseEventHover()
        {

            if(slotType == SlotType.CraftQ)
            {
                CancelCraftText.enabled = true;
            }
            if(isDraggable && Inventory.instance.isInDrag)
            {
                Inventory.instance.endSlot = this;
            }
            hover_Image.enabled = true;
            AudioM.instance.PlayHUDHoverClip();
         
            // Inventory.instance.panel_infos.ShowItemInfos(this);
         
        }
        

        public void MouseEventSelect(BaseEventData data)
        {
            if(currentItem == null)
                return;
 
            PointerEventData pointer = (PointerEventData)data;
            if(pointer.button == PointerEventData.InputButton.Left && slotType != SlotType.Required)
            {
                if(slotType == SlotType.CraftQ)
                {
                    CancelCraftInQueue();
                }else
                {
                    
                    Inventory.instance.panel_infos.ShowItemInfos(this);
                }
                

            }else if(pointer.button == PointerEventData.InputButton.Right)
            {
                if(slotType == SlotType.Craft || slotType == SlotType.CraftQ || slotType == SlotType.Required)
                return;
    
                Inventory.instance.panel_options.ShowOption(this);
                Inventory.instance.panel_infos.ShowItemInfos(this);

            }
            


        }

        public void ButtonInventoryEventSelect()
        {
            
            if(currentItem == null)
                return;
                
            if(slotType == SlotType.Craft || slotType == SlotType.CraftQ || slotType == SlotType.Required)
            return;
    
            Inventory.instance.panel_options.ShowOption(this);

            


        }

        // public void OnSelect(BaseEventData eventData)
        // {
            
        //     Inventory.instance.panel_options.ShowOption(this);
        // }

        // public void OnDeselect(BaseEventData eventData)
        // {

        //     Inventory.instance.panel_options.HideOption();
        //     // Inventory.instance.panel_options.ShowOption(this);
        // }



        

        public void MouseEventExit()
        {
            
            if(slotType != SlotType.Craft || slotType != SlotType.Required)
            {
                Inventory.instance.panel_infos.HideItemInfos();
            }
            if(slotType == SlotType.CraftQ)
            {
                CancelCraftText.enabled = false;
            }

            hover_Image.enabled = false;
            
            
            select_Image.enabled = false;
            
            if(isDraggable)
                Inventory.instance.endSlot = null;
        }
        #endregion
        public void DeleteItem()
        {
            if(currentItem == null || currentItem.amount <= 0)
            {
                return;
            }

            currentItem.amount --;
            

            if(currentItem.amount <= 0)
            {
                ChangeItem(null);
                return;
            }
            UpdateSlot();
        }
        public void DeleteItem(int amount)
        {
            if(currentItem == null || currentItem.amount <= 0 || amount <= 0)
            {
                return;
            }

            currentItem.amount -= amount;
            

            if(currentItem.amount <= 0)
            {
                ChangeItem(null);
                return;
            }
            UpdateSlot();
        }

        public void OnBeginDrag_Event()
        {
            if(Inventory.instance.isInDrag)
                return;

            if(Input.GetKey(KeyCode.Mouse0) && currentItem != null)
            {
                if(isDraggable)
                    Inventory.instance.StartDrag(this);
         
            }
        }

        public void OnEndDrag_event()
        {
            if(isDraggable)
                Inventory.instance.EndDrag();
          
        }

        
    }



}