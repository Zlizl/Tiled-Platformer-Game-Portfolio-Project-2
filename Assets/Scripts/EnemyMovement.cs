using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    BoxCollider2D flipCollider;
    [SerializeField] public int enemyHealth;

    [SerializeField] float moveSpeed = 1f;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        flipCollider = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        myRigidbody.linearVelocity = new UnityEngine.Vector2 (moveSpeed, 0f);
    }

    void  OnTriggerEnter2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
        flipEnemyFacing();
    }

    void flipEnemyFacing()
    {
        transform.localScale = new UnityEngine.Vector2 (-(Mathf.Sign(myRigidbody.linearVelocity.x)),1f);
    }
     
        
}
