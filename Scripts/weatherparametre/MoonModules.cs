using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonModules : DNBaseControl {

    [SerializeField] private Light moon;
    [SerializeField] private Gradient moonColor;
    [SerializeField] private float moonIntensityBase;




    public override void UpdateModul(float intensity)
    {
        moon.color = moonColor.Evaluate(1 - intensity);
        moon.intensity = (1 - intensity) * moonIntensityBase * 0.05f;
    }

   
}
