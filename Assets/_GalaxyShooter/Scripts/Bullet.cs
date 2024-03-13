using Lean.Pool;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    public BulletData data;
    [SerializeField] GameObject hitEffectPrefab;
    [SerializeField] Transform hitPoint;

    public void OnInit()
    {
        rb2d.velocity = transform.up * data.speed;
    }

    private void OnEnable()
    {
        OnInit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Limit"))
            LeanPool.Despawn(gameObject);

        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.OnHit(data.damage);
                LeanPool.Spawn(hitEffectPrefab, hitPoint.position, hitPoint.rotation);

                if (gameObject.activeSelf)
                    LeanPool.Despawn(gameObject);
            }
        }
    }
}
