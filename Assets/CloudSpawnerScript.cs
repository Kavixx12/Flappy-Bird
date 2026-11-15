using UnityEngine;

public class CloudSpawnerScript : MonoBehaviour
{
    public GameObject cloudPrefab;
    public float spawnRate = 5f; 
    private float timer = 0f;
    public float heightOffset = 2f; 

    void Start()
    {
        SpawnCloud();
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            SpawnCloud();
            timer = 0f;
        }
    }

    void SpawnCloud()
    {
        
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        Instantiate(cloudPrefab, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}