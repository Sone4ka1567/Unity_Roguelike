using System.Collections;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private Direction _direction;
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        None
    }

    private RoomVariants _roomVariants;

    private int _random;

    private bool _spawned = false;

    private float _waitTime;

    private float _destoryTime = 3;

    private void Start()
    {
        _roomVariants = RoomVariants.instance;
        _waitTime = _roomVariants.waitTime;
        Destroy(gameObject, _destoryTime);
        Invoke("Spawn", _waitTime);
    }
    private void Spawn()
    {
        _random = Random.Range(0, 4);
        if (_spawned == false)
        {
            if (_direction == Direction.Up)
            {
                Instantiate(_roomVariants.upRooms[_random], transform.position, _roomVariants.upRooms[_random].transform.rotation);
            }
            else if (_direction == Direction.Down)
            {
                Instantiate(_roomVariants.downRooms[_random], transform.position, _roomVariants.downRooms[_random].transform.rotation);
            }
            else if (_direction == Direction.Right)
            {
                Instantiate(_roomVariants.rightRoom[_random], transform.position, _roomVariants.rightRoom[_random].transform.rotation);
            }
            else if (_direction == Direction.Left)
            {
                Instantiate(_roomVariants.leftRoom[_random], transform.position, _roomVariants.leftRoom[_random].transform.rotation);
            }
            _spawned = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Room>())
        {
            Destroy(gameObject);
        }
    }
}
