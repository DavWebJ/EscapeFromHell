using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Input : MonoBehaviour
{
  [Header("Input Key")]
    public KeyCode input_actionPrimary = KeyCode.Mouse0;
    public KeyCode input_actionSecondary = KeyCode.Mouse1;
    public KeyCode input_moveobject = KeyCode.Mouse2;
    public KeyCode input_pickup = KeyCode.E;
    public KeyCode input_equip = KeyCode.F;
    public KeyCode input_dropobject = KeyCode.G;
    public KeyCode input_openInventory = KeyCode.Tab;
    public KeyCode input_inspect = KeyCode.T;
    public KeyCode input_run = KeyCode.LeftShift;
    public KeyCode input_jump = KeyCode.Space;
    public KeyCode input_crouch = KeyCode.AltGr;
    public KeyCode input_reload = KeyCode.R;
    public KeyCode input_LightOnOff = KeyCode.O;

    
    [Header("Input Gamepad")]
    public string gamepad_input_inventory = "";
    public string gamepad_input_actionPrimary = "";
    public string gamepad_input_actionSecondary = "";
    public string gamepad_input_run = "";
    public string gamepad_input_jump = "";
    public string gamepad_input_moveObject = "";
    public string gamepad_input_drop = "";
    public string gamepad_input_crouch = "";
    public string gamepad_input_lightAttack = "";
    public string gamepad_input_heavyattak= "";
    public string gamepad_input_split= "";
    public string gamepad_input_dropAll= "";
    public string gamepad_input_use_item= "";
    public string gamepad_input_equiped_weapon= "";
    public string gamepad_input_fire= "";
    public string gamepad_input_reload_weapon= "";
    public string gamepad_input_reload_flashlight= "";
    public string gamepad_input_aiming= "";


}
