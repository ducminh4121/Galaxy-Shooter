using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] Transform rightLimit;
    [SerializeField] Transform leftLimit;
    [SerializeField] Transform upLimit;
    [SerializeField] Transform downLimit;

    private void Awake()
    {
        _camera = Camera.main;
    }
    
    void Start()
    {
       SetUpMapLimit();
    }
 
    void SetUpMapLimit()
    {
        float limitOffset = rightLimit.localScale.x / 2;
        float halfHeight = _camera.orthographicSize + limitOffset;
        float halfWidth = _camera.aspect * halfHeight + limitOffset;

        upLimit.position = Vector3.up * halfHeight;
        downLimit.position = Vector3.down * halfHeight;
        rightLimit.position = Vector3.right * halfWidth;
        leftLimit.position = Vector3.left * halfWidth;
    }
}
