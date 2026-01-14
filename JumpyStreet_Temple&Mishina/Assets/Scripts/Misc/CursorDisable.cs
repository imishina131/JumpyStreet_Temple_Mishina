using UnityEngine;

public class CursorDisable : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}