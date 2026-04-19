using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public event System.Action Killed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "projectile_ally(Clone)")
        {
            Destroy(gameObject);
            ScoreChanger.Instance.AddScore(10);
            Killed.Invoke();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
