using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab; // префаб пули
    public Transform shootPoint;    // точка вылета
    public float bulletSpeed = 10f; // скорость пули
    public float fireRate = 0.3f;   // время между выстрелами

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        // Выстрел по Enter
        if (Input.GetKey(KeyCode.Return) && timer >= fireRate)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        // Создаём пулю
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

        // Делаем её маленькой, если нужно
        bullet.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        // Двигаем пулю вперёд
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.right * bulletSpeed; // летит вперёд относительно пушки
        }

        // Удаляем пулю через 5 секунд, чтобы не засорять сцену
        Destroy(bullet, 5f);
    }
}