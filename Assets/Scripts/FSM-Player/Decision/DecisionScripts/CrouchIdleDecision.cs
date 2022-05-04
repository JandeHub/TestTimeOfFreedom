using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Player/Decision/CrouchIdleDecision")]
public class CrouchIdleDecision : FSM.Decision
{
    private bool getCrouched;
    private float ver;
    private float hor;

    public override bool Decide(Controller controller)
    {
        getCrouched = controller.ReturnCrouched();
        ver = controller.ReturnHor();
        hor = controller.ReturnVer();

        bool t = (getCrouched && ver == 0 && hor == 0);

        return t;
    }
}
