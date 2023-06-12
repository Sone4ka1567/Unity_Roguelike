using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Weapon _playerWeapon;

    private Player _player;

    private IPlayerControlble _playerControler;
    private IWeaponControllble _weaponControler;

    private void Start()
    {
        _player = Player.instance;
        _playerControler = _player;
        _weaponControler = _playerWeapon;
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            _playerControler.Run(horizontal, vertical);
        }
        else
        {
            _playerControler.Idle();
        }

        if (Input.GetMouseButton(0))
        {
            if(_weaponControler !=null)
            {
                _weaponControler.Attack();
            }
        }
    }
}
