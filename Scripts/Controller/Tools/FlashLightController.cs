using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlackPearl;
[RequireComponent(typeof(AudioSource))]
public class FlashLightController : MonoBehaviour
{
    [Header("References")]

    private ToolsItem toolsItem = null;

    [SerializeField] private AudioSource audios = null;
    private Animator animator = null;
    public bool isInitialized = false;
    public bool isReloading;
    public int batery_remain = 0;
    public bool isFlashlightOn = false;
    public float timer = 0;

    private FirstPersonAIO player;
    [Header("Light parameter")]
   [SerializeField] public Light flashlight;
   public float DefaultIntensity = 8f;
   
   public void Initialized()
    {
        audios = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<FirstPersonAIO>();
        flashlight.intensity = DefaultIntensity;
        HUDWeapon.instance.isFlashlight = true;
        isFlashlightOn = false;
        flashlight.enabled = false;
        audios.PlayOneShot(toolsItem.equiped_sound,0.5f);
        isInitialized = true;
    }

    private void OnDisable() {
        HUDInfos.instance.FlashlightInput(false);
        HUDInfos.instance.ReloadInput(false);
    }


    public void SetToolsItem(Item item)
    {
        if(item.itemType != ItemType.Tools)
            return;
        toolsItem = item as ToolsItem;

    }

    public void updateInputsFlashLight()
    {
        
        
        if(!player.IsGrounded)
        {

            animator.SetBool(toolsItem.walk_animation,false);
            animator.SetBool(toolsItem.sprint_animation,false);

        }

        if(!player.isWalking && !player.isSprinting)
        {
    

         
            animator.SetBool(toolsItem.walk_animation,false);
            animator.SetBool(toolsItem.sprint_animation,false);
            
        }

        if(player.isWalking && player.fps_Rigidbody.velocity != Vector3.zero)
        {
    

            animator.SetBool(toolsItem.walk_animation,true);
          
           
           animator.SetBool(toolsItem.sprint_animation,false);
            
        }
        
        if(player.isSprinting && player.fps_Rigidbody.velocity != Vector3.zero)
        {

            animator.SetBool(toolsItem.walk_animation,false);
            animator.SetBool(toolsItem.sprint_animation,true);

       
        }


        if(Input.GetKeyDown(GameManager.instance.input.input_LightOnOff) && !GameManager.instance.CheckHUD)
        {

            OpenCloseFlashLight();
        }
       
        
        if (Input.GetKeyDown(GameManager.instance.input.input_reload) && toolsItem.batery <= 0)
        {
            if(toolsItem.batery <= 0 && batery_remain > 0 && !GameManager.instance.CheckHUD)
            {
                
                StartCoroutine(ReloadingBatery());
            }
       
         

        }
    }

    public void OpenCloseFlashLight()
    {
        if (toolsItem.batery <= 0) return; 

        isFlashlightOn = !isFlashlightOn;
        SwitchFlashLight(isFlashlightOn);

    }

    public void SwitchFlashLight(bool activate)
    {
        if(!audios.isPlaying)
            audios.PlayOneShot(toolsItem.on_off_clip);

        if (activate)
        {
            
            flashlight.enabled = true;
          
        }
        else
        {

            flashlight.enabled = false;
               
        }

        
    }



    public IEnumerator ReloadingBatery()
    {
        isReloading = true;
        animator.SetTrigger("hide");
        if(!audios.isPlaying)
        {
            audios.PlayOneShot(toolsItem.batery_out_clip);
        }
        yield return new WaitForSeconds(toolsItem.batery_out_clip.length);
        toolsItem.batery = toolsItem.batery_max;
        Inventory.instance.UpdateConsumableInInventory("Batery",1);
        if(!audios.isPlaying)
        {
            audios.PlayOneShot(toolsItem.reload_clip);
        }
        yield return new WaitForSeconds(toolsItem.reload_clip.length);
        animator.Play("Get");
        
        HUDWeapon.instance.Ui_Batery(toolsItem.batery, toolsItem.batery_max);
        flashlight.intensity = DefaultIntensity;
        isReloading = false;
        
        
    }

