using UnityEngine;

public class VehicleMover : MonoBehaviour
{

    [SerializeField] float speed = 1f;

    private void Update()
    {
        // move forward
        transform.position += transform.forward * speed * Time.deltaTime;
    }

}
