using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlackPearl;
[RequireComponent(typeof(AudioSource))]

public class ToolsController : MonoBehaviour
{

    [Header("References")]

    public Vector3 parentsModelOrigin = new Vector3();
    private ToolsItem toolsItem = null;

    [SerializeField] private AudioSource audios = null;
    private Animator animator = null;
    public bool isInitialized = false;


    private FirstPersonAIO player;


    public void Initialized()
    {
        audios = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        audios.playOnAwake = false;
        audios.loop = false;
        audios.volume = 0.5f;
        player = FindObjectOfType<FirstPersonAIO>();
     
        HUDWeapon.instance.GetToolsInfos(toolsItem);
        
        
        isInitialized = true;
    }

    public void SetToolsItem(Item item)
    {
        if(item.itemType != ItemType.Tools)
            return;
        toolsItem = item as ToolsItem;

        Initialized();
    }
    public void updateInputsTools()
    {
        HUDWeapon.instance.GetToolsInfos(toolsItem);   
        
       
        // HUD.instance.crosshair.gameObject.SetActive(!isAiming);
        
        
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


        


    }

    
   

   
    void Update()
    {
        if(isInitialized)
        {
            
            updateInputsTools();
           
            
        }
        
    }
}
