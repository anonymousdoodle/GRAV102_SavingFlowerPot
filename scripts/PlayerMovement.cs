using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 4f;
    public float sideSpeed = 4f;
    public float jumpForce = 5f;
    public float fallMultiplier = 2.4f;

    private Rigidbody rb;
    private Animator animator;

    public Transform groundCheck;
    public LayerMask groundLayer;

    public bool canMove = true;


    public Image[] hearts;
    private int currentHealth;


    public AudioSource jumpSFX;


    public GameObject loseCanvas;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        if (animator != null)
        {
            animator.SetBool("isRunning", true);
        }

        currentHealth = hearts.Length;

        if (loseCanvas != null)
            loseCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (!canMove) return;

        float moveX = Input.GetAxisRaw("Horizontal") * sideSpeed;
        float moveZ = Input.GetAxisRaw("Vertical") * forwardSpeed;

        Vector3 move = new Vector3(moveX, rb.velocity.y, moveZ);
        rb.velocity = move;

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            if (jumpSFX != null) jumpSFX.Play();
        }

        // Apply extra gravity for faster fall
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        // Animator parameters
        if (animator != null)
        {
            animator.SetBool("isRunningForward", moveZ > 0);
            animator.SetBool("isRunningBackward", moveZ < 0);
            animator.SetBool("isJumping", !IsGrounded());
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyTrigger"))
        {
            if (currentHealth > 0)
            {
                currentHealth--;
                hearts[currentHealth].enabled = false;
                Debug.Log("Player hit! Hearts left: " + currentHealth);

                if (currentHealth <= 0)
                {
                    TriggerLose();
                }
            }
        }
    }

    void TriggerLose()
    {
        canMove = false;
        rb.velocity = Vector3.zero;

        if (animator != null)
        {
            animator.SetBool("isRunning", false);
        }

        if (loseCanvas != null)
        {
            loseCanvas.SetActive(true);
        }

        Debug.Log("Game Over! Player lost.");

    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
    }
}