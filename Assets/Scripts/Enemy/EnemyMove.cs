using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]
    [Header("Collision")]
    public float collisionRadius = 0.25f;
    public Vector2 rightOffset, leftOffset;
    public float speed = 1;
    public float wallJumpLerp = 10;
    private Color debugCollisionColor = Color.red;

    private Vector2 leftDir = new Vector2(-1, 0);
    private Vector2 rightDir = new Vector2(1, 0);
    private Vector2 dir;
    private Rigidbody2D rb;
    public float moveForce=1f;

    // Start is called before the first frame update
    void Start()
    {
        dir = leftDir;
        rb = GetComponent<Rigidbody2D>();
    }

    private Quaternion leftDirRotation = Quaternion.Euler(0, 180, 0);
    private Quaternion rightDirRotation = Quaternion.Euler(0, 0, 0);
    // Update is called once per frame
    void Update()
    {

        bool onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        bool onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        if (onLeftWall)
        {
            dir = rightDir;
            this.transform.rotation = rightDirRotation;
        }
        else if (onRightWall) {
            dir = leftDir;
            this.transform.rotation = leftDirRotation;
        }
    }

    private void FixedUpdate()
    {
        Walk(dir);
    }

    private void Walk(Vector2 dir)
    {
        rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), Time.deltaTime* wallJumpLerp);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }
}
