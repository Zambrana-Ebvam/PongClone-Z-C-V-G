using UnityEngine;
using TMPro; // usa TextMeshPro, si no lo usas cambia por UnityEngine.UI

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int scorePlayer1 = 0;
    public int scorePlayer2 = 0;

    // Aquí van las referencias a los textos
    public TextMeshProUGUI scoreText1;
    public TextMeshProUGUI scoreText2;

    private void Awake()
    {
        if (instance == null)
            instance = this;
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
    }
}
