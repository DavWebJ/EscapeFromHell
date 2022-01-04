using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackPearl
{
    [CreateAssetMenu(menuName = "Inventory/Item/CreateItem")]
    public class Item : ScriptableObject
    {
        public string ItemName = "";
        public string ItemDescription = "";
        public Sprite ItemIcon = null;
        public int price = 0;
        public ItemType itemType = ItemType.None;
        public ItemRarityEnum ItemRarity = ItemRarityEnum.Common;
        public ObjectType objectType = ObjectType.None;
        public Firemode firemode = Firemode.semi;
        public WeaponType weaponType = WeaponType.none;
        public bool useWithAnimation = false;
        
        public int amount;
        public int maxAmount = 1;
        public bool stackable = false;
        public bool isPerimable = false;
        public GameObject[]  fx;
        public AudioClip heal_clip;
        public AudioClip equiped_clip;
        public AudioClip exhal_clip;
        
        public int rarityItem = 0; 
        public GameObject ItemGroundPrefabs = null;
        public GameObject WeaponArmPrefab = null;
        public GameObject ToolsArmPrefab = null;

        [Header("Attributes")]
        public Attributes attributes = new Attributes();
        public Actions actions = new Actions();

        [Header("Crafting")]
        public Crafting crafting = new Crafting();
        public Buildable buildable = new Buildable();


    }

    [System.Serializable]
    public class Crafting
    {
        public bool  isCraftable = false;
        public int resultQuantity = 1;
        public float timeToCraft = 1;
        public float timer = 0;
   
        public RecipeCraftItem[] recipeCraftItems = null;

    }

    [System.Serializable]
    public class RecipeCraftItem
    {
        public string CraftItemName = string.Empty;
        public int amountRequired = 1;

    }

    [System.Serializable]
    public class Buildable
    {
        public string BuilditemName = string.Empty;
        public GameObject BuildItemPrefabs = null;

        

    }

    [System.Serializable]
    public class Attributes
    {
        public string name = string.Empty;
        public string type = string.Empty;
        public string quality = string.Empty;
        public float value = 0;
        public float max = 0;
        public float GetPercentage()
        {
            return Inventory.instance.GetPercentage(value,max);
        }
        public void IncreaseValue()
        {
            value ++;
            if(value > max)
            {
                value = max;

            }

        }
        public void DecreaseValue()
        {
            value --;
            if(value <= 0)
            {
                value = 0;
                if(name == "Liquide")
                {
                    quality = string.Empty;
                }
                if(name == "Life")
                {
                    quality = "casser";
                }
            }
        }

        public bool ActivateVerticalBar()
        {
            return (name == "Liquide");
        }

        public bool ActivateConsumableLifeBar()
        {
            return (name == "Life");
        }
        public bool destroyItemAfterAction = false;
        public Actions [] actions = null;
        public Actions GetActions(string _name)
        {
            if(_name == string.Empty || actions.Length <= 0)
            {
                return null;
            }
            for (int i = 0; i < actions.Length; i++)
            {
                if(actions[i].name == _name)
                {
                    return actions[i];
                }
            }
            return null;
        }

    }

    [System.Serializable]
    public class Actions
    {
        public string name = string.Empty;
        public string fonctions = string.Empty;
        public int value = 0;
    }
}
