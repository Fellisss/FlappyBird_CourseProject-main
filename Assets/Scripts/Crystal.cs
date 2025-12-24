using UnityEngine;

public class Crystal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // можно добавить звук или бонус
            Destroy(gameObject);
        }
    }
}
