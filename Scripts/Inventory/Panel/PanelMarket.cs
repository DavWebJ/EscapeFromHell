using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackPearl
{
    

    public class PanelMarket : MonoBehaviour
    {
    //     public Transform gridSlot = null;
    //     private GameObject MarketObject = null;
    //     private Market market;

    //     public void Init()
    //     {
    //         gridSlot = transform.Find("GridSlot");
    //         HidePanelMarket();
    //     }

    //     public void ShowPanelMarket(GameObject _sceneObj,Item _sceneItem)
    //     {
           
    //         if(_sceneObj == null || _sceneItem == null || _sceneItem.itemType != ItemType.Market)
    //         {
                
    //             HidePanelMarket();
    //             return;
    //         }
    //         gameObject.SetActive(true);
    //         Inventory.instance.OpenCloseInventory();
    //         MarketObject = _sceneObj;
    //         market = _sceneItem as Market;

    //         StartCoroutine(DelayCreatedMarketSlot());
    //     }

    //     private IEnumerator DelayCreatedMarketSlot()
    //     {
    //         Inventory.instance.DestroyAllSlots(gridSlot);
    //         yield return new WaitForSeconds(0.3f);

    //         if(market.slotList.Count > 0)
    //         {
    //             Inventory.instance.UpdateStorageSlots(gridSlot,market.slotList);
    //         }else
    //         {
    //             // create empty slot by amount max
    //             Inventory.instance.CreateSlots(gridSlot,market.maxStock);

    //             // add items by loot
    //             if(market.marketListCreated && market.marketItems.Count > 0)
    //             {
    //                 for (int i = 0; i < market.marketItems.Count; i++)
    //                 {
    //                     Inventory.instance.AddItemMarket(market.marketItems[i]);
    //                 }
    //             }

    //         }
    //     }

    //     public void HidePanelMarket()
    //     {
    //         if(market != null)
    //         {
    //             market.slotList.Clear();
    //             market.slotList = Inventory.instance.GetSlots(gridSlot);
    //             market.marketItems.Clear();
    //         }

    //         if(MarketObject != null && MarketObject.GetComponent<Interract>())
    //         {
    //             MarketObject.GetComponent<Interract>().SetItem(market);
    //             MarketObject.GetComponent<Interract>().CloseMarket();
    //         }

    //         MarketObject = null;
    //         market = null;

    //         gameObject.SetActive(false);

    //     }
    }

    
}
