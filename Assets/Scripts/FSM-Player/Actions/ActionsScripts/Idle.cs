using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Player/Actions/Idle")]
public class Idle : FSM.Action
{
    public override void Act(Controller controller)
    {
        controller.SetAnimation("idle", true);
        controller.SetAnimation("idleCrouch", false);
        controller.SetAnimation("jump", false);
        controller.SetAnimation("walkBackward", false);
        controller.SetAnimation("walkCrouchBackward", false);
        controller.SetAnimation("walkForward", false);
        controller.SetAnimation("walkCrouchForward", false);
        controller.SetAnimation("walkLeft", false);
        controller.SetAnimation("walkCrouchLeft", false);
        controller.SetAnimation("walkRight", false);
        controller.SetAnimation("walkCrouchRight", false);
    }
}