    public void UpdateIcon()
    {
        HUDWeapon.instance.GetToolsInfos(toolsItem);
        if(toolsItem.batery > 0)
        {
            HUDWeapon.instance.batery.SetActive(true);
            HUDWeapon.instance.icon.sprite = HUDWeapon.instance.batery_normal;
        }else
        {
            HUDWeapon.instance.batery.SetActive(false);
            HUDWeapon.instance.icon.sprite = HUDWeapon.instance.batery_empty;
        }
    }

    public void CheckMessage()
    {
        if (!isFlashlightOn && toolsItem.batery > 0)
        {
            HUDInfos.instance.FlashlightInput(true);
            HUDInfos.instance.ReloadInput(false);
        }
        else if(!isFlashlightOn && toolsItem.batery <= 0 && batery_remain > 0)
        {
            HUDInfos.instance.FlashlightInput(false);
            HUDInfos.instance.ReloadInput(true);
            HUDWeapon.instance.ShowReload(false,"");
            // HUDWeapon.instance.ShowReload(true,"Appuyer sur " + GameManager.instance.input.input_reload+ " pour recharger votre lampe torche");
            
        }else if(!isFlashlightOn && toolsItem.batery <= 0 && batery_remain <= 0)
        {
            HUDInfos.instance.FlashlightInput(false);
            HUDWeapon.instance.ShowReload(true,"Trouver des piles pour recharger votre lampe torche");
        }else if(isFlashlightOn && toolsItem.batery != 0)
        {
            HUDInfos.instance.FlashlightInput(true);
            HUDWeapon.instance.ShowReload(false,"");
             HUDInfos.instance.ReloadInput(false);
        }else if(isFlashlightOn && toolsItem.batery <= 0 && batery_remain > 0)
        {
            HUDInfos.instance.FlashlightInput(false);
             HUDInfos.instance.ReloadInput(true);
            HUDWeapon.instance.ShowReload(true,"Appuyer sur " + GameManager.instance.input.input_reload+ " pour recharger votre lampe torche");
        }
        else
        {
            HUDInfos.instance.FlashlightInput(false);
            // HUDWeapon.instance.ShowReload(true,"Trouver des piles pour recharger votre lampe torche");
        }
        
    }

    
    void Update()
    {
        batery_remain = Inventory.instance.AmountConsumableInInventory("Batery");
        if(isInitialized)
        {
            
            updateInputsFlashLight();
           
            CheckMessage();
            UpdateIcon();
           
            

            if (isFlashlightOn && !GameManager.instance.CheckHUD)
            {
                
                if(toolsItem.batery > 0)
                {
                    toolsItem.batery -= toolsItem.batery_reduce_value * Time.deltaTime;
                    if(toolsItem.batery <= 20f)
                    {
                       
                        flashlight.intensity = Mathf.Lerp(flashlight.intensity,0, Time.deltaTime * 0.25f);
                        // LumiereTorche.gameObject.GetComponent<Animation>().Play("BatteryVide");
                        
                    }
                    if (toolsItem.batery <= 0 && batery_remain > 0 )
                    {
                        
                        isFlashlightOn = false;
                        toolsItem.batery = 0;
                        flashlight.enabled = false;
                        HUDWeapon.instance.batery.SetActive(false);
                        HUDWeapon.instance.icon.sprite = HUDWeapon.instance.batery_empty;
                        isFlashlightOn = false;
                        SwitchFlashLight(false);
                    }

                    if(toolsItem.batery <= 0 && batery_remain <= 0)
                    {
                        HUDWeapon.instance.ShowReload(false,"");
                        HUDWeapon.instance.batery.SetActive(false);
                        HUDWeapon.instance.icon.sprite = HUDWeapon.instance.batery_empty;
                        flashlight.enabled = false;
                        isFlashlightOn = false;
                        SwitchFlashLight(false);
                    }

                    
                    HUDWeapon.instance.Ui_Batery(toolsItem.batery, toolsItem.batery_max);
                }
            }

                
        }
    }
}
