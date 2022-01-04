using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace BlackPearl
{
    

    public class DragImages : MonoBehaviour
    {
        private Image img_icn = null;
        private Text text_amnt = null;

        private void Awake() {
            img_icn = transform.Find("icon").GetComponent<Image>();
            text_amnt = transform.Find("qty").GetComponent<Text>();
            Refresh(null);
        }

        public void Refresh(Item item)
        {
            if(item == null)
            {
                img_icn.sprite = null;
                img_icn.color = new Color(255,255,255,0);
                text_amnt.text = string.Empty;
                return;
            }

            img_icn.sprite = item.ItemIcon;
            img_icn.type = Image.Type.Simple;
            img_icn.preserveAspect = true;
            img_icn.color = Color.white;
            text_amnt.text = (item.amount > 1) ? "X " + item.amount : string.Empty;
        }

    }

}