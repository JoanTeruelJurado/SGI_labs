using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;  // Patrón singleton para acceso global

    public TextMeshProUGUI scoreText;
    public ScoreEffect scoreEffect; // Referencia al efecto
    private int score = 0;

    void Awake()
    {
        // Asegura que solo exista un ScoreManager
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
        //ScoreManager.Instance.AddScore(100); -> per sumar la puntuació quan s'elimini la linea de tetris

        // Llamar al efecto visual
        if (scoreEffect != null)
            scoreEffect.FlashScore();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public int GetScore()
    {
        return score;
    }
}
