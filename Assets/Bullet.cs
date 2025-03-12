using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    public int damage = 1;
    private Vector2 direction;

    private void Start()
    {

        // Set the bullet direction based on the player's facing direction
        direction = transform.right;
        Destroy(gameObject, lifeTime); // Destroy bullet after a few seconds
                                       
    }

    private void Update()
    {
        // Move the bullet
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Damage the enemy
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject); // Destroy bullet on impact
        }
    }
}
