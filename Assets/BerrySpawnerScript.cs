using UnityEngine;

public class BerrySpawnerScript : MonoBehaviour
{
    
    public GameObject[] berryPrefabs;
    public float spawnRate = 12f;
    private float timer = 0f;
    public float heightOffset = 4f;

    void Start()
    {
        SpawnBerry();
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnBerry();
            timer = 0f;
        }
    }

    void SpawnBerry()
    {
        
        if (berryPrefabs.Length == 0) return;

        
        int randomIndex = Random.Range(0, berryPrefabs.Length);
        GameObject selectedBerry = berryPrefabs[randomIndex];

        
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        Instantiate(selectedBerry, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}