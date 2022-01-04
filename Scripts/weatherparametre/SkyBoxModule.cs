using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxModule : DNBaseControl {

    [SerializeField] private Gradient skyColor;
    [SerializeField] private Gradient HorizonColor;
    [SerializeField] private Light sun;
    public float matinAmbienceIntensiter = 0.6f;
    public float journeeAmbienceIntensiter = 1.2f;
    public float soireeAmbienceIntensiter = 0.3f;
    public float nuitAmbienceIntensiter = 0;
    public float minPoint = -0.2f;
    public float minAmbientPoint = -0.2f;
    public float dayAtmosphereThickness = 0.5f;
    public float nightAtmosphereThickness = 1.3f;
    public float dayExposure = 2;
    public float nightExposure = 1.1f;
    public float _Matinintensiter = 0.6f;                            // intensitée lumiere lever du soleil
    public float _JourneeIntensiter = 1.7f;                            // intensitée lumiere journéé
    public float _SoireeIntensiter = 0.4f;                           // intensitée lumiere coucher du soleil
    public float _NuitIntensiter = 0.0f;
    public float sunSizeLever = 0.3f;
    public float sunSizeJournée = 0.1f;
     

    public override void UpdateModul(float intensity)
    {
        RenderSettings.skybox.SetColor("_SkyTint", skyColor.Evaluate(intensity));
        RenderSettings.skybox.SetColor("_GroundColor", HorizonColor.Evaluate(intensity));

       



        float tRange = 1 - minPoint;
        float dot = Mathf.Clamp01((Vector3.Dot(sun.transform.forward, Vector3.down) - minPoint) / tRange);
        float i = ((_JourneeIntensiter - _NuitIntensiter) * dot) + _NuitIntensiter;

       

        tRange = 1 - minAmbientPoint;

        dot = Mathf.Clamp01((Vector3.Dot(sun.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
        i = ((journeeAmbienceIntensiter - nuitAmbienceIntensiter) * dot) + nuitAmbienceIntensiter;
        RenderSettings.ambientIntensity = i;


        i=((sunSizeJournée-sunSizeLever) * dot) + sunSizeLever;
        RenderSettings.skybox.SetFloat("_SunSize", i);


        i = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
        RenderSettings.skybox.SetFloat("_AtmosphereThickness", i);

        i = ((dayExposure - nightExposure) * dot) + nightExposure;
        RenderSettings.skybox.SetFloat("_Exposure", i);

    }

 
}
