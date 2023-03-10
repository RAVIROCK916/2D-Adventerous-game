using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // player components
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    // player speed variables
    float dirX;
    [SerializeField] private float upSpeed = 10;
    [SerializeField] private float moveSpeed = 10;

    //player animation state
    private enum PlayerState
    {
        idle,
        running,
        jumping,
        falling
    }

    [SerializeField] private AudioSource jumpSound;

    // Start is called before the first frame update
    void Start()
    {
        // component initialization
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // horizontal movement
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        // jump movement
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, upSpeed);
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        PlayerState state;
        // animation for left and right movements
        if (dirX < 0)
        {
            state = PlayerState.running;
            sprite.flipX = true;
        }
        else if (dirX > 0)
        {
            state = PlayerState.running;
            sprite.flipX = false;
        }
        else
        {
            state = PlayerState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = PlayerState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = PlayerState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
