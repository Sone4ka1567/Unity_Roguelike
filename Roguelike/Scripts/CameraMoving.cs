using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smooth;
    [SerializeField] private Vector3 _offset;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, Time.deltaTime * _smooth);
    }
}
