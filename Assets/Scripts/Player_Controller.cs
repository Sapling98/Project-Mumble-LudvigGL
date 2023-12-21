using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float coyoteTime = 0.1f;
    public float jumpCooldown = 1f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    float moveX;
    bool isFacingRight = true;

    private Rigidbody2D rb;
    private float coyoteTimer;
    private bool canJump = true;

    private Animator anim;
    public string currentAnimation;
    const string PLAYER_WALK = "Player_WalkAN";
    const string PLAYER_IDLE = "Player_IdleAN";

    const string JumpDOWN = "JumpDOWN";
    const string JumpTOP = "JumpTOP";
    const string JumpUP = "JumpUP";

    public AudioClip[] jumpSounds;
    public AudioClip fallingStart;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        AudioSource.PlayClipAtPoint(fallingStart, transform.position);
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        if (isGrounded())
        {
            coyoteTimer = coyoteTime;
            canJump = true;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && (isGrounded() || coyoteTimer > 0) && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
            Invoke(nameof(ResetJump), jumpCooldown);
            AudioClip randomJumpSound = jumpSounds[Random.Range(0, jumpSounds.Length)];
            AudioSource.PlayClipAtPoint(randomJumpSound, transform.position);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (!isGrounded())
        {
            if (rb.velocity.y <= 3 && rb.velocity.y >= -3)
            {
                ChangeAnimationState(JumpTOP);
            }
            else if (rb.velocity.y > 3)
            {
                ChangeAnimationState(JumpUP);
            }
            else if (rb.velocity.y < -3)
            {
                ChangeAnimationState(JumpDOWN);
            }
        }
        else if (rb.velocity.x != 0)
        {
            ChangeAnimationState(PLAYER_WALK);
        }
        else
        {
            ChangeAnimationState(PLAYER_IDLE);
        }

        Flip();
    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
    }


    private void ResetJump()
    {
        canJump = true;
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentAnimation == newState) return;
        anim.Play(newState);
        currentAnimation = newState;
    }

    void Flip()
    {
        if (isFacingRight && moveX < 0f || !isFacingRight && moveX > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1;
            transform.localScale = localscale;
        }
    }


}
