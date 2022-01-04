using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
namespace BlackPearl
{
    
   public class HUDVitals : MonoBehaviour
   {
        public  static HUDVitals instance = null;
        [Header("Stamina")]
        [SerializeField] private GameObject stamina = null;

        [SerializeField] private Image stamina_fill = null;
        [Header("Health")]
        [SerializeField] private GameObject health = null;
        [SerializeField] private Image health_fill = null;

    

        private void Awake() {
            if(instance == null)
            {
                instance = this;
            }
        }
        public void Ui_Stamina(float value,float max)
        {
            
            float percent = Inventory.instance.GetPercentage(value,max);
           
            stamina_fill.fillAmount = percent;
            stamina_fill.color = HUD.instance.staminaBarColor.Evaluate(percent);
            
        }

        public void Ui_Health(float value,float max)
        {
            
            float percent = Inventory.instance.GetPercentage(value,max);
       
            health_fill.fillAmount = percent;
            health_fill.color = HUD.instance.lifeBarColor.Evaluate(percent);
            
        }

        
        // public void Ui_Mana(float value,float max)
        // {
            
        //     float percent = Inventory.instance.GetPercentage(value,max);
        //     if(percent > 0.9f )
        //     {
        //         mana_icon.sprite = mana_5;
        //     }else if(percent < 0.9f  && percent >= 0.75f)
        //     {
        //         mana_icon.sprite = mana_4;
        //     }else if(percent < 0.75f && percent >= 0.6f)
        //     {
        //         mana_icon.sprite = mana_3;
        //     }else if(percent < 0.6f  && percent > 0.4f)
        //     {
        //         mana_icon.sprite = mana_2;
        //     }else if(percent < 0.4f  && percent > 0.2f)
        //     {
        //         mana_icon.sprite = mana_1;
        //     }else if(percent < 0.2f){
        //         mana_icon.sprite = mana_empty;
        //     }
            
        //     if(mana.activeInHierarchy)
        //     {
        //         // mana_fill.fillAmount = percent;
        //         // mana_fill.color = HUD.instance.manaBarColor.Evaluate(percent);
        //     }
        // }


    }
}
