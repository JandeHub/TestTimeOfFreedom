using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSystem : MonoBehaviour
{
    public enum DisplayCategory
    {
        Telekinesis, Waveshock
    }

    public DisplayCategory categoryDisplay;

    private void OnEnable()
    {
        GetComponent<InspectionInteract>().GainAbility += SetAbility;

    }
    private void OnDisable()
    {
        GetComponent<InspectionInteract>().GainAbility -= SetAbility;
    }

 
    void AbilityCategories()
    {
        switch (categoryDisplay)
        {
            case DisplayCategory.Telekinesis:
                DisplayTelekinesis();
                break;

            case DisplayCategory.Waveshock:
                DisplayWaveshock();
                break;
        }
    }

    void DisplayTelekinesis()
    {
        GameObject.FindWithTag("Player").GetComponent<PickupObjects>().enabled = true;
    }

    void DisplayWaveshock()
    {

    }

    void SetAbility()
    {
        AbilityCategories();
    }
}
