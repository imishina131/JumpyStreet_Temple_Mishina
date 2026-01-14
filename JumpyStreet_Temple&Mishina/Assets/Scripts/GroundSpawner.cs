using UnityEngine;

public class GroundSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] groundPrefabs;
    [SerializeField] Transform player;

    [SerializeField] float spawnDistance;
    private float spawnPosition;

    private void Start()
    {
        // ground spawn position
        spawnPosition = player.position.z + spawnDistance;
    }

    private void Update()
    {
        // spawn ground if close enough to spawn position
        if (player.position.z >= spawnPosition - spawnDistance)
        {
            Spawn();
            spawnPosition += 1;
        }
    }

    void Spawn()
    {
        // spawn random ground at spawn position
        int i = Random.Range(0, groundPrefabs.Length);
        Vector3 pos = new Vector3(0, 0, spawnPosition);
        Instantiate(groundPrefabs[i], pos, Quaternion.identity);
    }

}

