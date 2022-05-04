using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Player/Decision/CrouchWalkForwardDecision")]
public class CrouchWalkForwardDecision : FSM.Decision
{
    private float ver;
    private bool getCrouched;

    public override bool Decide(Controller controller)
    {
        ver = controller.ReturnVer();
        getCrouched = controller.ReturnCrouched();

        bool t = (ver > 0 && getCrouched);

        return t;
    }
}
