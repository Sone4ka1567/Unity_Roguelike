using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [SerializeField] private int[] _slots;

    [Header("Slots: ")]
    [SerializeField] private GameObject[] _slotOne;
    [SerializeField] private GameObject[] _slotTwo;
    [SerializeField] private GameObject[] _slotThree;

    [SerializeField] private GameObject[] _weaponImage;

    private Player _player;

    private int _freeSlotNumber = -1;

    private int _usingSlotNumber;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            SetUsingSlot(0);
            UseItem(_slots[0]);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            SetUsingSlot(1);
            UseItem(_slots[1]);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            SetUsingSlot(2);
            UseItem(_slots[2]);
        }
    }

    public void SaveItem(int numberItem)
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i] == 0)
            {
                _freeSlotNumber = i;
                break;
            }
        }

        if (_freeSlotNumber != -1)
        {
            _slots[_freeSlotNumber] = numberItem;

            if (_freeSlotNumber == 0)
            {
                _slotOne[numberItem].SetActive(true);
            }
            else if (_freeSlotNumber == 1)
            {
                _slotTwo[numberItem].SetActive(true);
            }
            else
            {
                _slotThree[numberItem].SetActive(true);
            }

            _freeSlotNumber = -1;
        }
    }

    public void SetUsingSlot(int slot)
    {
        _usingSlotNumber = slot;
    }

    public void UseItem(int numberItem)
    {
        int slotNumber = _usingSlotNumber;

        switch (slotNumber)
        {
            case (0):
                _slotOne[numberItem].SetActive(false);
                break;
            case (1):
                _slotTwo[numberItem].SetActive(false);
                break;
            case (2):
                _slotThree[numberItem].SetActive(false);
                break;
        }

        _slots[slotNumber] = 0;

        _player.UsingItem(numberItem);
    }

    public void ActiveWeaponImage(int numberAcive)
    {
        for (int i = 0; i < _weaponImage.Length; i++)
        {
            _weaponImage[i].SetActive(false);
        }
        _weaponImage[numberAcive].SetActive(true);
    }
}
