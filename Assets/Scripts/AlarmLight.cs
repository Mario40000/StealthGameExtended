using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLight : MonoBehaviour
{
    public float fadeSpeed = 2f;
    public float highIntensity = 2f;
    public float lowIntensity = 0.5f;
    public float changeMargin = 0.2f;
    public bool alarmOn;

    private float targetIntensity;

    private void Awake()
    {
        GetComponent<Light>().intensity = 0f;
        targetIntensity = highIntensity;
    }

    private void Update()
    {
        if(alarmOn)
        {
            GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, targetIntensity, fadeSpeed * Time.deltaTime);
            CheckTargetIntensity();
        }
        else
        {
            GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, 0f, fadeSpeed * Time.deltaTime);
        }
    }

    void CheckTargetIntensity ()
    {
        if(Mathf.Abs(targetIntensity - GetComponent<Light>().intensity) < changeMargin)
        {
            if(targetIntensity == highIntensity)
            {
                targetIntensity = lowIntensity;
            }
            else
            {
                targetIntensity = highIntensity;
            }
        }
    }

}
