using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Player/Decision/JumpDecision")]
public class JumpDecision : FSM.Decision
{
    private bool getFloor;

    public override bool Decide(Controller controller)
    {
        getFloor = GroundCheckerManager.isGrounded;

        bool t = (!getFloor);

        return t;
    }
}
