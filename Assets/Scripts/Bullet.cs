using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Meteor"))
        {
            // Получаем компонент здоровья метеорита
            MeteorHealth meteorHealth = collision.GetComponent<MeteorHealth>();

            if (meteorHealth != null)
            {
                // Вызываем эффект разрушения
                meteorHealth.TakeDamage();
            }
            else
            {
                // На всякий случай старый способ
                Destroy(collision.gameObject);
            }

            // Уничтожаем пулю
            Destroy(gameObject);
        }
    }
}