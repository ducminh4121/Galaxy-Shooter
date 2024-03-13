using Lean.Pool;
using System.Collections;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] Rigidbody2D rb;
    private Transform _myTransform;
    private float offsetX, offsetY;
    private Camera _camera;

    [Space]
    [Header("Shoot")]
    [SerializeField] private float bulletReload = 0.1f;
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
        StartCoroutine(Shoot());
    }

    void Update()
    {
        MoveTouchInput();
    }

    IEnumerator Shoot()
    {
        LeanPool.Spawn(muzzlePreafb, muzzlePos.position, muzzlePos.rotation);
        LeanPool.Spawn(bulletPrefab, turret.position, turret.rotation);

        yield return new WaitForSeconds(bulletReload);
        StartCoroutine(Shoot());
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
