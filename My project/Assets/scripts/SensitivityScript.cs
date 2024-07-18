using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityScript : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public void Start()
    {
        slider.value = 1f;
    }

    public float GetSens()
    {
        return slider.value;
    }
}
