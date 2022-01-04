using System.Collections;
using UnityEngine.UI;
using UnityEngine;
namespace BlackPearl
{
    public class HUDWeapon : MonoBehaviour
    {
        public static HUDWeapon instance = null;
        public Text weapont_text = null;
        private Text weapont_ammo_text = null;
        public Image  icon = null;
        public Text reload_text = null;
        public GameObject reload_go = null;

        [SerializeField] private Image batery_fill = null;
        public GameObject batery = null;
        public Sprite batery_normal = null;
        public Sprite batery_empty = null;
        public bool isFlashlight = false;
        private void Awake()
        {
            if(instance == null)
                instance = this;
                weapont_text = transform.Find("info").GetComponent<Text>();
                weapont_ammo_text = transform.Find("ammos").GetComponent<Text>();
                icon  = transform.Find("icon").GetComponent<Image>();
                // icon.sprite = null;
                GetWeaponInfos(null);
                reload_go.SetActive(false);
                
   
        }

        public void GetWeaponInfos(WeaponItem weapon)
        {
            isFlashlight = false;
            if(weapon == null)
            {
                weapont_text.text = string.Empty;
                weapont_ammo_text.text = string.Empty;
                icon.sprite = null;
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(true);
            batery.SetActive(false);
            weapont_text.text = Inventory.instance.AmountConsumableInInventory("AmmoGun").ToString();
            weapont_ammo_text.text = weapon.ammo +" / "+ weapon.max_ammo;
            Item Ammo = GameManager.instance.resources.GetitemByName("AmmoGun");
            icon.sprite = Ammo.ItemIcon;
        }

        public void GetToolsInfos(ToolsItem tools)
        {
            if(tools == null)
            {
                // weapont_text.text = string.Empty;
                weapont_ammo_text.text = string.Empty;
                icon.sprite = null;
                gameObject.SetActive(false);
                return;
            }
            
            // weapont_text.text = weapon.ItemName;

            gameObject.SetActive(true);
            weapont_ammo_text.text = (int)tools.batery +" / "+ tools.batery_max;
          
        }

        public void Ui_Batery(float value,float max)
        {

            float percent = Inventory.instance.GetPercentage(value,max);
           
            batery_fill.fillAmount = percent;
            batery_fill.color = HUD.instance.bateryBarColor.Evaluate(percent);
            
        }

        public void ShowReload(bool active,string message)
        {
    
            reload_go.SetActive(active);
            reload_text.text = message;
        }


    }
}
