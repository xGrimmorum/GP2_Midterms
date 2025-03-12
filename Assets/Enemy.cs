using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int health = 3;
    public Transform leftPoint, rightPoint; // Waypoints
    private bool movingLeft = true;

    void Update()
    {
        Move();
    }

    void Move()
    {

        if (movingLeft)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;

            if (transform.position.x <= leftPoint.position.x)
            {
                movingLeft = false;
                Flip();
            }
        }
        else
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (transform.position.x >= rightPoint.position.x)
            {
                movingLeft = true;
                Flip();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(1);
        }
    }


    void Flip()
    {
        // Flip the enemy sprite
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject); // Enemy dies
        }
    }


}
