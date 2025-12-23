using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что столкнулся объект с тегом "Player"
        if (other.CompareTag("Player"))
        {
            // Завершаем игру
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}
