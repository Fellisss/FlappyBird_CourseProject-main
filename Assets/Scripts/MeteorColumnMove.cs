using UnityEngine;

public class MeteorColumnMove : MonoBehaviour
{
    public float speed = 4f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}