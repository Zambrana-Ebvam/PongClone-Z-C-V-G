using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Puntaje")]
    [SerializeField] private int targetScore = 10;   // Límite para ganar
    public int scorePlayer1 = 0;
    public int scorePlayer2 = 0;

    [Header("High Score")]
    public int highScore = 0;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [Header("HUD - Marcadores")]
    [SerializeField] private TextMeshProUGUI scoreText1;
    [SerializeField] private TextMeshProUGUI scoreText2;

    [Header("Escenas")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private void Awake()
    {
        if (instance == null) instance = this;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreUI();
        RefreshScoresUI();
    }

    public void AddScore(int player)
    {
        if (player == 1)
        {
            scorePlayer1++;
        }
        else if (player == 2)
        {
            scorePlayer2++;
        }

        RefreshScoresUI();
        UpdateHighScoreIfNeeded();

        // 👇 Lógica rápida: cuando alguien llega al target, ir al menú
        if (scorePlayer1 >= targetScore || scorePlayer2 >= targetScore)
        {
            GoToMainMenu();
        }
    }

    private void RefreshScoresUI()
    {
        if (scoreText1 != null) scoreText1.text = scorePlayer1.ToString();
        if (scoreText2 != null) scoreText2.text = scorePlayer2.ToString();
    }

    private void UpdateHighScoreIfNeeded()
    {
        int currentBest = Mathf.Max(scorePlayer1, scorePlayer2);
        if (currentBest > highScore)
        {
            highScore = currentBest;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            UpdateHighScoreUI();
        }
    }

    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
            highScoreText.text = $"High Score: {highScore}";
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
