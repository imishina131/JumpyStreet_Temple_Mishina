using UnityEngine;
using System.Collections;

public class VehicleSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] vehicles;
    [SerializeField] float minTime, maxTime, lifeTime, direction;
    public GameObject targetObject;
    Vector3 targetPosition;
    GameObject prefabToSpawn;

    private void Start()
    {
        targetPosition = targetObject.transform.position;
        StartCoroutine(SpawnVehicles());
    }
    IEnumerator SpawnVehicles()
    {
        while (true)
        {
            // spawn rate
            float delay = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(delay);

            // spawn prefab
            prefabToSpawn = Resources.Load<GameObject>("Black Queen 1");
            Instantiate(prefabToSpawn, new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.Euler(0f, direction, 0f));
            prefabToSpawn.transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10.0f * Time.deltaTime);
            // destroy vehicle
            //Destroy(prefabToSpawn, lifeTime);
        }
    }

    void Update()
    {
        prefabToSpawn.transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10.0f * Time.deltaTime);
    }

}
