using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Options : MonoBehaviour
{
    [Header("Outlines : ")]
    public Outline.Mode outline_mode = Outline.Mode.OutlineAll;
    public Color32 outline_color = new Color32(255,255,255,255);
    public float outlin_width = 2f;
    public bool enabledPad = false;

    public float user_mouseSensitivity = 100;
    public float multiMouseSensi = 1;

    public float MouseSensitivity => user_mouseSensitivity * multiMouseSensi;

}
