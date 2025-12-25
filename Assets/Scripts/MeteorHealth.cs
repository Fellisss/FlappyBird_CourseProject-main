using UnityEngine;

public class MeteorHealth : MonoBehaviour
{
    public GameObject destroyEffect; // сюда перетащим префаб эффекта

    public void TakeDamage()
    {
        // Создаём эффект разрушения
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }

        // Уничтожаем метеорит
        Destroy(gameObject);
    }
}