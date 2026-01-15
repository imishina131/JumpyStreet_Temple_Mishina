using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float cameraHeight = 10f;
    public float cameraDistance = -3f;


    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, cameraHeight, player.position.z + cameraDistance);
    }
}
