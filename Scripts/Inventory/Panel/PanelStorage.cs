using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackPearl
{
    

    public class PanelStorage : MonoBehaviour
    {
        public Transform gridSlot = null;
        private GameObject SceneObject = null;
        private Storage storage = null;
        public void Init()
        {
            gridSlot = transform.Find("GridSlot");
            HidePanel();
        }

        public void ShowPanel(GameObject _sceneObj,Item _sceneItem)
        {
         
            if(_sceneObj == null || _sceneItem == null || _sceneItem.itemType != ItemType.Storage)
            {
   
                HidePanel();
                return;
            }
 
             gameObject.SetActive(true);
            Inventory.instance.OpenCloseInventory();
            SceneObject = _sceneObj;
            storage = _sceneItem as Storage;

            StartCoroutine(DelayCreatedSlot());
        }

        private IEnumerator DelayCreatedSlot()
        {
            Inventory.instance.DestroyAllSlots(gridSlot);
            yield return new WaitForSeconds(0.2f);

            if(storage.slotList.Count > 0)
            {
                Inventory.instance.UpdateStorageSlots(gridSlot,storage.slotList);
            }else
            {
                // create empty slot by amount max
                Inventory.instance.CreateSlots(gridSlot,storage.maxStock);

                // add items by loot
                if(storage.useRandomLoot && storage.randomListCreated && storage.lootItems.Count > 0)
                {
                    for (int i = 0; i < storage.lootItems.Count; i++)
                    {
                        Inventory.instance.AddItemStorage(storage.lootItems[i]);
                    }
                }

            }
        }
        public void HidePanel()
        {

            if(storage != null)
            {
                storage.slotList.Clear();
                storage.slotList = Inventory.instance.GetSlots(gridSlot);
                storage.lootItems.Clear();
            }

            if(SceneObject != null && SceneObject.GetComponent<Interract>())
            {
                SceneObject.GetComponent<Interract>().SetItem(storage);
                SceneObject.GetComponent<Interract>().CloseStorage();
            }

            SceneObject = null;
            storage = null;

            gameObject.SetActive(false);

        }
    }

}
