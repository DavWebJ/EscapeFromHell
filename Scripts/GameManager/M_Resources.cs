using System.Collections;
using System.Collections.Generic;
using BlackPearl;
using UnityEngine;



public class M_Resources : MonoBehaviour
{
    private Item[] itemsDatabase = null;
    [SerializeField] private LootTable[] lootTables = null;
    [SerializeField] public Surfaces[] surfaces_DataBase = null;

    // [SerializeField] private MarketTable[] marketTables = null;

    public void InitItems()
    {
        itemsDatabase = Resources.LoadAll<Item>("Inventory/Item");
    }

    public Item GetitemByName(string _name)
    {
        if(itemsDatabase.Length <= 0)
        {
            return null;
        }

        for (int i = 0; i < itemsDatabase.Length; i++)
        {
            if(itemsDatabase[i].ItemName == _name)
            {
                return Instantiate(itemsDatabase[i]);
            }
        }
        return null;
    }

    public Item GetWeaponItemByName(string _name)
    {
        if(itemsDatabase.Length <= 0)
        {
            return null;
        }

        for (int i = 0; i < itemsDatabase.Length; i++)
        {
            if(itemsDatabase[i].ItemName == _name)
            {
                return Instantiate(itemsDatabase[i]);
            }
        }
        return null;
    }

    public GameObject getSurface(string _name)
    {
        if(_name == string.Empty || surfaces_DataBase.Length <= 0)
        {
            return null;
        }

        for (int i = 0; i < surfaces_DataBase.Length; i++)
        {
            if(surfaces_DataBase[i].name == _name)
            {
                return surfaces_DataBase[i].bullet_impact;
            }
        }
        return null;
    }

    public List<Item> GetRandomLoot(string _lootType, int maxStock)
    {
        LootTable table = new LootTable();

        if(lootTables.Length > 0)
        {
            for (int i = 0; i < lootTables.Length; i++)
            {
                if(lootTables[i].lootType == _lootType)
                table = lootTables[i];
            }
        }

        if(table.lootType == string.Empty)
        {
            return null;
        }

        List<Item> lootItems = new List<Item>();

        if(table.itemNames.Length > 0)
        {
            for (int i = 0; i < table.itemNames.Length; i++)
            {
                if(lootItems.Count < maxStock){
                    Item item = GetitemByName(table.itemNames[i]);
                    if(item != null)
                    {
                  
                        if(item.ItemRarity == ItemRarityEnum.Common)
                        {
                           item.rarityItem  = Random.Range(85,100);// 80%

                        }else  if(item.ItemRarity == ItemRarityEnum.Rare)
                        {
                            item.rarityItem  = Random.Range(65,85);//70%

                        }else  if(item.ItemRarity == ItemRarityEnum.Epic)
                        {
                            item.rarityItem  = Random.Range(50,65); //50%

                        }else  if(item.ItemRarity == ItemRarityEnum.Legendary)
                        {
                            item.rarityItem  = Random.Range(35,50);// 40%

                        }else  if(item.ItemRarity == ItemRarityEnum.Mythic)
                        {
                            item.rarityItem  = Random.Range(10,35); // 25%
                        }
                        
                        float randomPercentageLoot = Random.Range(0,100);
                        if(randomPercentageLoot <= item.rarityItem)
                        {
                            // random stack loot item amount
                            if(item.stackable)
                            {
                                int randomStack = Random.Range(1,Mathf.RoundToInt(item.maxAmount / 2));
                                Item itemAmount = Instantiate(item);
                                itemAmount.amount = randomStack;
                                lootItems.Add(itemAmount);
                            }else
                            {
                                lootItems.Add(item);
                            }
                            
                        }
                    }
                }
            }
            
        }
        return lootItems;
    }
    // public List<Item> GetRandomMarketList(string _marketType, int maxStock)
    // {
    //     MarketTable table = new MarketTable();

    //     if(marketTables.Length > 0)
    //     {
    //         for (int i = 0; i < marketTables.Length; i++)
    //         {
    //             if(marketTables[i].marketType == _marketType)
    //             table = marketTables[i];
    //         }
    //     }

    //     if(table.marketType == string.Empty)
    //     {
    //         return null;
    //     }

    //     List<Item> marketItems = new List<Item>();

    //     if(table.itemNames.Length > 0)
    //     {
    //         for (int i = 0; i < table.itemNames.Length; i++)
    //         {
    //             if(marketItems.Count < maxStock){
    //                 Item item = GetitemByName(table.itemNames[i]);
    //                 if(item != null)
    //                 {
    //                     float randomPercentageSell= Random.Range(0,100);
    //                     if(randomPercentageSell <= item.rarityItem)
    //                     {
    //                         // random stack loot item amount
    //                         if(item.stackable)
    //                         {
    //                             int randomStack = Random.Range(10,50);
    //                             Item itemAmount = Instantiate(item);
    //                             itemAmount.amount = randomStack;
    //                             marketItems.Add(itemAmount);
    //                         }else
    //                         {
    //                             marketItems.Add(item);
    //                         }
                            
    //                     }
    //                 }
    //             }
    //         }
            
    //     }
    //     return marketItems;
    // }

    [System.Serializable]
    public class LootTable
    {
        public string lootType = string.Empty;
        public string[] itemNames = null;
    }

    // [System.Serializable]
    // public class MarketTable
    // {
    //     public string marketType = string.Empty;
    //     public string[] itemNames = null;
    // }

    [System.Serializable]
    public class Surfaces
    {
        public string name = string.Empty;
        public GameObject bullet_impact = null;

    }


}


