using UnityEngine;
using System.Collections;

public class VehicleSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] vehicles;
    [SerializeField] float minTime, maxTime, lifeTime, direction;

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
            GameObject vehicle = Instantiate(vehicles[i], transform.position, Quaternion.Euler(0f, direction, 0f));
            vehicle.transform.Translate(Vector3.forward * 2.0f * Time.deltaTime);
            //vehicle.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            // destroy vehicle
            Destroy(vehicle, lifeTime);
        }
    }

}
