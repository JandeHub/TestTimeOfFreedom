using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaCanvas : MonoBehaviour
{
    [SerializeField]
    private Slider staminaSlider;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        staminaSlider.maxValue = StaminaManager.maxStamina;

        staminaSlider.value = StaminaManager.currentStamina;
    }
}
