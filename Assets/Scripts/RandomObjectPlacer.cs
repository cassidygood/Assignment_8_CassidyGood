using UnityEngine;

public class RandomObjectPlacer : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Assign tree, rock, etc. prefabs here
    public Terrain terrain;             // Drag your terrain here in the Inspector
    public int numberOfObjects = 100;

    public float minScale = 0.8f;
    public float maxScale = 1.5f;

    void Start()
    {
        PlaceObjects();
    }

    void PlaceObjects()
    {
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainSize = terrainData.size;

        for (int i = 0; i < numberOfObjects; i++)
        {
            // Pick a random prefab
            GameObject prefabToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

            // Get random position on terrain
            float x = Random.Range(0, terrainSize.x);
            float z = Random.Range(0, terrainSize.z);
            float y = terrain.SampleHeight(new Vector3(x, 0, z));

            Vector3 spawnPosition = new Vector3(x, y, z) + terrain.transform.position;

            // Random rotation and scale
            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
            float randomScale = Random.Range(minScale, maxScale);

            GameObject spawned = Instantiate(prefabToSpawn, spawnPosition, randomRotation);
            spawned.transform.localScale *= randomScale;
        }
    }
}

