using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BlackPearl;
public class HUDInfos : MonoBehaviour
{
    public static HUDInfos instance = null;
    [Header("Object Scene Infos: ")]
    [SerializeField] private GameObject sceneObjectInfos = null;
    [SerializeField] private Text sceneObjectNameText = null;
    [SerializeField] private Image sceneObjectInfosIcon = null;
    [SerializeField] private Text sceneObjectInfosText = null;

    [Header("Object Scene Actions: ")]
    [SerializeField] private GameObject action_pickup = null;
    [SerializeField] private GameObject action_equip = null;
    [SerializeField] private GameObject action_interract = null;
    [SerializeField] private GameObject action_flashlight = null;
    [SerializeField] private GameObject action_reload = null;
    // [SerializeField] private GameObject action_move = null;
    
    private void Awake() {
        if(instance == null)
            instance = this;
    }
    private void Start() {
        SceneObjectInfos(null);
        SetUpActionsInputsText(action_pickup, GameManager.instance.input.input_pickup.ToString());
        SetUpActionsInputsText(action_equip, GameManager.instance.input.input_equip.ToString());
        SetUpActionsInputsText(action_interract, GameManager.instance.input.input_actionPrimary.ToString());
        // SetUpActionsInputsText(action_move,GameManager.instance.input.input_moveobject + " pour déplacer");
        FlashlightInput(false);
        ReloadInput(false);
    }

    public void SceneObjectInfos(Interract interract)
    {
        SceneObjectInputs(interract);
        if(interract == null || interract.itemRef == null)
        {

            if(sceneObjectInfos.activeSelf)
            sceneObjectInfosText.text = string.Empty;
            sceneObjectInfos.SetActive(false);
        }
        else
        {
            string quality = string.Empty;
            if(interract.itemRef.attributes.name == "Liquide")
            {
                quality = " (" + interract.itemRef.attributes.GetPercentage() * 100 + " % - état = " + interract.itemRef.attributes.quality + " )";
            }
            sceneObjectInfos.SetActive(true);
            sceneObjectNameText.text = interract.itemRef.ItemName.ToUpper();
            sceneObjectInfosIcon.sprite = interract.itemRef.ItemIcon;
            sceneObjectInfosText.text = (interract.itemRef.amount > 1) ? "x" + interract.itemRef.amount + " " + interract.itemRef.ItemDescription : interract.itemRef.ItemDescription;
        }

    }

    private void SceneObjectInputs(Interract interract)
    {
        action_pickup.SetActive(interract != null && interract.actionType == Interract.ActionType.pickable);
        action_equip.SetActive(interract != null && interract.actionType == Interract.ActionType.equipable);
        action_interract.SetActive(interract != null && interract.actionType == Interract.ActionType.interractable);

        // action_move.SetActive(interract != null && interract.isMovable);
    }

    public void FlashlightInput(bool activate)
    {
        action_flashlight.SetActive(activate);
    }
    public void ReloadInput(bool activate)
    {
        action_reload.SetActive(activate);
    }

    private void SetUpActionsInputsText(GameObject action, string text_actions)
    {
        // action.transform.Find("TextInputtInfos").GetComponent<Text>().text = text_actions;
    }

    void Update()
    {
        
    }
}
