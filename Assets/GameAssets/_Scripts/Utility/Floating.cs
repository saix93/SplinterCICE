using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    [SerializeField]
    float _frecuency = 1.5f;
    [SerializeField]
    float _amplitude = .1f;

    float _originalYAxis;

    private void Start()
    {
        _originalYAxis = this.transform.position.y;
    }

    private void Update()
    {
        float value = Mathf.Sin(Time.time * _frecuency) * _amplitude;
        float newYAxis = _originalYAxis + value;
        this.transform.position = new Vector3(this.transform.position.x, newYAxis, this.transform.position.z);
    }
}
