using UnityEngine;

public class CoinPickup : MonoBehaviour
{
   
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsforCoin = 100;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(coinPickupSFX, transform.position);
            Destroy (gameObject);
            FindFirstObjectByType<GameSession>().AddToScore(pointsforCoin);
        }
        
    }

}
