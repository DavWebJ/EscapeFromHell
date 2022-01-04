using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
namespace BlackPearl{
    public class HUDEnemyVitals : MonoBehaviour
    {
        public  static HUDEnemyVitals instance = null;
        [SerializeField] private GameObject lifeBarObject = null;
        [SerializeField] private Image health_fill = null;
        private void Awake() {
            if(instance == null)
            {
                instance = this;
            }
        }

        public void Ui_Health(float value,float max)
        {
            
            float percent = Inventory.instance.GetPercentage(value,max);
             if(lifeBarObject.activeInHierarchy){
                health_fill.fillAmount = percent;
                health_fill.color = HUD.instance.lifeBarColor.Evaluate(percent);
             }
            
        }
    }
}
