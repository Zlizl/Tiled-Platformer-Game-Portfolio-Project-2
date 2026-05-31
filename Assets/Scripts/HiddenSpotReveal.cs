using UnityEngine;

public class HiddenSpotReveal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        //if player touches the hidden platforms, destroy them - reveals the hidden section
        if (collision.CompareTag("Player"))
        {
            Destroy (gameObject);
        }
    }
}
