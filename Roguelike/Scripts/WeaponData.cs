using UnityEngine;

[CreateAssetMenu(menuName = "Create WeaponItem")]
public class WeaponData : ScriptableObject
{
    public float damage => _damage;
    public float recharge => _recharge;

    [SerializeField] private float _damage;
    [SerializeField] private float _recharge;
}
