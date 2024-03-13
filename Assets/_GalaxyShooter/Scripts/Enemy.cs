using DG.Tweening;
using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform _myTransform;
    public EnemyData enemyData;
    private int _currentHealth;
    [SerializeField] Slider healthBar;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] Collider2D col;

    public AudioClip explosion;
    public AudioClip hit;

    private void Awake()
    {
        _myTransform = transform;
        _currentHealth = enemyData.health;
        healthBar.value = 1;
    }
    private void Start()
    {
        healthBar.gameObject.SetActive(false);
        col.enabled = false;
    }

    public void OnHit(int damage)
    {
        EnableHealthBar();

        _currentHealth -= damage;
        healthBar.value = (float)_currentHealth/enemyData.health;

        if (_currentHealth <= 0)
        {
            LeanPool.Spawn(explosionPrefab, transform.position, transform.rotation);
            AudioManager.Instance.Shoot(explosion);
            GameManager.Instance.AddScore();
            LeanPool.Despawn(gameObject);
        }
        else
            AudioManager.Instance.Shoot(hit);
    }

    void EnableHealthBar()
    {
        if (!healthBar.gameObject.activeSelf)
            healthBar.gameObject.SetActive(true);
    }

    public void MoveToTacticPosition(Vector3 pos, float time)
    {
        _myTransform.DOMove(pos, time);
    }

    public void EnableCollider()
    {
        col.enabled = true;
    }
}
