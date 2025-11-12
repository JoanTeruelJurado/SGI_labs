using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void FlashScore()
    {
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        Color original = scoreText.color;
        scoreText.color = Color.yellow; // color de resaltado
        yield return new WaitForSeconds(0.2f);
        scoreText.color = original;
    }
}
