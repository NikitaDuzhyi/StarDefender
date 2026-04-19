using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] Projectile allyProjectile;
    [SerializeField] private float _speed;
    [SerializeField] GameObject AimPoint;
    private Rigidbody2D _rigidbody2D;
    private float _direction;
    private float leftMax, rightMax;
    private bool activeProjectile = false;
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        leftMax = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        rightMax = Camera.main.ViewportToWorldPoint(Vector3.right).x;
    }
    private void Update()
    {
        _direction = Input.GetAxisRaw("Horizontal");
        ClapmScreenHorizontal();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();
        }
    }

    private void FixedUpdate()
    {
        Move(_direction);
    }

    public void Move(float direction)
    {
        _rigidbody2D.linearVelocity = Vector2.right * _speed * direction * Time.deltaTime;
    }

    private void ClapmScreenHorizontal()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, leftMax + 0.5f, rightMax - 0.5f);
        transform.position = position;
    }
    
    private void FireProjectile()
    {
        if (!activeProjectile)
        {
            Projectile projectile = Instantiate(allyProjectile, this.AimPoint.transform.position, Quaternion.identity);
            projectile.Destroyed += ProjectileDestroyed;
            activeProjectile = true;
        }
        
    }
    private void ProjectileDestroyed()
    {
        activeProjectile = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("projectile_hostile") || collision.CompareTag("enemy"))
        {
            Destroy(gameObject);
            GameManager.Instance.GameOver();
        }
    }

}
