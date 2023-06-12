using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class RoomVariants : MonoBehaviour
{
    public static RoomVariants instance;

    public float waitTime => _waitTime;

    public GameObject[] upRooms => _upRooms;
    public GameObject[] downRooms => _downRooms;
    public GameObject[] rightRoom => _rightRooms;
    public GameObject[] leftRoom => _leftRooms;

    [SerializeField] private GameObject[] _upRooms;
    [SerializeField] private GameObject[] _downRooms;
    [SerializeField] private GameObject[] _rightRooms;
    [SerializeField] private GameObject[] _leftRooms;

    [SerializeField] private float _waitTime;

    public List<GameObject> _rooms;

    private void Awake()
    {
        instance = this;
        StartCoroutine(LastSpawnRoom());
    }

    IEnumerator LastSpawnRoom()
    {
        yield return new WaitForSeconds(5f);
        Room lastRoom = _rooms[_rooms.Count - 1].GetComponent<Room>();
        lastRoom.SetLastRoom(true);
    }
}
