using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    public event Action OnLevelComplete;
    public event Action OnLevelFailed;
    public event Action OnEnemyDied;

    private List<Enemy> _enemies = new List<Enemy>();
    private int _totalNumberOfEnemies;

    private void Awake()
    {
        _enemies = GetComponentsInChildren<Enemy>().ToList();

        foreach (Enemy enemy in _enemies)
        {
            enemy.OnDeath += Enemy_OnDeath;
        }

        _totalNumberOfEnemies = _enemies.Count;
    }

    private void Start()
    {
        FindObjectOfType<PlayerController>().OnKilled += PlayerController_OnKilled;
    }

    private void PlayerController_OnKilled(Vector3 obj)
    {
        OnLevelFailed?.Invoke();
    }

    private void Enemy_OnDeath(Enemy enemy)
    {
        enemy.OnDeath -= Enemy_OnDeath;
        _enemies.Remove(enemy);

        OnEnemyDied?.Invoke();

        if (CheckIfAllEnemiesDead())
        {
            OnLevelComplete?.Invoke();
        }
    }

    public bool CheckIfAllEnemiesDead()
    {
        return _enemies.Count == 0;
    }

    public int GetTotalNumberOfEnemies()
    {
        return _totalNumberOfEnemies;
    }

    public int GetCurrentNumberOfEnemiesAlive()
    {
        return _enemies.Count;
    }
}
