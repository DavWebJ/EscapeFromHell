using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackPearl
{
    [CreateAssetMenu(menuName = "Inventory/Item/Loot Storage")]
    public class Storage : Item
    {

        [Header("Storage")]
        public int maxStock = 12;
        public List<Slot> slotList = new List<Slot>();
        public bool useRandomLoot = false;
        public bool isTrap = false;
        public bool randomListCreated =false;
        public List<Item> lootItems = new List<Item>();
        public string lootType = "";
        public Storage()
        {
            this.itemType = ItemType.Storage;
            this.amount = 1;
            this.maxAmount = 1;
            this.stackable = false;
            
        }

        public void CreateRandomLoot()
        {
            if(useRandomLoot && !randomListCreated)
            {
                lootItems = GameManager.instance.resources.GetRandomLoot(lootType,maxStock);
                randomListCreated = true;
            }else
            {
                return;
            }
            
           
        }
    }
}
