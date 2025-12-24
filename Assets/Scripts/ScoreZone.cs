using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    private bool scored = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (scored) return;

        if (other.CompareTag("Player"))
        {
            scored = true;
            FindObjectOfType<GameManager>().AddScore();

            // уничтожаем зону, чтобы больше НИКОГДА не срабатывала
            Destroy(gameObject);
        }
    }
}