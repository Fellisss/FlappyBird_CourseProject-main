
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody2D rb;

    public AudioClip hitSound;
    private AudioSource audioSource;

    private float tiltSmooth = 5f;
    private float maxUpRotation = 15f;
    private float maxDownRotation = -25f;
    private float targetRotation = 0f;

    private GameManager gameManager;

    private bool canTakeDamage = true; // защита от спама столкновений
    public float damageCooldown = 0.5f; // задержка между ударами

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // прыжок
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * jumpForce;
            SFXManager.instance.PlayJump();
            targetRotation = maxUpRotation;
        }

        // падение — наклон вниз
        if (rb.velocity.y < 0)
        {
            targetRotation = maxDownRotation;
        }

        // плавный поворот
        float currentZ = transform.rotation.eulerAngles.z;
        if (currentZ > 180) currentZ -= 360;

        float newZ = Mathf.Lerp(
            currentZ,
            targetRotation,
            tiltSmooth * Time.deltaTime
        );

        transform.rotation = Quaternion.Euler(0, 0, newZ);
    }

    // ❌ Collision больше не используем для метеоритов
    void OnCollisionEnter2D(Collision2D collision)
    {
        // оставлено пустым специально
    }

    // ✅ ВСЕ столкновения теперь здесь
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 💥 метеорит
        if (collision.CompareTag("Meteor"))
        {
            if (!canTakeDamage) return;
            TakeDamage();
        }

        // ☠️ зона смерти
        if (collision.CompareTag("Death"))
        {
            gameManager.GameOver();
        }

        // 🎯 зона очков (если снова понадобится)
        // if (collision.CompareTag("ScoreZone"))
        // {
        //     gameManager.AddScore();
        //     Destroy(collision.gameObject);
        // }
    }

    void TakeDamage()
    {
        canTakeDamage = false;

        if (hitSound != null)
            audioSource.PlayOneShot(hitSound);

        gameManager.LoseLife();

        Invoke(nameof(ResetDamage), damageCooldown);
    }

    void ResetDamage()
    {
        canTakeDamage = true;
    }
}

//using UnityEngine;

//public class Bird : MonoBehaviour
//{
//    public float jumpForce = 5f;
//    private Rigidbody2D rb;

//    public AudioClip hitSound;
//    private AudioSource audioSource;

//    private float tiltSmooth = 5f;
//    private float maxUpRotation = 15f;
//    private float maxDownRotation = -25f;
//    private float targetRotation = 0f;

//    private GameManager gameManager;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        audioSource = GetComponent<AudioSource>();
//        gameManager = FindObjectOfType<GameManager>();
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
//        {
//            rb.velocity = Vector2.up * jumpForce;
//            SFXManager.instance.PlayJump();
//            targetRotation = maxUpRotation;
//        }

//        if (rb.velocity.y < 0)
//        {
//            targetRotation = maxDownRotation;
//        }

//        float currentZ = transform.rotation.eulerAngles.z;
//        if (currentZ > 180) currentZ -= 360;
//        float newZ = Mathf.Lerp(currentZ, targetRotation, tiltSmooth * Time.deltaTime);
//        transform.rotation = Quaternion.Euler(0, 0, newZ);
//    }

//    void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Pipe"))
//        {
//            audioSource.PlayOneShot(hitSound);
//            gameManager.LoseLife();
//        }
//    }
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Death"))
//        {
//            FindObjectOfType<GameManager>().GameOver();
//        }
//    }

//}