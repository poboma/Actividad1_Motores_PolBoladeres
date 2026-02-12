using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Puntuación")]
    public int score = 0;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
        void Start()
    {
        UpdateScoreText();
    }

    public void AddPoints(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public void RemovePoints(int amount)
    {
        score -= amount;
        if (score < 0) score = 0;
        UpdateScoreText();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Puntuacion: " + score;
    }
}
