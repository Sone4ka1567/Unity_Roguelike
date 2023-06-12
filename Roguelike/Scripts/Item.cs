using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float _upRadius;

    [SerializeField] private int _numberItem;

    [SerializeField] private bool _onWeaponItem;

    private Weapon _playerWeapon;

    private Transform _playerTransform;

    private Inventory _inventory;


    private void Start()
    {
        _playerTransform = Player.instance.transform;

        _playerWeapon = Player.instance.GetComponentInChildren<Weapon>();

        _inventory = Inventory.instance;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _playerTransform.position) < _upRadius)
        {
            if (_onWeaponItem == false)
            {
                _inventory.SaveItem(_numberItem);
                Destroy(gameObject);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    _playerWeapon.UpWeapon(_numberItem);
                    Destroy(gameObject);
                }
            }
        }
    }

    public void SetNumberItem(int numberLastItem, Weapon playerWeapon)
    {
        _numberItem = numberLastItem;
        _playerWeapon = playerWeapon;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _upRadius);
    }
}
