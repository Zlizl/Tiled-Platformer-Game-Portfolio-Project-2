using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    [SerializeField] float bulletSpeed = 20f;
    PlayerMovement player;
    float xSpeed;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }
    
    void Update()
    {
        myRigidbody.linearVelocity = new UnityEngine.Vector2 (xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        if(other.CompareTag("StrongEnemy"))
        {
            EnemyMovement enemyMovement = other.GetComponent<EnemyMovement>();
            
            if (enemyMovement.enemyHealth !=0)
            {
                enemyMovement.enemyHealth--;
                if (enemyMovement.enemyHealth <=0)
                {            
                    Destroy(other.gameObject);
                }
            }
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject, 1f);
    }
}
