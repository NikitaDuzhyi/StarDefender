using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public event System.Action Killed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("projectile_ally"))
        {
            Destroy(gameObject);
            ScoreChanger.Instance.AddScore(10);
            Killed.Invoke();
        }
        else
        {
            GameManager.Instance.GameOver();
        }
    }
}
