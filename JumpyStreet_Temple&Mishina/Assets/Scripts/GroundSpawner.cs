using UnityEngine;
using System.Collections.Generic;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject[] tilePrefabs;

    [SerializeField] float stepLength = 1f;
    [SerializeField] int tileCount = 20;

    [SerializeField] int behind = 3; // keep this many tiles behind player

    List<Queue<GameObject>> pools = new List<Queue<GameObject>>();
    GameObject[] activeTiles;

    int nextIndex = 0;
    int lastPlayerZ;

    void Start()
    {
        for (int i = 0; i < tilePrefabs.Length; i++)
            pools.Add(new Queue<GameObject>());

        activeTiles = new GameObject[tileCount];

        // start so we have "behind" tiles behind player
        int startZ = PlayerZ() - behind;

        for (int i = 0; i < tileCount; i++)
            activeTiles[i] = GetRandomTile(new Vector3(0, 0, (startZ + i) * stepLength));

        lastPlayerZ = PlayerZ();
    }

    void Update()
    {
        int z = PlayerZ();
        if (z > lastPlayerZ)
        {
            int newZ = z + (tileCount - 1 - behind); // <-- key line
            RecycleTile(newZ);
            lastPlayerZ = z;
        }
    }

    int PlayerZ() => Mathf.RoundToInt(player.position.z / stepLength);

    void RecycleTile(int newZ)
    {
        ReturnToPool(activeTiles[nextIndex]);

        activeTiles[nextIndex] = GetRandomTile(new Vector3(0, 0, newZ * stepLength));
        nextIndex = (nextIndex + 1) % tileCount;
    }

    GameObject GetRandomTile(Vector3 pos)
    {
        int i = Random.Range(0, tilePrefabs.Length);
        GameObject t = pools[i].Count > 0 ? pools[i].Dequeue() : Instantiate(tilePrefabs[i]);

        t.transform.position = pos;
        t.transform.rotation = Quaternion.identity;
        t.SetActive(true);
        return t;
    }

    void ReturnToPool(GameObject t)
    {
        t.SetActive(false);

        // simple "which prefab pool?" check by name
        for (int i = 0; i < tilePrefabs.Length; i++)
        {
            if (t.name.StartsWith(tilePrefabs[i].name))
            {
                pools[i].Enqueue(t);
                return;
            }
        }

        pools[0].Enqueue(t);
    }
}

