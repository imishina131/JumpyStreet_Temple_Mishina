using UnityEngine;
using System.Collections;

public class VehicleSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] vehicles;
    [SerializeField] float minTime, maxTime, lifeTime;

    private void Start()
    {
        StartCoroutine(SpawnVehicles());
    }
    IEnumerator SpawnVehicles()
    {
        while (true)
        {
            // spawn rate
            float delay = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(delay);

            // spawn vehicle
            int i = Random.Range(0, vehicles.Length);
            GameObject vehicle = Instantiate(vehicles[i], transform.position, Quaternion.identity);

            // destroy vehicle
            Destroy(vehicle, lifeTime);
        }
    }

}
