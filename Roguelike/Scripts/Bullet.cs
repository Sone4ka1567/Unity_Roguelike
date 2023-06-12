using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _onEnemyBullet;

    private float _damage;

    private void FixedUpdate()
    {
        if (_onEnemyBullet == false)
        {
            transform.Translate(Vector2.up * _speed);
        }
        else
        {
            transform.Translate(Vector2.right * _speed);
        }
    }

    public float SetDamage(float value)
    {
        return _damage = value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_onEnemyBullet == false)
        {
            if (collision.gameObject.GetComponent<EnemyShooter>())
            {
                EnemyShooter enemyShooter = collision.gameObject.GetComponent<EnemyShooter>();
                enemyShooter.GetDamage(_damage);
            }
            if (collision.gameObject.GetComponent<Enemy>())
            {
                Enemy enemyShooter = collision.gameObject.GetComponent<Enemy>();
                enemyShooter.GetDamage(_damage);
            }
        }
        else if (collision.gameObject.GetComponent<Player>())
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.GetDamage(_damage);
        }
        Destroy(gameObject);
    }
}
