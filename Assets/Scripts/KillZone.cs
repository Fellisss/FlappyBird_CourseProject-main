using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что столкнулся объект с тегом "Bird"
        if (other.CompareTag("Bird"))
        {
            // Завершаем игру
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}
