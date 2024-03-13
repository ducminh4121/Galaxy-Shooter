using Lean.Pool;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData playerData;

    [Space]
    [Header("Move")]
    [SerializeField] Rigidbody2D rb;
    private Transform _myTransform;
    private float offsetX, offsetY;
    private Camera _camera;
    private Vector2 _mousePos = Vector2.zero;

    [Space]
    [Header("Shoot")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform turret;
    [SerializeField] private Transform muzzlePos;
    [SerializeField] private GameObject muzzlePreafb;

    private void Awake()
    {
        _myTransform = transform;
        _camera = Camera.main;
    }

    private void Start()
    {
        StartCoroutine(DelayShootWhenStart());
    }

    void Update()
    {
#if UNITY_EDITOR

        MoveTouchInput();

#endif

        MoveMouse();
    }

    IEnumerator DelayShootWhenStart()
    {
        yield return new WaitForSeconds(0.75f);
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        LeanPool.Spawn(muzzlePreafb, muzzlePos.position, muzzlePos.rotation);
        LeanPool.Spawn(bulletPrefab, turret.position, turret.rotation);

        yield return new WaitForSeconds(playerData.reloadTime);
        StartCoroutine(Shoot());
    }

    private void MoveMouse()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

            offsetX = _mousePos.x - _myTransform.position.x;
            offsetY = _mousePos.y - _myTransform.position.y;
        }
        if (Input.GetMouseButton(0))
        {
            _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            rb.MovePosition(new Vector2(_mousePos.x - offsetX, _mousePos.y - offsetY));
        }
        if (Input.GetMouseButtonUp(0))
        {
            rb.velocity = Vector2.zero;
        }
    }


    private void MoveTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 touchPos = _camera.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    offsetX = touchPos.x - _myTransform.position.x;
                    offsetY = touchPos.y - _myTransform.position.y;
                    break;

                case TouchPhase.Moved:
                    rb.MovePosition(new Vector2(touchPos.x - offsetX, touchPos.y - offsetY));
                    break;

                case TouchPhase.Ended:
                    rb.velocity = Vector2.zero;
                    break;
            }
        }
    }
}
