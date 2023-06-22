using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorExten : StateMachineBehaviour
{
    public Action enterHandle;
    public Action exitHandle;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (enterHandle != null) {
            enterHandle.Invoke();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        if (exitHandle != null) {
            exitHandle.Invoke();
        }
    }
}
