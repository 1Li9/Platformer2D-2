using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxHeight;

    private void Update()
    {
        Vector3 position2d = Vector3.Scale(Vector3.Lerp(transform.position, _target.position, _speed * Time.deltaTime), new Vector3(1f,1f, 0f));
        transform.position = position2d + Vector3.Scale(transform.position, new Vector3(0f, 0f, 1f));
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -_maxHeight, _maxHeight), transform.position.z);
    }
}
