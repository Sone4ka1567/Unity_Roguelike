using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private int _lastWeaponNumber;

    [SerializeField] private GameObject[] _weaponsImages;

    [SerializeField] private Text[] _parametrs;

    private int _level;

    private float _hp;

    private void Start()
    {
        _level = PlayerPrefs.GetInt("level");
        if (_level == 0)
        {
            _level = 1;
        }
        _parametrs[0].text = "Level: " + _level;
        _hp = PlayerPrefs.GetFloat("hp");
        if (_hp <= 0)
        {
            _hp = 10;
        }
        PlayerPrefs.SetFloat("hp", _hp);
        if (_hp < 10)
        {
            _parametrs[1].text = "HP: 10";
        }
        if (_hp <= 10 && _hp > 9)
        {
            _parametrs[1].text = "HP: 9";
        }
        if (_hp <= 9 && _hp > 8)
        {
            _parametrs[1].text = "HP: 8";
        }
        if (_hp <= 8 && _hp > 7)
        {
            _parametrs[1].text = "HP: 7";
        }
        if (_hp <= 7 && _hp > 6)
        {
            _parametrs[1].text = "HP: 6";
        }
        if (_hp <= 6 && _hp > 5)
        {
            _parametrs[1].text = "HP: 5";
        }
        if (_hp <= 5 && _hp > 4)
        {
            _parametrs[1].text = "HP: 4";
        }
        if (_hp <= 4 && _hp > 3)
        {
            _parametrs[1].text = "HP: 3";
        }
        if (_hp <= 3 && _hp > 2)
        {
            _parametrs[1].text = "HP: 2";
        }
        if (_hp <= 2 && _hp > 1)
        {
            _parametrs[1].text = "HP: 1";
        }
        if (_hp <= 1 && _hp > 0)
        {
            _parametrs[1].text = "HP: 0";
        }
        _lastWeaponNumber = PlayerPrefs.GetInt("lastWeaponNumber");

        _weaponsImages[_lastWeaponNumber].SetActive(true);
        if (_lastWeaponNumber == 0 || _lastWeaponNumber == 1)
        {
            _parametrs[2].text = "Damage: 1";
        }
        else if (_lastWeaponNumber == 2)
        {
            _parametrs[2].text = "Damage: 3";
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Location");
    }
}
