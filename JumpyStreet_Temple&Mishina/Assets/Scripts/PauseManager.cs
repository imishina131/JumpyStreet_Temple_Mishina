using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    [SerializeField] GameObject pausePanel;

    [SerializeField] PlayerController playerScript;

    private void Awake()
    {
        // disable pause panel
        pausePanel.gameObject.SetActive(false);
    }


    void Update()
    {
        // pause
        if (Input.GetKeyDown(KeyCode.Escape) && !pausePanel.activeSelf)
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

            // audio
            AudioListener.pause = true;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && pausePanel.activeSelf)
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

            // audio
            AudioListener.pause = false;
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

        // audio
        AudioListener.pause = false;
    }

}
