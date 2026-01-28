using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] Transform player;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    private float startPos;
    private float distance;
    public int score;

    private void Start()
    {
        startPos = player.position.z;

        LoadHighScore();
    }

    private void Update()
    {
        distance = player.position.z - startPos;

        if (distance < 0)
        {
            distance = 0;
        }

        score = Mathf.FloorToInt(distance);
        scoreText.text = "SCORE - " + score;
    }

    public void SaveHighScore()
    {
        int highscore = PlayerPrefs.GetInt("HighScore", 0);

        if (score > highscore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
            highscore = score;
        }

        highScoreText.text = "HIGH SCORE - " + highscore;
    }

    void LoadHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "HIGH SCORE - " + highScore; 
    }

}
