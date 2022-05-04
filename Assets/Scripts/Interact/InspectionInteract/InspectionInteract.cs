using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InspectionInteract : InteractManager
{
    public GameObject Inspection;
    public InspectionObject inspectionObject;
    public int index;

    public event Action GainAbility = delegate { };
    void UpdateInspector()
    {
        Inspection.SetActive(true);
        inspectionObject.TurnOnInspection(index);
        GainAbility();
    }

    public override string GetDescription()
    {
        return "Press [E] to inspect the object";

    }

    public override void Interact()
    {
        UpdateInspector();
    }
}
