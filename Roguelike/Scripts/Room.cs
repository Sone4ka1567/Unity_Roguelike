using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private Transform[] _enemyPoints;
    [SerializeField] private Transform _itemsPoints;

    [Header("Objects")]
    [SerializeField] private GameObject[] _enemyesObjects;
    [SerializeField] private GameObject[] _itemsObjects;

    [Header("Randoms")]
    [SerializeField] private int[] _randomEnemyes;
    [SerializeField] private int _randonItem;

    [SerializeField] private bool _onAllRoom;

    [Header("LastRoom: ")]
    [SerializeField] private bool _lastRoom = false;
    [SerializeField] private Transform _bossSpawnPoint;
    [SerializeField] private GameObject _bossObject;
    [SerializeField] private GameObject[] _weapon;
    [SerializeField] private GameObject[] _wallsClose;

     private bool _onSpawned = false;

    private RoomVariants _roomVariants;

    private void Start()
    {
        _roomVariants = RoomVariants.instance;
        _roomVariants._rooms.Add(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() && _onSpawned == false && _onAllRoom == false)
        {
            Spawning();
            _onSpawned = true;
            if(_lastRoom == true)
            {
                if (_wallsClose.Length > 1)
                {
                    for (int i = 0; i < _wallsClose.Length; i++)
                    {
                        _wallsClose[i].SetActive(true);
                    }
                }
                else
                {
                    _wallsClose[0].SetActive(true);
                }
            }
        }
    }

    private void Spawning()
    {
        if (_lastRoom == false)
        {
            for (int i = 0; i < _enemyesObjects.Length; i++)
            {
                _randomEnemyes[i] = Random.Range(0, _enemyesObjects.Length);
                Instantiate(_enemyesObjects[_randomEnemyes[i]], _enemyPoints[i].position, Quaternion.identity);
            }
            _randonItem = Random.Range(0, 2);
            Instantiate(_itemsObjects[_randonItem], _itemsPoints.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_bossObject, _bossSpawnPoint.position, Quaternion.identity);
            int randomNumberWeapon = Random.Range(0, 2);
            Instantiate(_weapon[randomNumberWeapon], _itemsPoints.position, Quaternion.identity);
        }
    }

    public void SetLastRoom(bool value)
    {
        _lastRoom = value;
    }

}
