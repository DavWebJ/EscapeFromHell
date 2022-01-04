using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace BlackPearl
{
    

    public class PanelBackPack : MonoBehaviour
    {

        [HideInInspector] public Transform grid = null;
       // public int MaxAmount = 10;
        private int NumberSlot = 12;
        public bool upgradeBackPack;
       // public int currentAmount;
       // public Text currAmountText;
        public GridLayoutGroup gridLayoutGroup;
       [SerializeField] private GameObject slot;

        public void Init()
        {
            grid = transform.Find("GridSlot");
      
            // currAmountText = transform.Find("CurrAmount").GetComponentInChildren<Text>();
            // currentAmount = 0;
            // MaxAmount = 10;
            
           
        }


        // public void UpdateWeight(int item_amount)
        // {
        //     if(currentAmount > MaxAmount)
        //     {
        //         currentAmount = MaxAmount;
        //     }
        //     currentAmount += item_amount;

        // }

        private void Update() {
            // if(currentAmount <= 0)
            // {
            //     currentAmount = 0;
            // }
            // if(currentAmount >= MaxAmount)
            // {
            //     currentAmount = MaxAmount;
            // }
            // currAmountText.text = currentAmount + " / " + MaxAmount + " KG";

        }

        public void UpgradedBackPack(int places)
        {
            if(!upgradeBackPack)
            {
                upgradeBackPack = true;
                NumberSlot += places;
                //MaxAmount += places;
            }else
            {
                return;
            }
            
        }
    }


}
