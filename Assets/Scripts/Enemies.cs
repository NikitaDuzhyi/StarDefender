using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemies : MonoBehaviour
{
    [SerializeField] private Enemy prefab;
    [SerializeField] private int rows = 5;
    [SerializeField] private int cols = 8;
    [SerializeField] private AnimationCurve speed;
    [SerializeField] private int attackRate;
    [SerializeField] Projectile projectile;
    private Vector3 _direction = Vector2.right;
    private Vector3 startPosition;
    private int totalAmount => this.rows * this.cols;
    private int killedAmount;
    private int aliveAmount => totalAmount - killedAmount;
    private float killedPercent => (float)killedAmount / (float)totalAmount;
    private void Awake()
    {
        startPosition = transform.position;
        Swapn();
    }

    private void Start()
    {
        InvokeRepeating("Attack", attackRate, attackRate);
    }
    private void Update()
    {
        Movement();
    }
    private void Movement()
    {
        this.transform.position += _direction * speed.Evaluate(killedPercent) * Time.deltaTime;
        Vector3 leftMax = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightMax = Camera.main.ViewportToWorldPoint(Vector3.right);
        foreach (Transform enemy in this.transform)
        {
            if (_direction == Vector3.right && enemy.position.x >= (rightMax.x - 0.4f))
            {
                ClampRow();
            }
            else if (_direction == Vector3.left && enemy.position.x <= (leftMax.x + 0.4f))
            {
                ClampRow();
            }
        }
    }
    private void ClampRow()
    {
        _direction.x *= -1f;
        Vector3 pos = this.transform.position;
        pos.y -= 0.2f;
        this.transform.position = pos;
    }

    private void Attack()
    {
        foreach (Transform enemy in this.transform)
        {
            if (Random.value < (1f / (float)aliveAmount)){
                Instantiate(projectile, enemy.position, Quaternion.identity);
            }
        }
    }

    private void Swapn()
    {
        for (int i = 0; i < rows; i++)
        {
            float width = cols - 1;
            float height = rows - 1;
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPos = new Vector3(centering.x, centering.y + i, 0f);
            for (int j = 0; j < cols; j++)
            {
                Enemy enemy = Instantiate(prefab, this.transform);
                enemy.Killed += EnemyKilled;
                Vector3 pos = rowPos;
                pos.x += j;
                enemy.transform.localPosition = pos;
            }
        }
    }
    private void EnemyKilled()
    {
        killedAmount++;
        if (killedAmount == totalAmount)
        {
            killedAmount = 0;
            Respawn();
        }
    }
    private void Respawn()
    {
        _direction = Vector3.right;
        transform.position = startPosition;
        killedAmount = 0;
        Swapn();
    }
}
