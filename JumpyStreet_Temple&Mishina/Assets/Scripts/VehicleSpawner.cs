using UnityEngine;
using System.Collections;

public class VehicleSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] vehicles;
    [SerializeField] float minTime, maxTime;

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
            Instantiate(vehicles[i], transform.position, Quaternion.identity);
        }
    }

}
