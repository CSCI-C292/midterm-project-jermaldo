using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform _player;
    void Start()
    {
        
    }

    
    void LateUpdate()
    {
        Vector3 temp = transform.position;

        temp.x = _player.position.x;
        
        transform.position = temp;
    }
}
