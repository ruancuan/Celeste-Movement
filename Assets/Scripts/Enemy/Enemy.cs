using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyMove enemyMove;
    private EnemyAnimation enemyAnimation;
    private Attribute attribute;
    private BoxCollider2D collider2D;
    private Animator animator;
    private Rigidbody2D rigidbody2D;

    public float hitInterval = 1;

    private int corpseLayer;

    //private 

    private void Awake()
    {
        enemyAnimation = GetComponent<EnemyAnimation>();
        attribute = GetComponent<Attribute>();
        enemyMove = GetComponent<EnemyMove>();
        collider2D = GetComponent<BoxCollider2D>();
        animator = GetComponentInChildren<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        corpseLayer = LayerMask.NameToLayer("Corpse");

        StateMachineBehaviour[] behaviours= animator.GetBehaviours(EnemyAnimation.hitAnimationHash,0);
        for (int k = 0; k < behaviours.Length; k++) {
            AnimatorExten animatorExten = behaviours[k] as AnimatorExten;
            if (animatorExten != null) {
                animatorExten.enterHandle += DisableMoveComponent;
                animatorExten.exitHandle += EnableMoveComponent;
            }
        }

        InitEventListener();
    }

    public void DisableMoveComponent() {
        this.enemyMove.enabled = false;
    }

    public void EnableMoveComponent()
    {
        this.enemyMove.enabled = true;
    }

    private void InitEventListener() {
        if (attribute != null) {
            attribute.AddAttributeListener(AttributeType.Hp, OnEnemyHit);
        }   
    }

    private float lastHitTime = 0;

    private void OnEnemyHit() {
        if (enemyAnimation != null)
        {
            if (Time.realtimeSinceStartup - lastHitTime >= this.hitInterval)
            {
                lastHitTime = Time.realtimeSinceStartup;
                enemyAnimation.PlayHitAnimation();
                if (attribute.GetAttributeByType(AttributeType.Hp) <= 0)
                {
                    this.DeadHandle();
                }
            }
        }
    }

    private void DeadHandle()
    {
        enemyAnimation.PlayDeadAnimation();
        //this.collider2D.enabled = false;
        this.enemyMove.enabled = false;
        //Destroy(rigidbody2D);
        this.gameObject.layer = corpseLayer;
    }
}
