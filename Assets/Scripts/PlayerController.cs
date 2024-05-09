using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float moveSpeed = 5f;
    public float climbSpeed = 5f;
    public float boundaryXMin = -8f;
    public float boundaryXMax = 8f;
    public Transform groundCheck;
    public Transform ladderCheck;
    public LayerMask groundLayer;
    public LayerMask ladderLayer;
    public AudioClip jumpSound;
    private Rigidbody2D rb;
    public Animator animator;
    private bool isGrounded;
    private bool isFacingRight = true;
    private bool isLadder;
    private bool canMove = true;
    private bool isClimbing = false;
    public AudioClip dieSound; // Sound played when triggered
    public Animator animLoader;
    private int keyCounter = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void HandleInput()
    {
        if (canMove)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
            isLadder = Physics2D.OverlapCircle(ladderCheck.position, 0.1f, ladderLayer);

            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            else
            {
                animator.SetBool("IsJumping", false);
            }

            if (isLadder && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0)
            {
                isClimbing = true;
            }
            else
            {
                isClimbing = false;
            }
        }
    }

    void MovePlayer()
    {
        if (!isClimbing)
        {
            if (canMove)
            {
                float horizontalInput = Input.GetAxis("Horizontal");
                float moveVelocity = horizontalInput * moveSpeed;
                rb.velocity = new Vector2(moveVelocity, rb.velocity.y);

                float clampedX = Mathf.Clamp(rb.position.x, boundaryXMin, boundaryXMax);
                rb.position = new Vector2(clampedX, rb.position.y);

                if (horizontalInput > 0 && !isFacingRight)
                {
                    Flip();
                }
                else if (horizontalInput < 0 && isFacingRight)
                {
                    Flip();
                }

                animator.SetBool("IsRunning", Mathf.Abs(horizontalInput) > 0);
            }
        }
        else
        {
            float verticalInput = Input.GetAxis("Vertical");
            float climbVelocity = verticalInput * climbSpeed;
            rb.velocity = new Vector2(rb.velocity.x, climbVelocity);
        }
    }

    void Jump()
    {
        animator.SetBool("IsJumping", true);
        AudioManager.PlaySound(jumpSound);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
        if (collision.CompareTag("Key"))
        {
            keyCounter++;
            Destroy(collision.gameObject);
        }
    }
    public int KeyCounter()
    {
        return keyCounter;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void FreezeMovement()
    {
        canMove = false;
    }
    public void RunBurnSound()
    {
        AudioManager.PlaySound(dieSound);
    }
    public void PLayScene()
    {

        StartCoroutine("LoadLevel");

    }
    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(2.3f);
        animLoader.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GamePlayScene");
    }

}
