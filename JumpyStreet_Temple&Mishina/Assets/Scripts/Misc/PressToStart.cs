using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PressToStart : MonoBehaviour
{

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            LoadGame();
        }
    }

    void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

}
