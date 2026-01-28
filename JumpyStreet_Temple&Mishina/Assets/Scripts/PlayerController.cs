using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    private float stepLength = 1f;

    [Header("Movement")]
    [SerializeField] float jumpHeight = 0.5f;
    [SerializeField] float jumpTime = 0.25f;

    [Header("Horizontal Bounds")]
    [SerializeField] float minX = -10f;
    [SerializeField] float maxX = 10f;

    [Header("Scripts")]
    [SerializeField] PlayerCollision playerCollision;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] AudioClip cantMoveSFX;

    private bool isMoving;

    public Transform rayPos;
    Ray ray;

    bool onLog;

    void Update()
    {

        // INPUT

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


        // out of bounds check for log
        if (transform.position.x < minX || transform.position.x > maxX)
        {
            playerCollision.Die();
        }
    }


    void TryMove(Vector3 dir)
    {
        Vector3 target = transform.position + dir * stepLength;

        // limit horizontal movement
        if (target.x < minX || target.x > maxX) return;

        // raycast check for tag of friendly piece to prevent move
        // if raycast hits friendly then dont move

        ray = new Ray(rayPos.transform.position, dir);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 1.0f))
        {
            if(hit.collider.gameObject.tag == "Friendly")
            {
                // play audio feedback of cant move
                audioSource.PlayOneShot(cantMoveSFX);

                return;
            }
        }


        Move(dir);
    }

    void Move(Vector3 dir)
    {
        // rotate
        transform.forward = dir;

        // jump
        StartCoroutine(Jump(dir));

        // audio
        audioSource.PlayOneShot(jumpSFX);
    }

    IEnumerator Jump(Vector3 dir)
    {
        isMoving = true;

        // log logic
        bool isJumping = onLog;

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

        // snap to board center after log jump exit
        if (isJumping)
        {
            Vector3 pos = transform.position;

            pos.x = Mathf.Round(pos.x);
            pos.z = Mathf.Round(pos.z);

            transform.position = pos;
        }
    }

    void OnTriggerEnter(Collider other) //parents the player to the moving log so it moves with it
    {
        if(other.GetComponent<Collider>().gameObject.tag == "Log")
        {
            transform.parent = other.transform;

            onLog = true;
        }
    }
    
    void OnTriggerExit(Collider other) //unparents player from log on exit
    {
        if(other.GetComponent<Collider>().gameObject.tag == "Log")
        {
            transform.parent = null;

            onLog = false;
        }
    }
}
