using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCanvas : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.maxValue = HealthManager.maxPlayerHealth;

        healthSlider.value = HealthManager.playerHealth;
    }
}
