using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour, IEnemyControllble
{
    [SerializeField] private float _hp;

    [SerializeField] private float _damage;

    [SerializeField] private float _speed;

    [SerializeField] private GameObject[] _items;

    [SerializeField] private GameObject _hit;

    [Header("Radiuses")]
    [SerializeField] private float _lookRadius;
    [SerializeField] private float _attckRadius;
    [SerializeField] private float _stoppingRadius;

    [SerializeField] private Transform _attackPoint;

    [SerializeField] private float _startRecharge;

    [SerializeField] private bool _onBoss;

    private float _recharge;

    private Player _player;

    private Transform _playerTransform;

    private Animator _animator;

    private void Start()
    {
        _player = Player.instance;
        _playerTransform = _player.transform;

        _recharge = _startRecharge;

        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _playerTransform.position) < _lookRadius)
        {
            if (Vector2.Distance(transform.position, _playerTransform.position) > _stoppingRadius)
            {
                transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, _speed * Time.deltaTime);
            }

            if (_playerTransform.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        if (_recharge <= 0 && Vector2.Distance(transform.position, _playerTransform.position) < _attckRadius)
        {
            Attack();
        }
        else
        {
            _recharge -= Time.deltaTime;
        }
    }

    private void Attack()
    {
        _player.GetDamage(_damage);
        _recharge = _startRecharge;
    }

    public void GetDamage(float damage)
    {
        _hp -= damage;
        _animator.SetBool("OnHit", true);
        if (_hp <= 0)
        {
            Instantiate(_hit, transform.position, Quaternion.identity);
            _player.PlusCountLevel(15);
            if (_onBoss == true)
            {
                _player.PlusCountLevel(85);
            }
            else
            {
                int random = Random.Range(1, 11);
                if (random < 3)
                {
                    Instantiate(_items[0], transform.position, Quaternion.identity);
                }
                else if (random > 2 && random < 6)
                {
                    Instantiate(_items[1], transform.position, Quaternion.identity);
                }
            }
            _animator.SetBool("OnDead", true);
        }
    }

    private void EndDeadAnimation()
    {
        if (_onBoss == true)
        {
            SceneManager.LoadScene("Menu");
        }
        Destroy(gameObject);
    }

    private void EndHitAnimation()
    {
        _animator.SetBool("OnHit", false);
        _animator.SetBool("OnIdle", true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _lookRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attckRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _stoppingRadius);
    }
}
