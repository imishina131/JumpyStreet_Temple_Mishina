using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    private float stepLength = 1f;

    [SerializeField] float jumpHeight = 0.5f;
    [SerializeField] float jumpTime = 0.25f;

    [SerializeField] float minX = -10f;
    [SerializeField] float maxX = 10f;

    private bool isMoving;

    void Update()
    {
        if (isMoving || Keyboard.current == null) return;

        // W
        if (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame)
            TryMove(Vector3.forward);

        // S
        if (Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.downArrowKey.wasPressedThisFrame)
            TryMove(Vector3.back);

        // A
        if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame)
            TryMove(Vector3.left);

        // D
        if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame)
            TryMove(Vector3.right);
    }


    void TryMove(Vector3 dir)
    {
        Vector3 target = transform.position + dir * stepLength;

        // limit horizontal movement
        if (target.x < minX || target.x > maxX) return;

        Move(dir);
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
}
