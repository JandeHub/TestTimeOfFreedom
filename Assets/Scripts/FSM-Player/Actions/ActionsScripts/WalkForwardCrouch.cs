using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Player/Actions/WalkForwardCrouch")]
public class WalkForwardCrouch : FSM.Action
{
    public override void Act(Controller controller)
    {
        controller.SetAnimation("idle", false);
        controller.SetAnimation("idleCrouch", false);
        controller.SetAnimation("jump", false);
        controller.SetAnimation("walkBackward", false);
        controller.SetAnimation("walkCrouchBackward", false);
        controller.SetAnimation("walkForward", false);
        controller.SetAnimation("walkCrouchForward", true);
        controller.SetAnimation("walkLeft", false);
        controller.SetAnimation("walkCrouchLeft", false);
        controller.SetAnimation("walkRight", false);
        controller.SetAnimation("walkCrouchRight", false);
    }
}
