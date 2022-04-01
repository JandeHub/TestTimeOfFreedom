using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLight : InteractManager
{
    public Light m_Light;
    public bool isOn;

    private void Start()
    {
        UpdateLight();
    }

    void UpdateLight()
    {
        isOn = !isOn;
        m_Light.enabled = isOn;
        
    }

    public override string GetDescription()
    {
        if (isOn) return "Press [E] to turn <color=red>off</color> the light.";
        return "Press [E] to turn <color=green>on</color> the light.";
    }

    public override void Interact()
    {
        
        UpdateLight();
        Debug.Log("Funcionando");
    }
}
