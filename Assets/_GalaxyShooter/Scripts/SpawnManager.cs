using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<EnemyTactic> enemyTransforms = new List<EnemyTactic>();
    [SerializeField] GameObject enemyPrafabs;
    [SerializeField] Transform leftSpawnPos;
    [SerializeField] Transform rightSpawnPos;
    [SerializeField] float moveTime = 2.5f;
    [SerializeField] float changeTacticTime = 5f;
    [SerializeField] Transform enimiesTrans;

    private List<Enemy> _enemies = new List<Enemy>();
    private int _tacticStage = 0;
    
    /*
    private void Start()
    {
        FirstWaySpawn();
        
        StartCoroutine(MoveTactic());
    }
    */
    public void StartGame()
    {
        FirstWaySpawn();

        StartCoroutine(MoveTactic());
    }


    void FirstWaySpawn()
    {
        for (int i = 0; i < enemyTransforms[0].tacticTransforms.Count; i++)
        {
            if ((i % 2) == 0)
                SpawnEnemy(leftSpawnPos, enemyTransforms[0].tacticTransforms[i]);
            else
                SpawnEnemy(rightSpawnPos, enemyTransforms[0].tacticTransforms[i]);
        }
        _tacticStage++;
    }

    void SpawnEnemy(Transform spawnTrans, Transform tacticTrans)
    {
        GameObject enemyGO = LeanPool.Spawn(enemyPrafabs, spawnTrans.position, spawnTrans.rotation, enimiesTrans);
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        enemy.MoveToTacticPosition(tacticTrans.position, moveTime);

        _enemies.Add(enemy);
    }

    IEnumerator MoveTactic()
    {
        yield return new WaitForSeconds(changeTacticTime + moveTime);

        for (int i = 0;i < _enemies.Count;i++)
        {
            _enemies[i].MoveToTacticPosition(enemyTransforms[_tacticStage].tacticTransforms[i].position, moveTime);
        }

        _tacticStage++;

        if (_tacticStage < enemyTransforms.Count)
            StartCoroutine(MoveTactic());
        else
            StartCoroutine(EnableEnemyCombat());
    }

    IEnumerator EnableEnemyCombat()
    {
        yield return new WaitForSeconds(moveTime);

        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].EnableCollider();
        }
    }
}
