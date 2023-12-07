using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialMeter : MonoBehaviour
{

    public Slider slider;

    public void SetMinMeter()
    {
        slider.maxValue = 0;
        slider.value = 0;
    }

    public void SetMeter(int meterValue)
    {
        slider.value = meterValue;
    }
}
