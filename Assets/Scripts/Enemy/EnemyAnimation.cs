using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private const string hitAnimationName = "hit";
    private const string deadAnimationName = "dead";

    public Animator animator;
    public void PlayHitAnimation() {
        if (animator) {
            animator.SetTrigger(hitAnimationName);
        }
    }

    public void PlayDeadAnimation() {
        if (animator) {
            animator.SetTrigger(deadAnimationName);
        }
    }
}
