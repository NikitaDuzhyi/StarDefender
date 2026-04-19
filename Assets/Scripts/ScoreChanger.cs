using TMPro;
using UnityEngine;

public class ScoreChanger : MonoBehaviour
{
    public static ScoreChanger Instance;
    [SerializeField] private TextMeshProUGUI ScoreText;
    private int score = 0;
    public int Score => score;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddScore(int value)
    {
        score += value;
        ScoreText.text = $"Score: {score}";
    }
}
