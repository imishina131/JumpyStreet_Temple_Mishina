using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;

    [SerializeField] PauseManager pauseManager;
    [SerializeField] ScoreManager scoreManager;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Hit Obstacle");

            // show gameover panel
            gameOverPanel.gameObject.SetActive(true);

            // pause time
            Time.timeScale = 0f;

            // disable pause manager
            pauseManager.enabled = false;

            // save score
            scoreManager.SaveHighScore();

            // cursor enable
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
