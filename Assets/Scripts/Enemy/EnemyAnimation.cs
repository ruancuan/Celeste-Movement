using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public static readonly string hitAnimationName = "hit";
    public static readonly string deadAnimationName = "dead";
    public static readonly int hitAnimationHash = Animator.StringToHash(hitAnimationName);
    public static readonly int deadAnimationHash = Animator.StringToHash(deadAnimationName);

    public Animator animator;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
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
