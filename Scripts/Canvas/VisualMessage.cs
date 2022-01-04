using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using BlackPearl;
public class VisualMessage : MonoBehaviour
{

    private Text item_name = null;
    private Text qty = null;
    private Text visualMessage = null;
    public Image icon = null;

    private void Awake() {
        
       item_name = transform.Find("name").GetComponent<Text>();
       qty = transform.Find("qty").GetComponent<Text>();
       visualMessage = transform.Find("message").GetComponent<Text>();
    
        icon = transform.Find("icon").GetComponent<Image>();
        

        item_name.text = string.Empty;
        qty.text = "";
        visualMessage.text = "";
    }

    private void Start() {
        Destroy(gameObject,4f);
    }

    public void SendVisualMessage(bool add, Item item)
    {
        if(item == null)
        {
            Destroy(gameObject);
        }else
        {
            visualMessage.text = (add) ? "+" : "-";
            visualMessage.color = (add) ? Color.green  : Color.red;
            qty.text = item.amount.ToString();
            qty.color = (add) ? Color.green  : Color.red;
            icon.sprite = item.ItemIcon;
            icon.preserveAspect = true;
            item_name.text = item.ItemName;



        }
        
    }

    public void SendVisualMessage(string message,Color color)
    {
        if(message == string.Empty)
        {
            Destroy(gameObject);
        }else
        {
            qty.text = string.Empty;
            item_name.text = string.Empty;
            icon.gameObject.SetActive(false);
            icon.sprite = null;
            visualMessage.text = message;
            visualMessage.color = color;

        }
    }

  
    void Update()
    {
        
    }
}
