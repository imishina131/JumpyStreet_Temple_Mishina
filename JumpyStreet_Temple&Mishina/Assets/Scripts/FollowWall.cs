using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class FollowWall : MonoBehaviour
{
    public Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(player.position.x, player.position.y, player.position.z - 4.0f);
    }
    //
    // Update is called once per frame
    void Update()// makes sure the player cant go back
    {
        if(Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
        }
    }
}
