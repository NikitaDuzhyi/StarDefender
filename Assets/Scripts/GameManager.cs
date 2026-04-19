using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverScreen;
    [SerializeField] Button  RestartButton;
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
        RestartButton.onClick.AddListener(Restart);
    }
    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }
    private void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
