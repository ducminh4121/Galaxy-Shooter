using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    private int _currentHealth;
    [SerializeField] Slider healthBar;
    [SerializeField] GameObject explosionPrefab;

    private void Awake()
    {
        _currentHealth = enemyData.health;
        healthBar.value = 1;
    }
    private void Start()
    {
        healthBar.gameObject.SetActive(false);
    }

    public void OnHit(int damage)
    {
        EnableHealthBar();

        _currentHealth -= damage;
        healthBar.value = (float)_currentHealth/enemyData.health;

        if ( _currentHealth <= 0 ) 
        {
            LeanPool.Spawn(explosionPrefab, transform.position, transform.rotation);
            LeanPool.Despawn(gameObject);
        }
    }

    void EnableHealthBar()
    {
        if (!healthBar.gameObject.activeSelf)
            healthBar.gameObject.SetActive(true);
    }
}
