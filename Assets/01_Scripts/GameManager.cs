using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int scorePlayer1 = 0;
    public int scorePlayer2 = 0;
    public int highScore = 0;

    public TextMeshProUGUI scoreText1;
    public TextMeshProUGUI scoreText2;
    public TextMeshProUGUI highScoreText; // 👈 Texto para el High Score

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        // Obtener HighScore guardado
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreUI();
    }

    public void AddScore(int player)
    {
        if (player == 1)
        {
            scorePlayer1++;
            scoreText1.text = scorePlayer1.ToString();
        }
        else if (player == 2)
        {
            scorePlayer2++;
            scoreText2.text = scorePlayer2.ToString();
        }

        // Calcular score total actual (ejemplo: suma de ambos jugadores)
        int currentScore = Mathf.Max(scorePlayer1, scorePlayer2);

        // Si supera el HighScore guardado, actualízalo
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            UpdateHighScoreUI();
        }
    }

    void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }
}
