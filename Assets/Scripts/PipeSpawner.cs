using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorColumnPrefab;
    public float spawnX = 10f;
    public float spawnRate = 2f;

    public GameObject crystalPrefab;
    [Range(0f, 1f)]
    public float crystalChance = 0.4f; //40% шанс появления

    private float timer;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnColumn();
            timer = 0f;
        }
    }

    void SpawnColumn()
    {
        float topY = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        float bottomY = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        // высота всей колонны (3 метеорита)
        float columnHeight = 6f;

        float safeMinY = bottomY + columnHeight / 2f;
        float safeMaxY = topY - columnHeight / 2f;

        float spawnY = Random.Range(safeMinY, safeMaxY);

        Instantiate(
            meteorColumnPrefab,
            new Vector3(spawnX, spawnY, 0),
            Quaternion.identity
        );

    }
}