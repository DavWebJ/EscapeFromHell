using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace BlackPearl
{
    [RequireComponent(typeof(AudioSource))]
    // [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]

    public class Interract : MonoBehaviour
    {
        [Header("Interract System")]
        private Outline[] outlines = null;
    

        [Header("Item")]
        [SerializeField] private string itemName = string.Empty;
        public enum ActionType{None,Examinable,pickable,equipable,interractable}
       
        [SerializeField] public ActionType actionType = ActionType.pickable;
        public ItemRarityEnum rarityEnum;
        public Item itemRef = null;

        [Header("Audio/FX")]
        private AudioSource audios;
        public AudioClip open_storageclip = null;
        public AudioClip close_storageclip = null;
        // public AudioClip open_MarketClip = null;
        // public AudioClip close_marketclip = null;
        public AudioClip pickup_clip = null;
        public AudioClip trap_clip;
        private Rigidbody rb = null;
        public bool isMovable = true;
        public bool isMoving = false;
        public bool isInspectable = false;
       private FirstPersonAIO player;
       private WeaponItem weapon = null;


        private void Start() {
            Init();
        }
        private void Init() {
            
            player = FindObjectOfType<FirstPersonAIO>();     
            rb = GetComponent<Rigidbody>();
            audios = GetComponent<AudioSource>();
            GetItem();
           
            // modelsAnimator = FindObjectOfType<PlayerManager>().GetComponentInChildren<PlayerModelsAnimator>();
 
            outlines = GetComponentsInChildren<Outline>();
            if(outlines.Length > 0)
            {
                for (int i = 0; i < outlines.Length; i++)
                {
                    outlines[i].OutlineMode = GameManager.instance.options.outline_mode;
                    outlines[i].OutlineColor = GameManager.instance.options.outline_color;
                    outlines[i].OutlineWidth = GameManager.instance.options.outlin_width;
                }

            }
            ActivateOutlines(false);
            

            audios.volume = 0.5f;
            audios.playOnAwake = false;

            
        }

        #region interract & Input

            public void EventInterraction(bool activate)
            {
                
                ActivateOutlines(activate);
                HUD.instance.ChangeCrossHair(HUD.crosshair_type.pickup);

                HUDInfos.instance.SceneObjectInfos(activate ? this : null);

            }

            public void Update_Input()
            {
                
                if(isMoving)
                {
                    if(Input.GetKeyUp(GameManager.instance.input.input_moveobject) && isMoving)
                    {
                        EjectObject();
                        
                    }
                }else
                {
                    if(Input.GetKey(GameManager.instance.input.input_moveobject) && isMovable)
                    {
                        MoveUpdate();
                    }
                    if(actionType == ActionType.pickable || actionType == ActionType.equipable)
                    {
                        if(Input.GetKeyDown(GameManager.instance.input.input_pickup))
                        {
                            
                            PickUp();
                             
                        }
                        if(Input.GetKeyDown(GameManager.instance.input.input_equip))
                        {

                            EquipItem();       
                            
                        }

                    }
                    if(actionType == ActionType.interractable)
                    {
                        if(Input.GetKeyDown(GameManager.instance.input.input_actionPrimary))
                        {
                            ActionInterract();
                        }
                    }

                    
                }
            }
        #endregion
        #region Items Actions
        public void PickUp()
        {
        
            HUDInfos.instance.SceneObjectInfos(null);
            bool result = Inventory.instance.AddItemBackPack(GetItem());

            

            if(result)
            {
               
                StartCoroutine(PickUpItem());
            
            }
                
        }

        IEnumerator PickUpItem()
        {

             audios.PlayOneShot(pickup_clip);
            yield return new WaitForSeconds(.2f);    
                Destroy(gameObject);
               
        }
        public void EquipItem()
        {
            
            HUDInfos.instance.SceneObjectInfos(null);
            audios.PlayOneShot(pickup_clip);
            bool result = HotBar.instance.AddItemToSlot(GetItem());
                Destroy(gameObject);
               
        }


        public Item GetItem()
        {
            if(itemRef == null)
            {
                itemRef = GameManager.instance.resources.GetitemByName(itemName);
            }
            return itemRef;
        }
        public void SetItem(Item item)
        {
            if(item == null)
            return;

            itemRef = item;
        }


        #endregion
        #region Action Interract Object
        public void ActionInterract()
        {

           HUDInfos.instance.SceneObjectInfos(null);
            if(GetComponentInChildren<Animator>())
            {
                GetComponentInChildren<Animator>().SetTrigger("open");

            }
            if(open_storageclip != null)
            {
                audios.PlayOneShot(open_storageclip);
            }
                
            // if(Inventory.instance.panel_market.isActiveAndEnabled)
            // {
         
            //     Inventory.instance.panel_market.gameObject.SetActive(false);
            // }


            if(itemRef.itemType == ItemType.Storage)
            {
                
                Storage storage = itemRef as Storage;
                if(storage.isTrap)
                {
                    //player damage
                    Transform spawn = transform.Find("fx_spawn");
                    for (int i = 0; i < storage.fx.Length; i++)
                    {
                        GameObject go_fx = Instantiate(storage.fx[i],spawn.transform);
                        go_fx.SetActive(true);
                    }

                    audios.PlayOneShot(trap_clip);
                    HUD.instance.ScreenEffect("BloodFallDamage");
                    Inventory.instance.player.playerVitals.Takedamage(25);
                   StartCoroutine(TrapStorage());
                    return;
                }
                storage.CreateRandomLoot();
               Inventory.instance.panel_storage.ShowPanel(gameObject,storage);
            }
  
            
            Inventory.instance.panel_storage.ShowPanel(gameObject, GetItem());

        }

        IEnumerator TrapStorage()
        {
            yield return new WaitForSeconds(1f);
            CloseStorage();
            yield return new WaitForSeconds(.5f);
            HUDInfos.instance.SceneObjectInfos(null);
            if(Inventory.instance.panel_storage.isActiveAndEnabled)
            {
                Inventory.instance.panel_storage.gameObject.SetActive(false);
            }
            Destroy(gameObject,2);
            yield break;
        }

        public void CloseStorage()
        {
            if(GetComponentInChildren<Animator>())
            {
                GetComponentInChildren<Animator>().SetBool("open",false);

                if(close_storageclip != null)
                audios.PlayOneShot(close_storageclip);
            }
        }

        // public void OpenMarket()
        // {
        //     if(open_MarketClip != null)
        //         audios.PlayOneShot(open_MarketClip);
        //     if(Inventory.instance.panel_storage.isActiveAndEnabled)
        //     {
        //         Inventory.instance.panel_storage.gameObject.SetActive(false);
        //     }

        //         // create market list
        //     if(itemRef != null && itemRef.itemType == ItemType.Market)
        //     {
        //         Market market = itemRef as Market;
        //         market.CreateMarketList();
        //         Inventory.instance.panel_market.ShowPanelMarket(gameObject,market);

        //     }

        // }

        // public void CloseMarket()
        // {

        //     if(close_marketclip != null)
        //         audios.PlayOneShot(close_marketclip);
        // }
        #endregion

        #region Move object
        public bool MoveUpdate()
        {

            if(isMovable)
                CatchObject();
            return isMoving;
        }
        // method pour  attraper un objet
        private void CatchObject()
        {
            rb.isKinematic = true;
            transform.parent = player.fpscam.targetLook.transform;
            isMoving = true;
        }

        // methode pour jeter l'objet
        private void EjectObject()
        {
            rb.isKinematic = false;
            transform.parent = null;
            isMoving = false;
            rb.AddForce(player.transform.forward * (player.fpscam.dropForce / rb.mass));
            audios.PlayOneShot(pickup_clip);
        }
        private void DropObject()
        {
            rb.isKinematic = false;
            transform.parent = null;
            isMoving = false;
            audios.PlayOneShot(pickup_clip);
      
        }

        public void DropObject(Item item)
        {
            if(item == null || item.amount <= 0 || item.ItemGroundPrefabs == null || item.itemType == ItemType.Buildable)
            {
                return;
            }

             GameObject go = Instantiate(item.ItemGroundPrefabs,player.fpscam.targetEject.position,player.fpscam.targetEject.rotation);

            if(go.GetComponent<Rigidbody>())
            {
                Rigidbody rb = go.GetComponent<Rigidbody>();
                rb.AddForce(player.transform.forward * player.fpscam.dropForce / rb.mass);
            }
            if(go.GetComponent<Interract>())
            {
                go.GetComponent<Interract>().SetItem(item);
            }
            audios.PlayOneShot(pickup_clip);
            HotBar.instance.Selection();
        }

        #endregion

        #region Outline system
        public void ActivateOutlines(bool activate)
        {
            if(outlines.Length > 0)
            {
                for (int i = 0; i < outlines.Length; i++)
                {
                    outlines[i].enabled = activate;
                }
            }
            
        }
        #endregion

    }
}
