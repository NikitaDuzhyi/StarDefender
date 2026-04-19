using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Vector3 direction;

    [SerializeField] private float speed;
    public event System.Action Destroyed;
    private void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Destroyed != null)
        {
            Destroyed.Invoke();
        }
        Destroy(gameObject);
    }
    private void OnBecameInvisible()
    {
        if (Destroyed != null)
        {
            Destroyed.Invoke();
        }
        Destroy(gameObject);
    }
}
