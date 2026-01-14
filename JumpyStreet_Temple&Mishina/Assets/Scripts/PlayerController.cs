using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    private float stepLength = 1f;

    [SerializeField] float jumpHeight = 0.5f;
    [SerializeField] float jumpTime = 0.25f;

    private bool isMoving;

    void Update()
    {
        if (isMoving || Keyboard.current == null) return;

        // W
        if (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame)
            Move(Vector3.forward);

        // S
        if (Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.downArrowKey.wasPressedThisFrame)
            Move(Vector3.back);

        // A
        if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame)
            Move(Vector3.left);

        // D
        if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame)
            Move(Vector3.right);
    }

    void Move(Vector3 dir)
    {
        // rotate
        transform.forward = dir;

        // jump
        StartCoroutine(Jump(dir));
    }

    IEnumerator Jump(Vector3 dir)
    {
        isMoving = true;

        // start position
        Vector3 start = transform.position;

        // end position stepLength away
        Vector3 end = start + dir * stepLength;

        // time
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / jumpTime;

            // arc
            float y = Mathf.Sin(t * Mathf.PI) * jumpHeight;

            // move from start to end and jump
            transform.position = Vector3.Lerp(start, end, t) + Vector3.up * y;

            yield return null;
        }

        // move to end position
        transform.position = end;
        isMoving = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Hit Obstacle");

            // show gameover panel


            // pause time
            Time.timeScale = 0f;

            // disable pause manager


            // save score

        }
    }
}
