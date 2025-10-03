using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "GameScene";
    [SerializeField] private TextMeshProUGUI highScoreText; 

    private void Start()
    {
        if (highScoreText != null)
        {
            int hi = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = $"High Score: {hi}";
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
