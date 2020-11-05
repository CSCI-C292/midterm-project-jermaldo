using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    //https://www.youtube.com/watch?v=zit45k6CUMk to get the background to move 
    private float _length;
    private float _startPos;

    public GameObject _camera;
    public float _parallax;
    void Start()
    {
        _startPos = transform.position.x;

        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void Update()
    {
        float temp = (_camera.transform.position.x * (1-_parallax));
        float dist = (_camera.transform.position.x * _parallax);
        transform.position = new Vector3(_startPos + dist, transform.position.y, transform.position.z);
        if (temp > _startPos+_length)
            _startPos += _length;
        else if(temp < _startPos - _length)
            _startPos -= _length;
    }
}
