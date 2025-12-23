
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
    private bool isDead = false; // защита от повторных столкновений

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (isDead) return; // если игра окончена — ничего не делаем

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

    // 💥 столкновение с метеоритами / колоннами
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Pipe"))
        {
            isDead = true;

            if (hitSound != null)
                audioSource.PlayOneShot(hitSound);

            gameManager.LoseLife();
        }
    }

    // ☠️ зона смерти (выход за экран, нижняя/верхняя граница)
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.CompareTag("Death"))
        {
            isDead = true;
            gameManager.GameOver();
        }
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