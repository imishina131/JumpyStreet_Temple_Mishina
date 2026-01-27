using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    [SerializeField] GameObject pausePanel;

    [SerializeField] PlayerController playerScript;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip pauseSFX;
    [SerializeField] AudioClip unpauseSFX;

    private void Awake()
    {
        // disable pause panel
        pausePanel.gameObject.SetActive(false);
    }


    void Update()
    {
        // pause
        if (Keyboard.current.escapeKey.wasPressedThisFrame && !pausePanel.activeSelf)
        {
            if (pausePanel == null) return;

            // enable pause panel
            pausePanel.gameObject.SetActive(true);

            // disable player scripts
            playerScript.enabled = false;

            // cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // pause time
            Time.timeScale = 0f;

            // pause audio
            audioSource.PlayOneShot(pauseSFX);
        }

        else if (Keyboard.current.escapeKey.wasPressedThisFrame && pausePanel.activeSelf)
        {
            if (pausePanel == null) return;

            // diable pause panel
            pausePanel.gameObject.SetActive(false);

            // enable player script
            playerScript.enabled = true;

            // cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // resume time
            Time.timeScale = 1f;

            // unpause audio
            audioSource.PlayOneShot(unpauseSFX);
        }
    }

    public void Resume()
    {
        // diable pause panel
        pausePanel.gameObject.SetActive(false);

        // enable player script
        playerScript.enabled = true;

        // cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // resume time
        Time.timeScale = 1f;

        // unpause audio
        audioSource.PlayOneShot(unpauseSFX);
    }

}
