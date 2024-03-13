using DG.Tweening;
using UnityEngine;

public class Map : MonoBehaviour
{
    private Camera _camera;

    [Header("Map Limit")]
    [SerializeField] Transform rightLimit;
    [SerializeField] Transform leftLimit;
    [SerializeField] Transform upLimit;
    [SerializeField] Transform downLimit;

    [Space]
    [Header("BackGround")]
    [SerializeField] Transform backGround;
    [SerializeField] Transform firstBG;
    [SerializeField] Transform secondBG;
    [SerializeField] private float loopSpeed = 0.1f;
    [SerializeField] private float limitLoopY = -14.5f;
    private Vector3 _startPosition;

    private void Awake()
    {
        _camera = Camera.main;
    }
    
    void Start()
    {
        float limitOffset = rightLimit.localScale.x;
        float halfHeight = _camera.orthographicSize + limitOffset;
        float halfWidth = _camera.aspect * halfHeight + limitOffset;

        SetUpMapLimit(halfHeight, halfWidth);

        backGround.position = Vector3.down * halfHeight;
        _startPosition = secondBG.position;
    }

    void SetUpMapLimit(float height, float width)
    {
        upLimit.position = Vector3.up * height;
        downLimit.position = Vector3.down * height;
        rightLimit.position = Vector3.right * width;
        leftLimit.position = Vector3.left * width;
    }

    private void Update()
    {
        backGround.Translate(Vector3.down * loopSpeed * Time.fixedDeltaTime);

        LoopBG(firstBG, limitLoopY, _startPosition);
        LoopBG(secondBG, limitLoopY, _startPosition);
    }

    void LoopBG(Transform bgTransform, float limitY, Vector3 restartPos)
    {
        if (bgTransform.position.y < limitY)
            bgTransform.position = restartPos;
    }
}
