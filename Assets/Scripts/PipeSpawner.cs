
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorColumnPrefab;
    public GameObject crystalPrefab;

    public float spawnX = 10f;
    public float spawnRate = 2f;

    public float gapHeight = 3.5f; // размер прохода
    public float columnMoveRange = 2.5f; // насколько колонна двигается вверх/вниз

    [Range(0f, 1f)]
    public float crystalChance = 0.5f;

    private float timer;
    private Camera cam;
    private GameManager gameManager;

    void Start()
    {
        cam = Camera.main;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnColumnWithCrystal();
            timer = 0f;
        }
    }

    void SpawnColumnWithCrystal()
    {
        // ?? РАНДОМНОЕ СМЕЩЕНИЕ КОЛОННЫ
        float offsetY = Random.Range(-columnMoveRange, columnMoveRange);

        // ?? СПАВН КОЛОННЫ
        Instantiate(
            meteorColumnPrefab,
            new Vector3(spawnX, offsetY, 0),
            Quaternion.identity
        );

        // ?? КРИСТАЛЛ — ТОЛЬКО ПОСЛЕ 20 ОЧКОВ
        if (gameManager.score < 2) return;
        if (Random.value > crystalChance) return;

        

        // ?? КООРДИНАТА КРИСТАЛЛА — ВНУТРИ ПРОХОДА
        float crystalY = offsetY + Random.Range(-gapHeight / 2f + 0.4f, gapHeight / 2f - 0.4f);

        Instantiate(
            crystalPrefab,
            new Vector3(spawnX, crystalY, 0),
            Quaternion.identity
        );
    }
}
//using UnityEngine;

//public class MeteorSpawner : MonoBehaviour
//{
//    public GameObject meteorColumnPrefab;
//    public float spawnX = 10f;
//    public float spawnRate = 2f;

//    public GameObject crystalPrefab;
//    [Range(0f, 1f)]
//    public float crystalChance = 0.4f; //40% шанс появления

//    private float timer;
//    private Camera cam;

//    void Start()
//    {
//        cam = Camera.main;
//    }

//    void Update()
//    {
//        timer += Time.deltaTime;

//        if (timer >= spawnRate)
//        {
//            SpawnColumn();
//            timer = 0f;
//        }
//    }

//    void SpawnColumn()
//    {
//        float topY = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
//        float bottomY = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

//        // высота всей колонны (3 метеорита)
//        float columnHeight = 6f;

//        float safeMinY = bottomY + columnHeight / 2f;
//        float safeMaxY = topY - columnHeight / 2f;

//        float spawnY = Random.Range(safeMinY, safeMaxY);

//        Instantiate(
//            meteorColumnPrefab,
//            new Vector3(spawnX, spawnY, 0),
//            Quaternion.identity
//        );

//    }
//}