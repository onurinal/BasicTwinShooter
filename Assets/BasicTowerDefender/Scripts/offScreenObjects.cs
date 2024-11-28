using UnityEngine;

public class OffScreenObjects : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}