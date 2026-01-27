using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject gameOverPanel;

    [Header("Scripts")]
    [SerializeField] PlayerController playerController;
    [SerializeField] PauseManager pauseManager;
    [SerializeField] ScoreManager scoreManager;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip deathSFX;

    [Header("Pause Delay")]
    [SerializeField] float pauseDelay = 1f;

    bool onLog = false;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }

        if (other.gameObject.CompareTag("Log"))
        {
            onLog = true;
        }

        if (other.gameObject.CompareTag("Lava"))
        {
            if (onLog)
            {
                return;
            }
            else if (!onLog)
            {
                Die();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Log"))
        {
            onLog = false;
        }
    }

    public void Die()
    {
        Debug.Log("Hit Obstacle");

        // audio
        audioSource.PlayOneShot(deathSFX);

        // show gameover panel
        gameOverPanel.gameObject.SetActive(true);

        // pause time
        //Time.timeScale = 0f;

        // disable pause manager
        pauseManager.enabled = false;

        // disable player script;
        playerController.enabled = false;

        // save score
        scoreManager.SaveHighScore();

        // cursor enable
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // wait to pause time for death animation?
        // invoke nameof method time
        Invoke(nameof(PauseTime), pauseDelay);
    }

    void PauseTime()
    {
        Time.timeScale = 0f;
    }
}
