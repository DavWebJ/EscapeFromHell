using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlackPearl;
public class LighterController : MonoBehaviour
{
[Header("References")]

    private ToolsItem toolsItem = null;

    [SerializeField] private AudioSource audios = null;
    private Animator animator = null;
    public bool isInitialized = false;
    public bool isReloading;
    public int oil_remain = 0;
    public bool isLighterOn = false;
    public float timer = 0;
    [SerializeField] private GameObject flame_go;

    private FirstPersonAIO player;
    [Header("Light parameter")]
   [SerializeField] private Light lighterlight;
   public float DefaultIntensity = 1f;
   public float range_light = 5f;
   
   public void Initialized()
    {
        audios = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<FirstPersonAIO>();
        lighterlight.intensity = DefaultIntensity;
        lighterlight.range = range_light;
        HUDWeapon.instance.isFlashlight = true;
        isLighterOn = false;
        lighterlight.enabled = false;
        
        audios.PlayOneShot(toolsItem.equiped_sound,0.5f);
        isInitialized = true;
    }



    public void SetToolsItem(Item item)
    {
        if(item.itemType != ItemType.Tools)
            return;
        toolsItem = item as ToolsItem;

    }
    public void OpenZippo()
    {
        audios.PlayOneShot(toolsItem.zippo_open_clip);
    }

    public void StartLighter()
    {
        audios.PlayOneShot(toolsItem.on_off_clip);
        flame_go.SetActive(true);
        lighterlight.enabled = true;
        lighterlight.intensity = 1f;
        lighterlight.range = 5f;
    }

    private void Update() {
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

        if(player.isWalking && !player.isSprinting)
        {
    

            animator.SetBool(toolsItem.walk_animation,true);
          
           
           animator.SetBool(toolsItem.sprint_animation,false);
            
        }
        
        if(player.isSprinting && player.isWalking)
        {
     
            
            animator.SetBool(toolsItem.walk_animation,false);
            animator.SetBool(toolsItem.sprint_animation,true);
            
       
        }
    }
}
