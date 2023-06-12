using UnityEngine;

public class EnemyShooter : MonoBehaviour, IEnemyControllble
{
    [Header("Radius: ")]
    [SerializeField] private float _attackRadius;

    [Header("Parametrs: ")]
    [SerializeField] private float _startRecharge;

    [SerializeField] private Weapon _weapon;

    [SerializeField] private float _hp;

    [SerializeField] private GameObject _hit;

    private Transform _weaponTransform;

    private Animator _animator;

    private Player _player;

    private bool _onLife = true;

    private void Start()
    {
        _weaponTransform = _weapon.transform;
        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < _attackRadius && _onLife == true)
        {
            _weapon.Attack();
        }

        _weaponTransform.right = _player.transform.position - transform.position;
    }

    public void GetDamage(float damage)
    {
        _animator.SetBool("OnHit", true);
        _hp -= damage;
        Instantiate(_hit, transform.position, Quaternion.identity);
        if (_hp <= 0)
        {
            _player.PlusCountLevel(25);
            _animator.SetBool("OnDead", true);
            _onLife = false;
        }
    }

    private void EndDeadAnimation()
    {
        Destroy(gameObject);
    }

    private void EndHitAnimation()
    {
        _animator.SetBool("OnHit", false);
        _animator.SetBool("OnIdle", true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
}
