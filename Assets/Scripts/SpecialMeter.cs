using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HolyCannon : MonoBehaviour
{

    public Slider slider;

    public void SetMaxMeter(int max)
    {
        slider.maxValue = max;
        slider.value = max;
    }

    public void SetMeter(int meterValue)
    {
        slider.value = meterValue;
    }
}
