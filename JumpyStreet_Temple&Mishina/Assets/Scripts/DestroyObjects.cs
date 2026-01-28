using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    public Transform cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(cam.position.x, cam.position.y, cam.position.z - 5.0f);
    }

    void OnTriggerEnter(Collider other)//deletes past tiles
    {
        if(other.GetComponent<Collider>().gameObject.tag == "Ground" && GetComponent<Collider>().gameObject.tag == "DestroyWall")
        {
            Debug.Log("entered");
            Destroy(other.gameObject);
        }
    }
}
