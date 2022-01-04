using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
//using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
namespace BlackPearl{
public class VitalState : MonoBehaviour {

    [Header("Reference")]
    // private PlayerLocomotion playerStates;
//     PlayerModelsAnimator modelsAnimator;
    private FirstPersonAIO player  = null;
    //public PostProcessVolume Volume;
    public float timer = 0;
    [Header("Bool")]
    public bool IsTired = false;

    [Header("vital state parameter")]
    
    public bool isdead;
    public float stamina = 100;
    public float StaminaReduceValue = 5f;
    public float StaminaRegainValue = 2.5f;
    public float health = 100;
    // public float hunger;
    // public float thirst;
    // public float mana;
    // public float fatigue;
    public float staminaMax = 100;
    public float healthMax = 100;
    // public float hungerMax = 100;
    // public float thirstMax = 100;
    // public float manaMax = 100;
    public float staminaDecrease = 1;
    public float healthDecrease;
    // public float hungerDecrease;
    // public float thirstDecrease;
    // public float manaDecrease;
    public float staminaIncrease = 1;
    public float healthIncrease;
    // public float hungerIncrease;
    // public float thirstIncrease;
    // public float manaIncrease;
    // public bool isSprinting;

    public float StaminaTimer =0;
    public bool isTired = false;

    public AudioClip hexal_clip;
    public AudioClip pills_clip;





    void Start () {

       HUDVitals.instance.Ui_Stamina(stamina, staminaMax);
       HUDVitals.instance.Ui_Health(health, healthMax);
       player = GetComponent<FirstPersonAIO>();

    }
	
	
	void Update () {

        SetStamina();
       SetHealth();


    }
    public void SetHealth()
    {

        if(health >= healthMax)
        {
            health = healthMax;
        }
            HUDVitals.instance.Ui_Health(health, healthMax);
    }

    public void AddHealth(int value)
    {
        health += value;
        if(health >= healthMax)
        {
            health = healthMax;
        }
        player.audioSource.PlayOneShot(pills_clip);
        
        HUDVitals.instance.Ui_Health(health, healthMax);
        StartCoroutine(HealAndHexal());
    }
    public void AddHealthFull(int value)
    {
        health += value;
        if(health >= healthMax)
        {
            health = healthMax;
        }
    
        HUDVitals.instance.Ui_Health(health, healthMax);
        
    }

    public IEnumerator HealAndHexal()
    {
        yield return new WaitForSeconds(2.5f);
        player.GetComponent<AudioSource>().PlayOneShot(hexal_clip);
        yield break;
    }


    public void Takedamage(int damage)
    {
        if(isdead)
            return;

        HUD.instance.ScreenEffect("BloodFallDamage");

       // modelsAnimator.PlayTargetAnimation("hit",true);
        health -= damage;
         
        if (health <= 0)
        {
            health = 0;
            isdead = true;
          //  modelsAnimator.animator.SetTrigger("death");
            // if(AudioM.instance.audioSource.isPlaying && AudioM.instance.audioSource.clip !=AudioM.instance.AI_victoryClip)
            // {
               
            //     AudioM.instance.PlayTransitionAIVictory();

            // }
    
            
            
        }
        HUDVitals.instance.Ui_Health(health, healthMax);
    }


    public void SetStamina()
    {
        
        // if(Time.time > timer && stamina != staminaMax)
        //     {
        //         stamina += StaminaRegainValue;
        //         if(stamina > staminaMax )
        //         {
        //             stamina = staminaMax;
        //         }
        //         timer  = Time.time + staminaIncrease;
        //     }

        if(player.isSprinting && player.fps_Rigidbody.velocity != Vector3.zero)
        {
            
            if(Time.time > timer && stamina > 0)
            {
                stamina -= StaminaReduceValue;
                
                if(stamina <= 0 )
                {
                    stamina = 0;

                }
                timer = Time.time + staminaDecrease;
            }
        }else
        {
            if(Time.time > timer && stamina != staminaMax)
            {
                stamina += StaminaRegainValue;
              
                if(stamina > staminaMax )
                {
                    stamina = staminaMax;
                }
                timer  = Time.time + staminaIncrease;
            }
        }

        isTired = stamina <= 0;
        HUDVitals.instance.Ui_Stamina(stamina, staminaMax);
    }


    

}
}
