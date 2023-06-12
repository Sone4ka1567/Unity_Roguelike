using UnityEngine;

public class Weapon : MonoBehaviour, IWeaponControllble
{
    [Header("Recharges: ")]
    [SerializeField] private float _startRecharge;
    [SerializeField] private float _speedRecharge;

    [SerializeField] private float _startTimeSpeedShooting;

    [SerializeField] private float _offset;

    [SerializeField] private GameObject _shoot;

    [Header("Points: ")]
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Transform _throwOutPoint;
    [SerializeField] private GameObject _bullet;

    [SerializeField] private bool _enemyWeapon;
    [SerializeField] private Transform _target;

    [Header("Parametrs: ")]
    [SerializeField] private int _weaponNumber;
    [SerializeField] private float _damage;

    [Header("SpriteMaterials: ")]
    [SerializeField] private Sprite[] _weaponSprites;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("WeaponItems: ")]
    [SerializeField] private WeaponData[] _weaponDates;

    [SerializeField] private GameObject _weapoItemObject;

    private float _recharge;

    private WeaponData _thisData;

    private bool _onSpeedShooting = false;

    private float _timeSpeedShooting;

    private Inventory _inventory;

    private void Start()
    {
        _inventory = Inventory.instance;
        _recharge = _startRecharge;
        _timeSpeedShooting = _startTimeSpeedShooting;
        if (_enemyWeapon == false)
        {
            _weaponNumber = PlayerPrefs.GetInt("lastWeaponNumber");
            _inventory.ActiveWeaponImage(_weaponNumber);
            _spriteRenderer.sprite = _weaponSprites[_weaponNumber];
            _thisData = _weaponDates[_weaponNumber];

            _recharge = _thisData.recharge;
            _damage = _thisData.damage;
        }
    }

    private void Update()
    {
        _recharge -= Time.deltaTime;

        if (_enemyWeapon == false)
        {
            if (_onSpeedShooting == true)
            {
                _timeSpeedShooting -= Time.deltaTime;
                if (_timeSpeedShooting <= 0)
                {
                    _onSpeedShooting = false;
                    _recharge = _startRecharge;
                    _timeSpeedShooting = _startTimeSpeedShooting;
                }
            }
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotateWeapon = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotateWeapon + _offset);
        }
    }

    public void Attack()
    {
        if (_recharge <= 0)
        {
            Bullet bullet;
            GameObject bulletObject = Instantiate(_bullet, _attackPoint.position, transform.rotation);

            if(_enemyWeapon ==false)
            {
                Instantiate(_shoot, transform.position, Quaternion.identity);
            }

            bullet = bulletObject.GetComponent<Bullet>();
            bullet.SetDamage(_damage);

            if (_onSpeedShooting == false)
            {
                _recharge = _startRecharge;
            }
            else
            {
                _recharge = _speedRecharge;
            }
        }
    }

    public void UpWeapon(int weaponNumber)
    {
        GameObject weaponItemObject = Instantiate(_weapoItemObject, _throwOutPoint.position, Quaternion.identity);
        SpriteRenderer spriteRenderer = weaponItemObject.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = _weaponSprites[_weaponNumber];
        Item item = weaponItemObject.GetComponent<Item>();
        item.SetNumberItem(_weaponNumber, this);

        _weaponNumber = weaponNumber;
        _spriteRenderer.sprite = _weaponSprites[weaponNumber];
        _thisData = _weaponDates[weaponNumber];

        _recharge = _thisData.recharge;
        _damage = _thisData.damage;

        _inventory.ActiveWeaponImage(_weaponNumber);

        PlayerPrefs.SetInt("lastWeaponNumber", _weaponNumber);
    }

    public bool SetSpeedShoot(bool value)
    {
        return _onSpeedShooting = value;
    }

    public float SetTimeSpeedShooting(float plusValue)
    {
        return _timeSpeedShooting += plusValue;
    }

    public float SetDamage(float plusValue)
    {
        return _damage += plusValue;
    }

    public float SetRecharge(float minusValue)
    {
        return _recharge -= minusValue;
    }
}
