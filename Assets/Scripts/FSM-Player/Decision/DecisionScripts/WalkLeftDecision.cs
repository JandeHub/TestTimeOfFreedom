using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Player/Decision/WalkLeftDecision")]
public class WalkLeftDecision : FSM.Decision
{
    private float hor;
    private bool getCrouched;

    public override bool Decide(Controller controller)
    {
        hor = controller.ReturnHor();
        getCrouched = controller.ReturnCrouched();

        bool t = (hor < 0 && !getCrouched);

        return t;
    }
}
