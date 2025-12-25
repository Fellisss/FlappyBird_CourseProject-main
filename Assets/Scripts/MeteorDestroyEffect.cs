using UnityEngine;

public class MeteorDestroyEffect : MonoBehaviour
{
    public GameObject destroyEffect; // префаб эффекта разрушения

    void OnDestroy()
    {
        // Если объект уничтожается и у нас есть эффект
        if (destroyEffect != null)
        {
            // Создаём эффект на позиции метеорита
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }
    }
}
