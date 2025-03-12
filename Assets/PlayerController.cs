using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    public int health = 10;
    private HealthUI healthUI;
    public GameObject gameOverScreen;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthUI = Object.FindFirstObjectByType<HealthUI>();
        healthUI.SetHealth(health);
        gameOverScreen.SetActive(false);
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }


        // **Shoot when pressing Z**
        if (Input.GetKeyDown(KeyCode.Z) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        if (moveInput != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthUI.SetHealth(health);  // Update UI when health changes
        if (health <= 0)
        {
            Die();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        // Make sure the bullet moves in the direction the player is facing
        if (transform.localScale.x < 0)
        {
            bullet.transform.right = Vector3.left;  // Flip the bullet if player is facing left
        }
    }

    private void Die()
    {
        Debug.Log("Game Over triggered!");
        Object.FindFirstObjectByType<GameOverManager>().ShowGameOver();
        gameOverScreen.SetActive(true); // Show the Game Over screen
        Time.timeScale = 0; // Pause the game

    }

}
